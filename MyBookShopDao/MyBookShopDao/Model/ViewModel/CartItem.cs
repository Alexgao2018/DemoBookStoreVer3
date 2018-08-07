using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBookShopDao.Model
{
    //购物车产品项
    public class CartItem
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        //小计
        public decimal SubTotal
        {
            get
            {
                //价格乘以数量
                return Price * Quantity;
            }
        }
    }
}
