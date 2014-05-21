using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using VIT.Entity;

namespace VIT.DataAccessLayer
{
    public partial class UserAdminDAL
    {
        public UserAdmin GetUserAdminInclude(string userName)
        {
            var userAdmin = this.GetMany(c => c.UserInfo.UserAccount.UserName == userName)
                .Include(c => c.UserInfo)
                .Include(c => c.UserInfo.UserAccount)
                .Include(c => c.UserInfo.UserAdmin)
                .Include(c => c.UserInfo.UserMember)
                .FirstOrDefault();
            return userAdmin;
        }
    }

    public partial interface IUserAdminDAL
    {
        /// <summary>
        /// Get user admin include user info and etc...
        /// </summary>
        /// <param name="userName">User name</param>
        /// <returns></returns>
        UserAdmin GetUserAdminInclude(string userName);
    }
}
