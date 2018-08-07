using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyBookShopDao.Model;

namespace MyBookShopDao.DAL
{
    public class UserDAL:DBHelper<User>
    {
        public User CheckLogin(string loginid, string loginpwd)
        {
            var user = (from u in dbSet
                        where u.LoginId == loginid
                        && u.LoginPwd == loginpwd
                        select u).SingleOrDefault();
            return user;
        }

        public void GetUser(bool isUser,List<int> ids)
        {
            var result = from p in dbSet
                         where ids.Contains(p.Id)
                         select p;
            foreach (var u in result)
            {
                u.UserState = isUser;
            }
        }
    }

}
