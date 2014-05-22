namespace VIT.Pre.HealthCare.Controllers
{
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
        private readonly DrugBLL _drugBLL;
        private readonly CptBLL _cptBLL;
        private readonly IcdBLL _icdBLL;
        private readonly DoctorBLL _doctorBLL;
        private readonly ChargeBLL _chargeBLL;

        public ChargeController()
        {
            this._facilityBLL = new FacilityBLL();
            this._drugBLL = new DrugBLL();
            this._cptBLL = new CptBLL();
            this._icdBLL = new IcdBLL();
            this._doctorBLL = new DoctorBLL();
            this._chargeBLL = new ChargeBLL();
        }

        public ActionResult Index(int patientId, bool allfacility = false)
        {
            var user = this.Session[SettingsManager.Constants.SessionUser] as UserData;
            if (user == null) return this.RedirectToAction("Login", "Login");
            this.ViewBag.FacilityName = this._facilityBLL.GetFacilityName(user.CompanyId);
            this.ViewBag.UserName = user.UserName;

            var model = new ChargeModel();
            model.ListCpts = this._cptBLL.Get(user.CompanyId).ToList();
            model.ListCpts.Insert(0, new CptDto { Code = null, Description = "--- Chọn ---" });

            model.ListDoctors = this._doctorBLL.Get(user.CompanyId).ToList();
            model.ListDoctors.Insert(0, new DoctorDto { LastName = "--- Chọn ---" });

            model.ListDrugs = this._drugBLL.Get(user.CompanyId).ToList();
            model.ListDrugs.Insert(0, new DrugDto { Name = null, Description = "--- Chọn ---" });

            model.ListIcds = this._icdBLL.Get().ToList();
            model.ListIcds.Insert(0, new IcdDto { Code = null, Description = "--- Chọn ---" });

            var facility = 0;
            if (!allfacility) facility = user.CompanyId;
            
            var charges = this._chargeBLL.Get(patientId, facility).ToList();
            foreach (var charge in charges)
            {
                charge.CPTDescription = model.ListCpts.Where(e => e.Code == charge.CPTCode).Select(e => e.Description).FirstOrDefault();
                
                if(!string.IsNullOrEmpty(charge.ICDCode1))
                {
                    charge.Diagnostic += string.IsNullOrEmpty(charge.Diagnostic) ? string.Empty : ", ";
                    charge.Diagnostic += model.ListIcds.Where(e => e.Code == charge.ICDCode1).Select(e => e.Description).FirstOrDefault();
                }

                if (!string.IsNullOrEmpty(charge.ICDCode2))
                {
                    charge.Diagnostic += string.IsNullOrEmpty(charge.Diagnostic) ? string.Empty : ", ";
                    charge.Diagnostic += model.ListIcds.Where(e => e.Code == charge.ICDCode2).Select(e => e.Description).FirstOrDefault();
                }

                if (!string.IsNullOrEmpty(charge.ICDCode3))
                {
                    charge.Diagnostic += string.IsNullOrEmpty(charge.Diagnostic) ? string.Empty : ", ";
                    charge.Diagnostic += model.ListIcds.Where(e => e.Code == charge.ICDCode2).Select(e => e.Description).FirstOrDefault();
                }

                if (!string.IsNullOrEmpty(charge.ICDCode4))
                {
                    charge.Diagnostic += string.IsNullOrEmpty(charge.Diagnostic) ? string.Empty : ", ";
                    charge.Diagnostic += model.ListIcds.Where(e => e.Code == charge.ICDCode2).Select(e => e.Description).FirstOrDefault();
                }
            }
            model.ListCharges = charges;

            return View(model);
        }

        [HttpPost]
        public ActionResult Index(ChargeModel model, bool allfacility = false)
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
                DoctorId = model.DoctorId,
                Drugs = model.Drugs,
                ICDCode1 = model.ICDCode1,
                ICDCode2 = model.ICDCode2,
                ICDCode3 = model.ICDCode3,
                ICDCode4 = model.ICDCode4,
                Note = model.Note,
                Quality = model.Quality
            };

            model.ListCpts = this._cptBLL.Get(user.CompanyId).ToList();
            model.ListCpts.Insert(0, new CptDto { Code = null, Description = "--- Chọn ---" });

            model.ListDoctors = this._doctorBLL.Get(user.CompanyId).ToList();
            model.ListDoctors.Insert(0, new DoctorDto { LastName = "--- Chọn ---" });

            model.ListDrugs = this._drugBLL.Get(user.CompanyId).ToList();
            model.ListDrugs.Insert(0, new DrugDto { Name = null, Description = "--- Chọn ---" });

            model.ListIcds = this._icdBLL.Get().ToList();
            model.ListIcds.Insert(0, new IcdDto { Code = null, Description = "--- Chọn ---" });

            var facility = 0;
            if (!allfacility) facility = user.CompanyId;
            model.ListCharges = this._chargeBLL.Get(model.PatientId, facility).ToList();

            return View(model);
        }
    }
}
