namespace VIT.Pre.HealthCare.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;

    using VIT.BusinessLogicLayer.HealthCare;
    using VIT.DataTransferObject.HealthCare;
    using VIT.Library;
    using VIT.Library.Web.ObjectData;
    using VIT.Pre.HealthCare.Models;

    public class ChargeController : Controller
    {
        private readonly FacilityBLL _facilityBLL;
        private readonly PatientBLL _patientBLL;
        private readonly TreatmentBLL _treatmentBLL;
        private readonly IcdBLL _icdBLL;
        private readonly ChargeBLL _chargeBLL;

        private readonly DoctorBLL _doctorBLL;

        public ChargeController()
        {
            this._facilityBLL = new FacilityBLL();
            this._patientBLL = new PatientBLL();
            this._treatmentBLL = new TreatmentBLL();
            this._icdBLL = new IcdBLL();
            this._chargeBLL = new ChargeBLL();
            this._doctorBLL = new DoctorBLL();
        }

        public ActionResult PrintCharge(int chargeId)
        {
            var user = this.Session[SettingsManager.Constants.SessionUser] as UserData;
            if (user == null) return this.RedirectToAction("Login", "Login");
            this.ViewBag.FacilityName = this._facilityBLL.GetFacilityName(user.CompanyId);
            this.ViewBag.UserName = user.UserName;

            var chargeInfo = this._chargeBLL.GetInfo(chargeId);
            var model = new ChargePrintModel
                {
                    Id = chargeInfo.Id,
                    CPT = chargeInfo.Treatments,
                    DateService = chargeInfo.DateService,
                    Days = chargeInfo.Days,
                    Diagnostic = chargeInfo.Diagnostic,
                    DoctorName = chargeInfo.DoctorName,
                    Note = chargeInfo.Note,
                    PatientId = chargeInfo.PatientId,
                    PatientName = chargeInfo.PatientName                    
                };

            model.Drugs = this._chargeBLL.GetDrugs(model.PatientId, chargeId).ToList();
            model.Instruments = this._chargeBLL.GetInstruments(model.PatientId, chargeId).ToList();

            return View(model);
        }

        public ActionResult PrintCharges(int patientId, bool allfacility = false)
        {
            var user = this.Session[SettingsManager.Constants.SessionUser] as UserData;
            if (user == null) return this.RedirectToAction("Login", "Login");

            this.ViewBag.PatientId = patientId;

            var patientName = this._patientBLL.GetById(patientId);
            if (patientName != null) this.ViewBag.PatientName = patientName.FirstName + " " + patientName.LastName;

            var facility = this._facilityBLL.GetAll(patientId);
            if (!allfacility) facility = facility.Where(e => e.value == user.CompanyId);

            var models = facility.Select(e => new ChargesPrintModel
                    {
                        FacilityId = e.value,
                        FacilityName = e.label
                    })
                    .ToList();

            var icds = this._icdBLL.Get().ToList();
            foreach (var chargesPrintModel in models)
            {
                var charges = this._chargeBLL.Gets(patientId, chargesPrintModel.FacilityId).OrderByDescending(e => e.DateService).ToList();
                foreach (var charge in charges)
                {
                    charge.DiagnosticDisplay = charge.Diagnostic;
                    if(!string.IsNullOrEmpty(charge.ICDCode1))
                    {
                        charge.DiagnosticDisplay += string.IsNullOrEmpty(charge.DiagnosticDisplay) ? string.Empty : ", ";
                        charge.DiagnosticDisplay += icds.Where(e => e.Code == charge.ICDCode1).Select(e => e.Description).FirstOrDefault();
                    }

                    if (!string.IsNullOrEmpty(charge.ICDCode2))
                    {
                        charge.DiagnosticDisplay += string.IsNullOrEmpty(charge.DiagnosticDisplay) ? string.Empty : ", ";
                        charge.DiagnosticDisplay += icds.Where(e => e.Code == charge.ICDCode2).Select(e => e.Description).FirstOrDefault();
                    }

                    if (!string.IsNullOrEmpty(charge.ICDCode3))
                    {
                        charge.DiagnosticDisplay += string.IsNullOrEmpty(charge.DiagnosticDisplay) ? string.Empty : ", ";
                        charge.DiagnosticDisplay += icds.Where(e => e.Code == charge.ICDCode2).Select(e => e.Description).FirstOrDefault();
                    }

                    if (!string.IsNullOrEmpty(charge.ICDCode4))
                    {
                        charge.DiagnosticDisplay += string.IsNullOrEmpty(charge.DiagnosticDisplay) ? string.Empty : ", ";
                        charge.DiagnosticDisplay += icds.Where(e => e.Code == charge.ICDCode2).Select(e => e.Description).FirstOrDefault();
                    }
                }
                chargesPrintModel.ListCharges = charges;
            }

            return View(models);
        }

        public ActionResult Detail(int patientId, int chargeId = 0)
        {
            var user = this.Session[SettingsManager.Constants.SessionUser] as UserData;
            if (user == null) return this.RedirectToAction("Login", "Login");
            this.ViewBag.FacilityName = this._facilityBLL.GetFacilityName(user.CompanyId);
            this.ViewBag.UserName = user.UserName;

            var model = new ChargeDetailModel();

            model.Id = chargeId;
            model.PatientId = patientId;
            var patientName = this._patientBLL.GetById(patientId);
            if (patientName == null) ViewBag.ErrorLabel = "Bệnh nhân không tồn tại";
            else model.PatientName = patientName.FirstName + " " + patientName.LastName;

            var charge = this._chargeBLL.Gets(patientId, user.CompanyId);
            if (chargeId == 0)
            {
                model.DateOnset = charge.Select(e => e.DateOnset).Max();
                model.DateService = DateTime.Now;
            }
            else
            {
                var chargeInfo = charge.FirstOrDefault(e => e.Id == chargeId);
                if (chargeInfo == null) ViewBag.ErrorLabel = "Điều trị không tồn tại";
                model.DateOnset = chargeInfo.DateOnset;
                model.DateService = chargeInfo.DateService;

                model.Days = chargeInfo.Days;
                model.Note = chargeInfo.Note;
                model.ICDCode1 = chargeInfo.ICDCode1;
                model.ICDCode2 = chargeInfo.ICDCode2;
                model.ICDCode3 = chargeInfo.ICDCode3;
                model.ICDCode4 = chargeInfo.ICDCode4;
                model.Treatments = chargeInfo.Treatments;
                model.DoctorId = chargeInfo.DoctorId;
                model.Diagnostic = chargeInfo.Diagnostic;
            }

            model.ListDoctors = this._doctorBLL.Get(user.CompanyId).ToList();
            model.ListDoctors.Insert(0, new DoctorDto { Id = 0, LastName = "--- Chọn ---" });
            model.ListChargeDrugs = this._chargeBLL.GetDrugs(patientId, chargeId).ToList();
            model.ListChargeInstruments = this._chargeBLL.GetInstruments(model.PatientId, model.Id).ToList();

            if (chargeId == 0) model.Clinical = this._patientBLL.GetClinical(model.PatientId, null);
            else model.Clinical = this._patientBLL.GetClinical(model.PatientId, chargeId);

            return View(model);
        }

        [HttpPost]
        public ActionResult Detail(ChargeDetailModel model, string action)
        {
            var user = this.Session[SettingsManager.Constants.SessionUser] as UserData;
            if (user == null) return this.RedirectToAction("Login", "Login");
            this.ViewBag.FacilityName = this._facilityBLL.GetFacilityName(user.CompanyId);
            this.ViewBag.UserName = user.UserName;

            var patientName = this._patientBLL.GetById(model.PatientId);
            if (patientName != null) model.PatientName = patientName.FirstName + " " + patientName.LastName;
            
            switch (action)
            {
                case "ADDDRUG":
                    if (model.DrugId > 0)
                    {
                        var drugDto = new ChargeDrugDto
                            {
                                ChargeId = model.Id,
                                DrugId = model.DrugId,
                                Quality = model.DrugQuality,
                                Note = model.DrugNote,
                                PatientId = model.PatientId
                            };
                        this._chargeBLL.AddChargeDrug(drugDto);
                        model.DrugNote = string.Empty;
                        model.DrugQuality = null;
                        model.DrugId = 0;
                    }

                    break;
                case "REMOVEDRUG":
                    this._chargeBLL.RemoveChargeDrug(model.DrugId, model.PatientId, model.Id);
                    model.DrugId = 0;
                    break;
                case "ADDINSTRUMENT":
                    if (model.DrugId > 0)
                    {
                        var drugDto = new ChargeInstrumentDto
                        {
                            ChargeId = model.Id,
                            InstrumentId = model.InstrumentId,
                            Quality = model.InstrumentQuality,
                            Note = model.InstrumentNote,
                            PatientId = model.PatientId
                        };
                        this._chargeBLL.AddChargeInstrument(drugDto);
                        model.DrugNote = string.Empty;
                        model.DrugQuality = null;
                        model.DrugId = 0;
                    }

                    break;
                case "REMOVEDINSTRUMENT":
                    this._chargeBLL.RemoveChargeInstrument(model.DrugId, model.PatientId, model.Id);
                    model.DrugId = 0;
                    break;
                case "ADDCHARGE": 
                    var dto = new ChargeDto
                    {
                        Id = model.Id,
                        Treatments = model.Treatments,
                        Diagnostic = model.Diagnostic,
                        DoctorId = model.DoctorId == 0 ? null : model.DoctorId,
                        ICDCode1 = model.ICDCode1,
                        ICDCode2 = model.ICDCode2,
                        ICDCode3 = model.ICDCode3,
                        ICDCode4 = model.ICDCode4,
                        Note = model.Note,
                        Days = model.Days,
                        DateOnset = model.DateOnset,
                        DateService = model.DateService,
                    };

                    if (dto.Id == 0)
                    {
                        this._chargeBLL.Insert(dto, model.PatientId, user.CompanyId, user.UserId);
                        model.Id = dto.Id;
                    }
                    else this._chargeBLL.Update(dto, model.PatientId, user.CompanyId, user.UserId);

                    break;
            }

            model.ListDoctors = this._doctorBLL.Get(user.CompanyId).Where(e => e.Active).ToList();
            model.ListDoctors.Insert(0, new DoctorDto { LastName = "--- Chọn ---" });
            model.ListChargeDrugs = this._chargeBLL.GetDrugs(model.PatientId, model.Id).ToList();
            model.ListChargeInstruments = this._chargeBLL.GetInstruments(model.PatientId, model.Id).ToList();

            if (model.Id == 0) model.Clinical = this._patientBLL.GetClinical(model.PatientId, null);
            else model.Clinical = this._patientBLL.GetClinical(model.PatientId, model.Id);

            return View(model);
        }

        public ActionResult Index(int patientId, bool allfacility = false)
        {
            var user = this.Session[SettingsManager.Constants.SessionUser] as UserData;
            if (user == null) return this.RedirectToAction("Login", "Login");
            this.ViewBag.FacilityName = this._facilityBLL.GetFacilityName(user.CompanyId);
            this.ViewBag.UserName = user.UserName;

            var model = new ChargeListModel();

            model.PatientId = patientId;
            var patientName = this._patientBLL.GetById(patientId);
            if (patientName == null)
            {
                ViewBag.ErrorLabel = "Bệnh nhân không tồn tại";
                model.ListCharges = new List<ChargeDto>();
                return View(model);
            }

            model.PatientName = patientName.FirstName + " " + patientName.LastName;
            model.DateService = this._chargeBLL.Gets(patientId, user.CompanyId).Select(e => e.DateService).Max();

            var icds = this._icdBLL.Get().ToList();

            var facility = 0;
            if (!allfacility) facility = user.CompanyId;

            var charges = this._chargeBLL.Gets(patientId, facility).OrderByDescending(e => e.DateService).ToList();
            foreach (var charge in charges)
            {
                charge.DiagnosticDisplay = charge.Diagnostic;
                if(!string.IsNullOrEmpty(charge.ICDCode1))
                {
                    charge.DiagnosticDisplay += string.IsNullOrEmpty(charge.DiagnosticDisplay) ? string.Empty : ", ";
                    charge.DiagnosticDisplay += icds.Where(e => e.Code == charge.ICDCode1).Select(e => e.Description).FirstOrDefault();
                }

                if (!string.IsNullOrEmpty(charge.ICDCode2))
                {
                    charge.DiagnosticDisplay += string.IsNullOrEmpty(charge.DiagnosticDisplay) ? string.Empty : ", ";
                    charge.DiagnosticDisplay += icds.Where(e => e.Code == charge.ICDCode2).Select(e => e.Description).FirstOrDefault();
                }

                if (!string.IsNullOrEmpty(charge.ICDCode3))
                {
                    charge.DiagnosticDisplay += string.IsNullOrEmpty(charge.DiagnosticDisplay) ? string.Empty : ", ";
                    charge.DiagnosticDisplay += icds.Where(e => e.Code == charge.ICDCode2).Select(e => e.Description).FirstOrDefault();
                }

                if (!string.IsNullOrEmpty(charge.ICDCode4))
                {
                    charge.DiagnosticDisplay += string.IsNullOrEmpty(charge.DiagnosticDisplay) ? string.Empty : ", ";
                    charge.DiagnosticDisplay += icds.Where(e => e.Code == charge.ICDCode2).Select(e => e.Description).FirstOrDefault();
                }
            }

            model.ListCharges = charges;

            return View(model);
        }

        public ActionResult Delete(int patientId, int id, bool allfacility = false)
        {
            var user = this.Session[SettingsManager.Constants.SessionUser] as UserData;
            if (user == null) return this.RedirectToAction("Login", "Login");
            this.ViewBag.FacilityName = this._facilityBLL.GetFacilityName(user.CompanyId);
            this.ViewBag.UserName = user.UserName;

            try
            {
                this._chargeBLL.Delete(id, user.CompanyId);
            }
            catch (Exception exception)
            {
                ViewBag.ErrorLabel = exception.Message;
            }

            return this.RedirectToAction("Index", "Charge", new { PatientId = patientId, Allfacility = allfacility });
        }
    }
}
