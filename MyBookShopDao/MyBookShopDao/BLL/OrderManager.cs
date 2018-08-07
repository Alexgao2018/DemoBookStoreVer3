using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyBookShopDao.Model;
using MyBookShopDao.DAL;

namespace MyBookShopDao.BLL
{
    public class OrderManager
    {
        //插入订单
        public void AddOrderDetail(Order order, List<OrderDetail> list)
        {
            using(MyBookShopEntities db = new MyBookShopEntities())
            {
                OrderDAL orderDal = new OrderDAL(db);
                OrderDetailDAL odDal = new OrderDetailDAL(db);

                orderDal.Insert(order);

                foreach (var o in list)
                {
                    o.OrderID = order.Id;
                    odDal.Insert(o);
                }
                db.SaveChanges();
            }
        }
       
        public List<Order> GetOrders(string start, string end,int IsDelivered, int orderid)
        {
            return new OrderDAL().GetOrders(start, end, IsDelivered, orderid);
        }
        //根据id查询
        public Order GetById(int id)
        {
            return new OrderDAL().GetByID(id);
        }
        //发货
        public void Deliver(int id)
        {
            OrderDAL orderDal = new OrderDAL();
            orderDal.Deliver(id);
            orderDal.context.SaveChanges();
        }
        public List<Order> GetByUid(int uid)
        {
            return new OrderDAL().GetByUid(uid);
        }
    }
}
