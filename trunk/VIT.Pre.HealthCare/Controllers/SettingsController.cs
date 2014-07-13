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

        #region drug
        public ActionResult DrugComplete(string term)
        {
            var user = this.Session[SettingsManager.Constants.SessionUser] as UserData;
            if (user == null) return this.RedirectToAction("Login", "Login");
            this.ViewBag.FacilityName = this._facilityBLL.GetFacilityName(user.CompanyId);
            this.ViewBag.UserName = user.UserName;

            var query = this._drugBLL.Get(user.CompanyId)
                .Where(e => e.Active)
                .Where(c => c.Name.StartsWith(term) || c.Description.Contains(term))
                .Take(100)
                .OrderBy(e => e.Name);
            var icds = query.Select(e => new AutoCompletedIntDto
            {
                label = e.Name,
                value = e.Id
            });

            return this.Json(icds, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Drug(string code, int id = 0)
        {
            var user = this.Session[SettingsManager.Constants.SessionUser] as UserData;
            if (user == null) return this.RedirectToAction("Login", "Login");
            this.ViewBag.FacilityName = this._facilityBLL.GetFacilityName(user.CompanyId);
            this.ViewBag.UserName = user.UserName;

            var model = new DrugModel();
            if (id > 0)
            {
                var dto = this._drugBLL.Get(user.CompanyId).FirstOrDefault(e => e.Id == id);
                if (dto != null)
                {
                    model.Active = dto.Active;
                    model.Name = dto.Name;
                    model.Description = dto.Description;
                    model.Id = dto.Id;
                }
            }

            model.Drugs = this._drugBLL.Get(user.CompanyId, code).OrderBy(e => e.Name).ToList();
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
                Active = model.Active,
            };

            try
            {
                if (model.Id > 0) this._drugBLL.Update(dto, user.CompanyId);
                else
                {
                    this._drugBLL.Insert(dto, user.CompanyId);
                    model.Id = dto.Id;
                }
            }
            catch (Exception exception)
            {
                ViewBag.ErrorLabel = exception.Message;
            }

            model.Drugs = this._drugBLL.Get(user.CompanyId).OrderBy(e => e.Name).ToList();

            return this.View(model);
        }

        public ActionResult DrugDelete(int id)
        {
            var user = this.Session[SettingsManager.Constants.SessionUser] as UserData;
            if (user == null) return this.RedirectToAction("Login", "Login");

            this._drugBLL.Delete(id, user.CompanyId);
            return this.RedirectToAction("Drug", "Settings");
        }
        #endregion

        #region CPT
        public ActionResult CptComplete(string term)
        {
            var user = this.Session[SettingsManager.Constants.SessionUser] as UserData;
            if (user == null) return this.RedirectToAction("Login", "Login");
            this.ViewBag.FacilityName = this._facilityBLL.GetFacilityName(user.CompanyId);
            this.ViewBag.UserName = user.UserName;

            var query = this._cptBLL.Get(user.CompanyId)
                .Where(e => e.Active)
                .Where(c => c.Name.StartsWith(term) || c.Description.Contains(term))
                .Take(100)
                .OrderBy(e => e.Name);
            var icds = query.Select(e => new AutoCompletedIntDto
            {
                label = e.Name,
                value = e.Id
            });

            return this.Json(icds, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Cpt(string code, int id = 0)
        {
            var user = this.Session[SettingsManager.Constants.SessionUser] as UserData;
            if (user == null) return this.RedirectToAction("Login", "Login");
            this.ViewBag.FacilityName = this._facilityBLL.GetFacilityName(user.CompanyId);
            this.ViewBag.UserName = user.UserName;

            var model = new DrugModel();
            if (id > 0)
            {
                var dto = this._cptBLL.Get(user.CompanyId).FirstOrDefault(e => e.Id == id);
                if (dto != null)
                {
                    model.Active = dto.Active;
                    model.Name = dto.Name;
                    model.Description = dto.Description;
                    model.Id = dto.Id;
                }
            }

            model.Drugs = this._drugBLL.Get(user.CompanyId, code).OrderBy(e => e.Name).ToList();
            return this.View(model);
        }

        [HttpPost]
        public ActionResult Drug(CptModel model)
        {
            var user = this.Session[SettingsManager.Constants.SessionUser] as UserData;
            if (user == null) return this.RedirectToAction("Login", "Login");
            this.ViewBag.FacilityName = this._facilityBLL.GetFacilityName(user.CompanyId);
            this.ViewBag.UserName = user.UserName;

            var dto = new CptDto
            {
                Id = model.Id,
                Name = model.Name,
                Description = model.Description,
                Fee = model.Fee,
                Active = model.Active,
            };

            try
            {
                this._cptBLL.Save(dto, user.CompanyId);
                model.Id = dto.Id;
            }
            catch (Exception exception)
            {
                ViewBag.ErrorLabel = exception.Message;
            }

            model.Cpts = this._cptBLL.Get(user.CompanyId).OrderBy(e => e.Name).ToList();

            return this.View(model);
        }

        public ActionResult CptDelete(int id)
        {
            var user = this.Session[SettingsManager.Constants.SessionUser] as UserData;
            if (user == null) return this.RedirectToAction("Login", "Login");

            this._cptBLL.Delete(id, user.CompanyId);
            return this.RedirectToAction("Cpt", "Settings");
        }
        #endregion

        #region ICD
        public ActionResult IcdComplete(string term)
        {
            var user = this.Session[SettingsManager.Constants.SessionUser] as UserData;
            if (user == null) return this.RedirectToAction("Login", "Login");
            this.ViewBag.FacilityName = this._facilityBLL.GetFacilityName(user.CompanyId);
            this.ViewBag.UserName = user.UserName;

            var query = this._icdBLL.Get()
                .Where(e => e.Active)
                .Where(c => c.Code.StartsWith(term) || c.Description.Contains(term))
                .Take(100)
                .OrderBy(e => e.Code);
            var icds = query.Select(e => new AutoCompletedStringDto
            {
                label = e.Code + " - " + e.Description,
                value = e.Code
            });

            return this.Json(icds, JsonRequestBehavior.AllowGet);
        }

        public ActionResult IcdCompleteById(string value)
        {
            var user = this.Session[SettingsManager.Constants.SessionUser] as UserData;
            if (user == null) return this.RedirectToAction("Login", "Login");
            this.ViewBag.FacilityName = this._facilityBLL.GetFacilityName(user.CompanyId);
            this.ViewBag.UserName = user.UserName;

            var dto = new AutoCompletedStringDto();
            var data = this._icdBLL.Get().Select(e => new { e.Code, e.Description }).FirstOrDefault(c => c.Code == value);
            if (data != null)
            {
                dto.label = data.Code + " - " + data.Description;
                dto.value = data.Code;
            }

            return this.Json(dto, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Icd(string skey, string code)
        {
            var user = this.Session[SettingsManager.Constants.SessionUser] as UserData;
            if (user == null) return this.RedirectToAction("Login", "Login");
            this.ViewBag.FacilityName = this._facilityBLL.GetFacilityName(user.CompanyId);
            this.ViewBag.UserName = user.UserName;

            var model = new IcdModel();
            if (!string.IsNullOrEmpty(code))
            {
                var dto = this._icdBLL.Get().FirstOrDefault(e => e.Code == code);
                if (dto != null)
                {
                    model.Active = dto.Active;
                    model.Code = dto.Code;
                    model.Description = dto.Description;
                }
            }

            model.Icds = this._icdBLL.Get(skey).Take(100).OrderBy(e => e.Code).ToList();
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
                Code = model.Code,
                Description = model.Description,
                Active = model.Active
            };

            try
            {
                if (!string.IsNullOrEmpty(model.Code)) this._icdBLL.Save(dto);
            }
            catch (Exception exception)
            {
                ViewBag.ErrorLabel = exception.Message;
            }

            model.Icds = this._icdBLL.Get().OrderBy(e => e.Code).ToList();

            return this.View(model);
        }
        #endregion

        #region Doctor
        public ActionResult Doctor(string vkey, int id = 0)
        {
            var user = this.Session[SettingsManager.Constants.SessionUser] as UserData;
            if (user == null) return this.RedirectToAction("Login", "Login");
            this.ViewBag.FacilityName = this._facilityBLL.GetFacilityName(user.CompanyId);
            this.ViewBag.UserName = user.UserName;

            var model = new DoctorModel();
            if (id > 0)
            {
                var dto = this._doctorBLL.Get(user.CompanyId).FirstOrDefault(e => e.Id == id);
                if (dto != null)
                {
                    model.Address = dto.Address;
                    model.BirthYear = dto.BirthYear;
                    model.Email = dto.Email;
                    model.FirstName = dto.FirstName;
                    model.LastName = dto.LastName;
                    model.Phone = dto.Phone;
                    model.Id = dto.Id;
                    model.Sex = dto.Sex;
                    model.Active = dto.Active;
                }
            }

            var doctors = this._doctorBLL.Get(user.CompanyId).OrderBy(e => e.LastName).ToList();
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
                BirthYear = model.BirthYear,
                Email = model.Email,
                FirstName = model.FirstName,
                LastName = model.LastName,
                Phone = model.Phone,
                Id = model.Id,
                Sex = model.Sex,
                Active = model.Active
            };

            try
            {
                if (model.Id > 0) this._doctorBLL.Update(dto, user.CompanyId);
                else
                {
                    this._doctorBLL.Insert(dto, user.CompanyId);
                    model.Id = dto.Id;
                }
            }
            catch (Exception exception)
            {
                ViewBag.ErrorLabel = exception.Message;
            }

            var doctors = this._doctorBLL.Get(user.CompanyId).OrderBy(e => e.LastName).ToList();
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
        #endregion
    }
}
