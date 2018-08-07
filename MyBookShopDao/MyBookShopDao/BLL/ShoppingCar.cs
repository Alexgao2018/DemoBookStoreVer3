using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyBookShopDao.Model;

namespace MyBookShopDao.BLL
{
    public class ShoppingCar
    {
        public List<CartItem> Items { get; set; }

        public ShoppingCar()
        {
            Items = new List<CartItem>();
        }
        //总金额
        public decimal Total
        {
            get
            {
                decimal sum = 0;
                foreach (var t in Items)
                {
                    sum += t.SubTotal;
                }
                return sum;
            }
        }

        //查找
        private CartItem FindById(int id)
        {
            foreach(var t in Items)
            {
                if (t.Id == id)
                {
                    return t;
                }
            }
            return null;
        }
        //添加商品
        public void AddToCart(int id)
        {
            CartItem item = FindById(id);
            if (item == null)
            {
                item = new CartItem();
                item.Id = id;
                Book m = new BookManager().GetById(id);
                item.Title = m.Title;
                item.Price = m.UnitPrice;
                item.Quantity = 1;
                Items.Add(item);
            }
            else
            {
                item.Quantity += 1;
            }
        }
        //删除
        public void Delete(int id)
        {
            CartItem item = FindById(id);
            if(item!=null)
            {
                Items.Remove(item);
            }

        }
        //修改
        public void Update(int id, int num)
        {
            CartItem item = FindById(id);
            if (item != null)
            {
                item.Quantity = num;
            }
        }
    }
}
