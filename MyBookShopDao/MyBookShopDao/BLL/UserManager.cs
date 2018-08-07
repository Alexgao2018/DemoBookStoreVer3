using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyBookShopDao.Model;
using MyBookShopDao.DAL;

namespace MyBookShopDao.BLL
{
    public class UserManager
    {
        UserDAL userDal = new UserDAL();
        public User Login(string id, string pwd)
        {
            return userDal.CheckLogin(id,pwd);
        }
        //注册
        public void AddUser(User user)
        {
            user.UserRoleId = 1;
            userDal.Insert(user);
            userDal.context.SaveChanges();
        }
        //查询全部
        public List<User> GetAllUser()
        {
            return userDal.dbSet.ToList<User>();
        }
        //删除
        public void DeleteUser(int id)
        {
            userDal.Delete(id);
            userDal.context.SaveChanges();
        }
        //修改
        public void UpdateUser(User u)
        {
            userDal.context.Configuration.ValidateOnSaveEnabled = false;//关闭验证
            userDal.Update(u);
            userDal.context.SaveChanges();
            userDal.context.Configuration.ValidateOnSaveEnabled = true;
        }
        public User GetById(int id)
        {
            User u=new UserDAL().GetByID(id);
            return u;
        }
        public void GetUser(bool isUser,List<int> ids)
        {
            userDal.GetUser(isUser,ids);
            userDal.context.SaveChanges();
        }
    }
}
