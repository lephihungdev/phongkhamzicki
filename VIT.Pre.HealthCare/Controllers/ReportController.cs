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

    public class ReportController : Controller
    {
        private readonly PatientBLL _patientBLL;
        private readonly FacilityBLL _facilityBLL;
        private readonly ChargeBLL _chargeBLL;

        public ReportController()
        {
            this._patientBLL = new PatientBLL();
            this._facilityBLL = new FacilityBLL();
            this._chargeBLL = new ChargeBLL();
        }

        public ActionResult Index()
        {
            return this.View();
        }

        public ActionResult Patient()
        {
            var user = this.Session[SettingsManager.Constants.SessionUser] as UserData;
            if (user == null) return this.RedirectToAction("Login", "Login");
            this.ViewBag.FacilityName = this._facilityBLL.GetFacilityName(user.CompanyId);
            this.ViewBag.UserName = user.UserName;

            var model = new PatientReportModel();
            model.FromDate = DateTime.Now;
            model.ToDate = DateTime.Now;
            model.Patients = new List<PatientDto>();

            return this.View(model);
        }

        [HttpPost]
        public ActionResult Patient(PatientReportModel model)
        {
            var user = this.Session[SettingsManager.Constants.SessionUser] as UserData;
            if (user == null) return this.RedirectToAction("Login", "Login");
            this.ViewBag.FacilityName = this._facilityBLL.GetFacilityName(user.CompanyId);
            this.ViewBag.UserName = user.UserName;

            model.Patients = this._patientBLL.Gets(user.CompanyId, model.FromDate, model.ToDate).OrderBy(e => e.LastName).ToList(); ;
            return this.View(model);
        }

        public ActionResult Charge()
        {
            var user = this.Session[SettingsManager.Constants.SessionUser] as UserData;
            if (user == null) return this.RedirectToAction("Login", "Login");
            this.ViewBag.FacilityName = this._facilityBLL.GetFacilityName(user.CompanyId);
            this.ViewBag.UserName = user.UserName;

            var model = new ChargeReportModel();
            model.FromDate = DateTime.Now;
            model.ToDate = DateTime.Now;
            model.Charges = new List<ChargeReportDto>();

            return this.View(model);
        }

        [HttpPost]
        public ActionResult Charge(ChargeReportModel model)
        {
            var user = this.Session[SettingsManager.Constants.SessionUser] as UserData;
            if (user == null) return this.RedirectToAction("Login", "Login");
            this.ViewBag.FacilityName = this._facilityBLL.GetFacilityName(user.CompanyId);
            this.ViewBag.UserName = user.UserName;

            model.Charges = this._chargeBLL.GetReports(user.CompanyId, model.FromDate, model.ToDate).OrderBy(e => e.DateService).ToList(); ;
            return this.View(model);
        }
    }
}
