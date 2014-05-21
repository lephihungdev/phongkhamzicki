namespace VIT.BusinessLogicLayer.HealthCare
{
    using System;

    using VIT.DataAccessLayer.HealthCare;
    using VIT.DataTransferObject.HealthCare;
    using VIT.Library;

    public class UserBLL : BLLBase
    {
        private readonly IUserDAL _userDAL;

        public UserBLL(string connectionString = "")
            : base(connectionString)
        {
            this._userDAL = new UserDAL(this.DatabaseFactory);
        }

        /// <summary>
        /// ham kiem tra dang nhap
        /// </summary>
        /// <param name="userName">Ten dang nhap</param>
        /// <param name="passWord">mat khau</param>
        /// <returns>Tra ve thong tin user neu dang nhap thanh cong</returns>
        public UserLoginDto Login(string userName, string passWord)
        {
            var mem = this._userDAL.Get(o => o.UserName == userName);
            if (mem == null)
                throw new Exception("User không tồn tại");

            if (mem.Password != passWord.EnCodeMD5())
                throw new Exception("PAssword không đúng");

            var user = new UserLoginDto();
            user.UserId = mem.Id;
            user.UserName = mem.UserName;
            user.FullName = mem.FullName;
            user.FacilityId = mem.FacilityId;

            return user;
        }

        public void ChangePass(int userId, int facilityId, string newPass)
        {
            var mem = this._userDAL.Get(o => o.Id == userId && o.FacilityId == facilityId);
            if (mem != null)
            {
                mem.Password = newPass.EnCodeMD5();
                this._userDAL.Update(mem);

                this.SaveChanges();
            }
        }
    }
}
