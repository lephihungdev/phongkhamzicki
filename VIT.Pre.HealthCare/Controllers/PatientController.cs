namespace VIT.Pre.HealthCare.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;

    using VIT.BusinessLogicLayer.HealthCare;
    using VIT.DataTransferObject.HealthCare;
    using VIT.Library;
    using VIT.Library.Web.ObjectData;
    using VIT.Pre.HealthCare.Models;

    public class PatientController : Controller
    {
        private readonly PatientBLL _patientBLL;
        private readonly FacilityBLL _facilityBLL;

        public PatientController()
        {
            this._patientBLL = new PatientBLL();
            this._facilityBLL = new FacilityBLL();
        }

        public ActionResult Search(string key, bool allfacility = false)
        {
            var user = this.Session[SettingsManager.Constants.SessionUser] as UserData;
            if (user == null) return this.RedirectToAction("Login", "Login");
            this.ViewBag.FacilityName = this._facilityBLL.GetFacilityName(user.CompanyId);
            this.ViewBag.UserName = user.UserName;
            
            var facility = 0;
            if(!allfacility) facility = user.CompanyId;
            
            var model = new PatientModel();
            model.Patients = this._patientBLL.Search(key, facility).ToList();
            model.Sexs = this._patientBLL.GetSexs();

            return this.View(model);
        }

        public ActionResult Delete(int id)
        {
            var user = this.Session[SettingsManager.Constants.SessionUser] as UserData;
            if (user == null) return this.RedirectToAction("Login", "Login");

            this._patientBLL.Delete(id, user.CompanyId);
            return this.RedirectToAction("Search", "Patient");
        }

        [HttpPost]
        public ActionResult Search(PatientModel model)
        {
            var user = this.Session[SettingsManager.Constants.SessionUser] as UserData;
            if (user == null) return this.RedirectToAction("Login", "Login");
            this.ViewBag.FacilityName = this._facilityBLL.GetFacilityName(user.CompanyId);
            this.ViewBag.UserName = user.UserName;

            var dto = new PatientDto
            {
                Address = model.Address,
                Birthday = model.Birthday,
                DateOnSet = model.DateOnSet,
                Email = model.Email,
                FirstName = model.FirstName,
                LastName = model.LastName,
                Phone = model.Phone,
                Id = model.Id,
                Sex = model.Sex
            };

            if (model.Id > 0) this._patientBLL.Update(dto, user.CompanyId);
            else
            {
                this._patientBLL.Insert(dto, user.CompanyId);
                model.Id = dto.Id;
            }

            model.Patients = this._patientBLL.Search(string.Empty, user.CompanyId).ToList();
            model.Sexs = this._patientBLL.GetSexs();

            return this.View(model);
        }
    }
}
