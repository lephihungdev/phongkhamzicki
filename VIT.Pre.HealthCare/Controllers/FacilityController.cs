namespace VIT.Pre.HealthCare.Controllers
{
    using System.Web.Mvc;

    using VIT.BusinessLogicLayer.HealthCare;
    using VIT.Pre.HealthCare.Models;

    public class FacilityController : Controller
    {
         private readonly UserBLL _userBLL;

         public FacilityController()
        {
            this._userBLL = new UserBLL();
        }

         public ActionResult Intro()
         {
             return this.View();
         }

        [HttpPost]
         public ActionResult Intro(FacilityModel model)
        {

            return this.View(model);
        }
    }
}
