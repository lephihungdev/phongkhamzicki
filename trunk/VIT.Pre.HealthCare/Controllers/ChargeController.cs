namespace VIT.Pre.HealthCare.Controllers
{
    using System.Linq;
    using System.Web.Mvc;

    using VIT.BusinessLogicLayer.HealthCare;
    using VIT.DataTransferObject.HealthCare;
    using VIT.Library;
    using VIT.Library.Web.ObjectData;
    using VIT.Pre.HealthCare.Models;
    using System;

    public class ChargeController : Controller
    {
        private readonly FacilityBLL _facilityBLL;
        private readonly PatientBLL _patientBLL;
        private readonly CptBLL _cptBLL;
        private readonly IcdBLL _icdBLL;
        private readonly ChargeBLL _chargeBLL;
        private readonly DrugBLL _drugBLL;
        private readonly DoctorBLL _doctorBLL;

        public ChargeController()
        {
            this._facilityBLL = new FacilityBLL();
            this._patientBLL = new PatientBLL();
            this._cptBLL = new CptBLL();
            this._icdBLL = new IcdBLL();
            this._chargeBLL = new ChargeBLL();
            this._drugBLL = new DrugBLL();
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
                    CPT = chargeInfo.CPT,
                    DateService = chargeInfo.DateService,
                    Days = chargeInfo.Days,
                    Diagnostic = chargeInfo.Diagnostic,
                    DoctorName = chargeInfo.DoctorName,
                    Note = chargeInfo.Note,
                    PatientId = chargeInfo.PatientId,
                    PatientName = chargeInfo.PatientName                    
                };

            model.Drugs = this._chargeBLL.GetDrugs(model.PatientId, chargeId).ToList();

            return View(model);
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
            var patientName = this._patientBLL.Get(user.CompanyId).Where(e => e.Id == patientId).Select(e => new { e.FirstName, e.LastName }).FirstOrDefault();
            if (patientName == null) ViewBag.ErrorLabel = "Bệnh nhân không tồn tại";
            model.PatientName = patientName.FirstName + " " + patientName.LastName;

            var charge = this._chargeBLL.Get(patientId, user.CompanyId);
            if (chargeId == 0)
            {
                model.DateOnset = charge.Select(e => e.DateOnset).Max();
                model.DateService = DateTime.Now;
            }
            else
            {
                var chargeInfo = charge.FirstOrDefault(e => e.Id == chargeId);
                if (patientName == null) ViewBag.ErrorLabel = "Điều trị không tồn tại";
                model.DateOnset = chargeInfo.DateOnset;
                model.DateService = chargeInfo.DateService;

                model.Days = chargeInfo.Days;
                model.Note = chargeInfo.Note;
                //model.ICDCode1 = chargeInfo.ICDCode1;
                //model.ICDCode2 = chargeInfo.ICDCode2;
                //model.ICDCode3 = chargeInfo.ICDCode3;
                //model.ICDCode4 = chargeInfo.ICDCode4;
                //model.CPTCode = chargeInfo.CPTCode;
                model.DoctorId = chargeInfo.DoctorId;
                model.Diagnostic = chargeInfo.Diagnostic;
            }

            model.ListDoctors = this._doctorBLL.Get(user.CompanyId).ToList();
            model.ListDoctors.Insert(0, new DoctorDto { Id = 0, LastName = "--- Chọn ---" });
            model.ListChargeDrugs = this._chargeBLL.GetDrugs(patientId, chargeId).ToList();

            return View(model);
        }

        [HttpPost]
        public ActionResult Detail(ChargeDetailModel model)
        {
            var user = this.Session[SettingsManager.Constants.SessionUser] as UserData;
            if (user == null) return this.RedirectToAction("Login", "Login");
            this.ViewBag.FacilityName = this._facilityBLL.GetFacilityName(user.CompanyId);
            this.ViewBag.UserName = user.UserName;

            var dto = new ChargeDto
            {
                Id = model.Id,
                CPTCode = model.CPTCode,
                Diagnostic = model.Diagnostic,
                DoctorId = model.DoctorId == 0 ? null : model.DoctorId,
                ICDCode1 = model.ICDCode1,
                ICDCode2 = model.ICDCode2,
                ICDCode3 = model.ICDCode3,
                ICDCode4 = model.ICDCode4,
                Note = model.Note,
                Days = model.Days
            };

            model.ListDoctors = this._doctorBLL.Get(user.CompanyId).Where(e => e.Active).ToList();
            model.ListDoctors.Insert(0, new DoctorDto { LastName = "--- Chọn ---" });
            model.ListChargeDrugs = this._chargeBLL.GetDrugs(model.PatientId, model.Id).ToList();

            var facility = 0;
            facility = user.CompanyId;

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
            var patientName = this._patientBLL.Get(user.CompanyId).Where(e => e.Id == patientId).Select(e => new { e.FirstName, e.LastName }).FirstOrDefault();
            if (patientName == null) ViewBag.ErrorLabel = "Bệnh nhân không tồn tại";
            model.PatientName = patientName.FirstName + " " + patientName.LastName;
            model.DateService = this._chargeBLL.Get(patientId, user.CompanyId).Select(e => e.DateService).Max();

            var cpts = this._cptBLL.Get(user.CompanyId).ToList();
            var icds = this._icdBLL.Get().ToList();

            var facility = 0;
            if (!allfacility) facility = user.CompanyId;

            var charges = this._chargeBLL.Get(patientId, facility).OrderByDescending(e => e.DateService).ToList();
            foreach (var charge in charges)
            {
                charge.CPTDescription = cpts.Where(e => e.Code == charge.CPTCode).Select(e => e.Description).FirstOrDefault();

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
    }
}
