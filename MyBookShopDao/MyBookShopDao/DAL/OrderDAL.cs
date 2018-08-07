using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyBookShopDao.Model;
using System.Data.Entity;

namespace MyBookShopDao.DAL
{
    public class OrderDAL:DBHelper<Order>
    {
        public OrderDAL() { }
        //调用父类带参数构造函数
        public OrderDAL(DbContext db) : base(db) { }

        public List<Order> GetOrders(string start, string end, int IsDelivered, int orderid)
        {
            var result = from p in dbSet
                         select p;
            if (IsDelivered > 0)
            {
                result = from p in result
                         where p.IsDelivered == true
                         select p;
            }
            else
            {
                result = from p in result
                         where p.IsDelivered == false
                         select p;
            }
            if (orderid > 0)
            {
                result = from p in result
                         where p.Id == orderid
                         select p;
            }
            if (start != null && !start.Equals(""))
            {
                DateTime time = DateTime.Parse(start);
                result = from p in result
                         where p.OrderDate > time
                         select p;
            }
            if (end != null && !end.Equals(""))
            {
                DateTime time = DateTime.Parse(end);
                result = from p in result
                         where p.OrderDate < time
                         select p;
            }
            List<Order> list = result.ToList<Order>();
            return list;
        }
        public void Deliver(int id)
        {
            Order order = GetByID(id);
            order.IsDelivered = true;
        }
        public List<Order> GetByUid(int uid)
        {
            var result = from p in dbSet
                         where p.UserId==uid
                         select p;
            List<Order> list = result.ToList<Order>();
            return list;
        }
    }
}
