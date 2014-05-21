namespace VIT.Pre.HealthCare.Controllers
{
    using System;
    using System.Web.Mvc;

    using VIT.BusinessLogicLayer.HealthCare;
    using VIT.Library;
    using VIT.Library.Web.ObjectData;

    public class LoginController : Controller
    {
         private readonly UserBLL _userBLL;

         public LoginController()
        {
            this._userBLL = new UserBLL();
        }

         public ActionResult Login()
         {
             return this.View();
         }

        [HttpPost]
        public ActionResult Login(string userName, string passWord)
        {
            try
            {
                var userInfo = this._userBLL.Login(userName, passWord);

                var userContext = new UserData();
                userContext.UserId = userInfo.UserId;
                userContext.CompanyId = userInfo.FacilityId;
                userContext.UserName = userInfo.UserName;
                userContext.Roles = userInfo.FullName;
                this.Session[SettingsManager.Constants.SessionUser] = userContext;

                //this.Response.Redirect("/Patient/Search");
                return this.RedirectToAction("Search", "Patient");
            }
            catch (Exception ex)
            {
                this.ViewBag.LoginErrorLabel = ex.Message;
            }

            return this.View();
        }

        public ActionResult Logout(string userName, string passWord)
        {
            this.Session[SettingsManager.Constants.SessionUser] = null;

            return this.RedirectToAction("Login", "Login");
        }
    }
}
