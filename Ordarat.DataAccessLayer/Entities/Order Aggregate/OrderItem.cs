using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordarat.DataAccessLayer.Entities.Order_Aggregate
{
    public class OrderItem:BaseEntity
    {
        public OrderItem()
        {

        }
        public OrderItem(ProductItemOrdered itemOrder, decimal price, int quantitiy)
        {
            ItemOrder = itemOrder;
            Price = price;
            Quantitiy = quantitiy;
        }

        public ProductItemOrdered ItemOrder { get; set; }
        public decimal Price { get; set; }
        public int Quantitiy { get; set; }
    }
}
