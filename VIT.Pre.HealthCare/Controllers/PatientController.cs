namespace VIT.Pre.HealthCare.Controllers
{
    using System;
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
            var patients = this._patientBLL.Search(key, facility).ToList();
            model.Patients = patients;
            model.Sexs = this._patientBLL.GetSexs();

            return this.View(model);
        }

        public ActionResult Delete(int id)
        {
            var user = this.Session[SettingsManager.Constants.SessionUser] as UserData;
            if (user == null) return this.RedirectToAction("Login", "Login");

            try
            {
                this._patientBLL.Delete(id, user.CompanyId);
            }
            catch (Exception exception)
            {
                ViewBag.ErrorLabel = exception.Message;
            }

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
                BirthYear = model.BirthYear,
                Email = model.Email,
                FirstName = model.FirstName,
                LastName = model.LastName,
                Phone = model.Phone,
                Id = model.Id,
                Sex = model.Sex
            };

            try
            {
                if (model.Id > 0) this._patientBLL.Update(dto, user.CompanyId);
                else
                {
                    this._patientBLL.Insert(dto, user.CompanyId);
                    model.Id = dto.Id;
                }
            }
            catch (Exception exception)
            {
                ViewBag.ErrorLabel = exception.Message;
            }

            var patients = this._patientBLL.Search(string.Empty, user.CompanyId).ToList();
            
            model.Patients = patients;
            model.Sexs = this._patientBLL.GetSexs();

            return this.View(model);
        }
    }
}
