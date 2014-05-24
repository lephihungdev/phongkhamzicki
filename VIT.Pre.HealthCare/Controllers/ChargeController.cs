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

        public ActionResult Detail(int patientId)
        {
            var user = this.Session[SettingsManager.Constants.SessionUser] as UserData;
            if (user == null) return this.RedirectToAction("Login", "Login");
            this.ViewBag.FacilityName = this._facilityBLL.GetFacilityName(user.CompanyId);
            this.ViewBag.UserName = user.UserName;

            var model = new ChargeModel();

            model.ListCpts = this._cptBLL.Get(user.CompanyId).ToList();
            model.ListCpts.Insert(0, new CptDto { Code = null, Description = "--- Chọn ---" });

            model.ListDoctors = this._doctorBLL.Get(user.CompanyId).ToList();
            model.ListDoctors.Insert(0, new DoctorDto { Id = 0, LastName = "--- Chọn ---" });

            model.ListDrugs = this._drugBLL.Get(user.CompanyId).ToList();
            model.ListDrugs.Insert(0, new DrugDto { Name = null, Description = "--- Chọn ---" });

            model.ListIcds = this._icdBLL.Get().ToList();
            model.ListIcds.Insert(0, new IcdDto { Code = null, Description = "--- Chọn ---" });

            var facility = 0;
            facility = user.CompanyId;

            model.DateOnset = this._chargeBLL.Get(patientId, facility).Select(e => e.DateOnset).Max();
            model.DateService = DateTime.Now;

            var charges = this._chargeBLL.Get(patientId, facility).ToList();
            foreach (var charge in charges)
            {
                charge.CPTDescription = model.ListCpts.Where(e => e.Code == charge.CPTCode).Select(e => e.Description).FirstOrDefault();

                charge.DiagnosticDisplay = charge.Diagnostic;
                if (!string.IsNullOrEmpty(charge.ICDCode1))
                {
                    charge.DiagnosticDisplay += string.IsNullOrEmpty(charge.DiagnosticDisplay) ? string.Empty : ", ";
                    charge.DiagnosticDisplay += model.ListIcds.Where(e => e.Code == charge.ICDCode1).Select(e => e.Description).FirstOrDefault();
                }

                if (!string.IsNullOrEmpty(charge.ICDCode2))
                {
                    charge.DiagnosticDisplay += string.IsNullOrEmpty(charge.DiagnosticDisplay) ? string.Empty : ", ";
                    charge.DiagnosticDisplay += model.ListIcds.Where(e => e.Code == charge.ICDCode2).Select(e => e.Description).FirstOrDefault();
                }

                if (!string.IsNullOrEmpty(charge.ICDCode3))
                {
                    charge.DiagnosticDisplay += string.IsNullOrEmpty(charge.DiagnosticDisplay) ? string.Empty : ", ";
                    charge.DiagnosticDisplay += model.ListIcds.Where(e => e.Code == charge.ICDCode2).Select(e => e.Description).FirstOrDefault();
                }

                if (!string.IsNullOrEmpty(charge.ICDCode4))
                {
                    charge.DiagnosticDisplay += string.IsNullOrEmpty(charge.DiagnosticDisplay) ? string.Empty : ", ";
                    charge.DiagnosticDisplay += model.ListIcds.Where(e => e.Code == charge.ICDCode2).Select(e => e.Description).FirstOrDefault();
                }
            }

            model.ListCharges = charges;

            return View(model);
        }

        [HttpPost]
        public ActionResult Detail(ChargeModel model)
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

            model.ListCpts = this._cptBLL.Get(user.CompanyId).Where(e => e.Active).ToList();
            model.ListCpts.Insert(0, new CptDto { Code = null, Description = "--- Chọn ---" });

            model.ListDoctors = this._doctorBLL.Get(user.CompanyId).Where(e => e.Active).ToList();
            model.ListDoctors.Insert(0, new DoctorDto { LastName = "--- Chọn ---" });

            model.ListDrugs = this._drugBLL.Get(user.CompanyId).Where(e => e.Active).ToList();
            model.ListDrugs.Insert(0, new DrugDto { Name = null, Description = "--- Chọn ---" });

            model.ListIcds = this._icdBLL.Get().Where(e => e.Active).ToList();
            model.ListIcds.Insert(0, new IcdDto { Code = null, Description = "--- Chọn ---" });

            var facility = 0;
            facility = user.CompanyId;

            var charges = this._chargeBLL.Get(model.PatientId, facility).ToList();
            foreach (var charge in charges)
            {
                charge.CPTDescription = model.ListCpts.Where(e => e.Code == charge.CPTCode).Select(e => e.Description).FirstOrDefault();

                charge.DiagnosticDisplay = charge.Diagnostic;
                if (!string.IsNullOrEmpty(charge.ICDCode1))
                {
                    charge.DiagnosticDisplay += string.IsNullOrEmpty(charge.DiagnosticDisplay) ? string.Empty : ", ";
                    charge.DiagnosticDisplay += model.ListIcds.Where(e => e.Code == charge.ICDCode1).Select(e => e.Description).FirstOrDefault();
                }

                if (!string.IsNullOrEmpty(charge.ICDCode2))
                {
                    charge.DiagnosticDisplay += string.IsNullOrEmpty(charge.DiagnosticDisplay) ? string.Empty : ", ";
                    charge.DiagnosticDisplay += model.ListIcds.Where(e => e.Code == charge.ICDCode2).Select(e => e.Description).FirstOrDefault();
                }

                if (!string.IsNullOrEmpty(charge.ICDCode3))
                {
                    charge.DiagnosticDisplay += string.IsNullOrEmpty(charge.DiagnosticDisplay) ? string.Empty : ", ";
                    charge.DiagnosticDisplay += model.ListIcds.Where(e => e.Code == charge.ICDCode2).Select(e => e.Description).FirstOrDefault();
                }

                if (!string.IsNullOrEmpty(charge.ICDCode4))
                {
                    charge.DiagnosticDisplay += string.IsNullOrEmpty(charge.DiagnosticDisplay) ? string.Empty : ", ";
                    charge.DiagnosticDisplay += model.ListIcds.Where(e => e.Code == charge.ICDCode2).Select(e => e.Description).FirstOrDefault();
                }
            }
            model.ListCharges = charges;

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
