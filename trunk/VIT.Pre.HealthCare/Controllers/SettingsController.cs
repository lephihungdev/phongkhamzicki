using System.Web.Mvc;
using System.Linq;

namespace VIT.Pre.HealthCare.Controllers
{
    using VIT.BusinessLogicLayer.HealthCare;
    using VIT.Library;
    using VIT.Library.Web.ObjectData;
    using VIT.Pre.HealthCare.Models;
    using VIT.DataTransferObject.HealthCare;

    public class SettingsController : Controller
    {
        private readonly FacilityBLL _facilityBLL;
        private readonly DrugBLL _drugBLL;
        private readonly DoctorBLL _doctorBLL;

        public SettingsController()
        {
            this._facilityBLL = new FacilityBLL();
            this._drugBLL = new DrugBLL();
            this._doctorBLL = new DoctorBLL();
        }

        public ActionResult Index()
        {
            var user = this.Session[SettingsManager.Constants.SessionUser] as UserData;
            if (user == null) return this.RedirectToAction("Login", "Login");
            this.ViewBag.FacilityName = this._facilityBLL.GetFacilityName(user.CompanyId);
            this.ViewBag.UserName = user.UserName;

            return View();
        }

        public ActionResult Drug(string code)
        {
            var user = this.Session[SettingsManager.Constants.SessionUser] as UserData;
            if (user == null) return this.RedirectToAction("Login", "Login");
            this.ViewBag.FacilityName = this._facilityBLL.GetFacilityName(user.CompanyId);
            this.ViewBag.UserName = user.UserName;

            var model = new DrugModel();
            model.Drugs = this._drugBLL.Get(user.CompanyId, code).ToList();
            return this.View(model);
        }

        [HttpPost]
        public ActionResult Drug(DrugModel model)
        {
            var user = this.Session[SettingsManager.Constants.SessionUser] as UserData;
            if (user == null) return this.RedirectToAction("Login", "Login");
            this.ViewBag.FacilityName = this._facilityBLL.GetFacilityName(user.CompanyId);
            this.ViewBag.UserName = user.UserName;

            var dto = new DrugDto
            {
                Id = model.Id,
                Name = model.Name,
                Description = model.Description,
                Fee = model.Fee,
            };

            if (model.Id > 0) this._drugBLL.Update(dto, user.CompanyId);
            else this._drugBLL.Insert(dto, user.CompanyId);

            model.Drugs = this._drugBLL.Get(user.CompanyId).ToList();

            return this.View(model);
        }

        public ActionResult Doctor(string key)
        {
            var user = this.Session[SettingsManager.Constants.SessionUser] as UserData;
            if (user == null) return this.RedirectToAction("Login", "Login");
            this.ViewBag.FacilityName = this._facilityBLL.GetFacilityName(user.CompanyId);
            this.ViewBag.UserName = user.UserName;

            var model = new DoctorModel();
            model.Doctors = this._doctorBLL.Get(user.CompanyId, key).ToList();
            return this.View(model);
        }

        [HttpPost]
        public ActionResult Doctor(DoctorModel model)
        {
            var user = this.Session[SettingsManager.Constants.SessionUser] as UserData;
            if (user == null) return this.RedirectToAction("Login", "Login");
            this.ViewBag.FacilityName = this._facilityBLL.GetFacilityName(user.CompanyId);
            this.ViewBag.UserName = user.UserName;

            var dto = new DoctorDto
            {
                Address = model.Address,
                Birthday = model.Birthday,
                Email = model.Email,
                FirstName = model.FirstName,
                LastName = model.LastName,
                Phone = model.Phone,
                Id = model.Id,
                Sex = model.Sex
            };

            if (model.Id > 0) this._doctorBLL.Update(dto, user.CompanyId);
            else this._doctorBLL.Insert(dto, user.CompanyId);

            model.Doctors = this._doctorBLL.Get(user.CompanyId).ToList();

            return this.View(model);
        }
    }
}
