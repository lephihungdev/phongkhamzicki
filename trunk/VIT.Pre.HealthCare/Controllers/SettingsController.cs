﻿namespace VIT.Pre.HealthCare.Controllers
{
    using System.Linq;
    using System.Web.Mvc;

    using VIT.BusinessLogicLayer.HealthCare;
    using VIT.DataTransferObject.HealthCare;
    using VIT.Library;
    using VIT.Library.Web.ObjectData;
    using VIT.Pre.HealthCare.Models;

    public class SettingsController : Controller
    {
        private readonly FacilityBLL _facilityBLL;
        private readonly DrugBLL _drugBLL;
        private readonly CptBLL _cptBLL;
        private readonly IcdBLL _icdBLL;
        private readonly DoctorBLL _doctorBLL;

        public SettingsController()
        {
            this._facilityBLL = new FacilityBLL();
            this._drugBLL = new DrugBLL();
            this._cptBLL = new CptBLL();
            this._icdBLL = new IcdBLL();
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
            else
            {
                this._drugBLL.Insert(dto, user.CompanyId);
                model.Id = dto.Id;
            }

            model.Drugs = this._drugBLL.Get(user.CompanyId).ToList();

            return this.View(model);
        }

        public ActionResult DrugDelete(int id)
        {
            var user = this.Session[SettingsManager.Constants.SessionUser] as UserData;
            if (user == null) return this.RedirectToAction("Login", "Login");

            this._drugBLL.Delete(id, user.CompanyId);
            return this.RedirectToAction("Drug", "Settings");
        }

        public ActionResult Cpt(string code)
        {
            var user = this.Session[SettingsManager.Constants.SessionUser] as UserData;
            if (user == null) return this.RedirectToAction("Login", "Login");
            this.ViewBag.FacilityName = this._facilityBLL.GetFacilityName(user.CompanyId);
            this.ViewBag.UserName = user.UserName;

            var model = new CptModel();
            model.Cpts = this._cptBLL.Get(user.CompanyId, code).ToList();
            return this.View(model);
        }

        [HttpPost]
        public ActionResult Cpt(CptModel model)
        {
            var user = this.Session[SettingsManager.Constants.SessionUser] as UserData;
            if (user == null) return this.RedirectToAction("Login", "Login");
            this.ViewBag.FacilityName = this._facilityBLL.GetFacilityName(user.CompanyId);
            this.ViewBag.UserName = user.UserName;

            var dto = new CptDto
            {
                Id = model.Id,
                Code = model.Code,
                Description = model.Description,
                Fee = model.Fee,
            };

            if (model.Id > 0) this._cptBLL.Update(dto, user.CompanyId);
            else
            {
                this._cptBLL.Insert(dto, user.CompanyId);
                model.Id = dto.Id;
            }

            model.Cpts = this._cptBLL.Get(user.CompanyId).ToList();

            return this.View(model);
        }

        public ActionResult CptDelete(int id)
        {
            var user = this.Session[SettingsManager.Constants.SessionUser] as UserData;
            if (user == null) return this.RedirectToAction("Login", "Login");

            this._cptBLL.Delete(id, user.CompanyId);
            return this.RedirectToAction("Cpt", "Settings");
        }

        public ActionResult Icd(string code)
        {
            var user = this.Session[SettingsManager.Constants.SessionUser] as UserData;
            if (user == null) return this.RedirectToAction("Login", "Login");
            this.ViewBag.FacilityName = this._facilityBLL.GetFacilityName(user.CompanyId);
            this.ViewBag.UserName = user.UserName;

            var model = new IcdModel();
            model.Icds = this._icdBLL.Get(code).ToList();
            return this.View(model);
        }

        [HttpPost]
        public ActionResult Icd(IcdModel model)
        {
            var user = this.Session[SettingsManager.Constants.SessionUser] as UserData;
            if (user == null) return this.RedirectToAction("Login", "Login");
            this.ViewBag.FacilityName = this._facilityBLL.GetFacilityName(user.CompanyId);
            this.ViewBag.UserName = user.UserName;

            var dto = new IcdDto
            {
                Id = model.Id,
                Code = model.Code,
                Description = model.Description
            };

            if (model.Id > 0) this._icdBLL.Update(dto);
            else
            {
                this._icdBLL.Insert(dto);
                model.Id = dto.Id;
            }

            model.Icds = this._icdBLL.Get().ToList();

            return this.View(model);
        }

        public ActionResult Doctor(string key)
        {
            var user = this.Session[SettingsManager.Constants.SessionUser] as UserData;
            if (user == null) return this.RedirectToAction("Login", "Login");
            this.ViewBag.FacilityName = this._facilityBLL.GetFacilityName(user.CompanyId);
            this.ViewBag.UserName = user.UserName;

            var model = new DoctorModel();
            var doctors = this._doctorBLL.Get(user.CompanyId).ToList();
            doctors.ForEach(e => e.SexName = e.Sex == true ? "Nam" : "Nữ");
            model.Doctors = doctors;
            model.Sexs = this._doctorBLL.GetSexs();

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
            else
            {
                this._doctorBLL.Insert(dto, user.CompanyId);
                model.Id = dto.Id;
            }

            var doctors = this._doctorBLL.Get(user.CompanyId).ToList();
            doctors.ForEach(e => e.SexName = e.Sex == true ? "Nam" : "Nữ");
            model.Doctors = doctors;
            model.Sexs = this._doctorBLL.GetSexs();

            return this.View(model);
        }

        public ActionResult DoctorDelete(int id)
        {
            var user = this.Session[SettingsManager.Constants.SessionUser] as UserData;
            if (user == null) return this.RedirectToAction("Login", "Login");

            this._doctorBLL.Delete(id, user.CompanyId);
            return this.RedirectToAction("Doctor", "Settings");
        }
    }
}
