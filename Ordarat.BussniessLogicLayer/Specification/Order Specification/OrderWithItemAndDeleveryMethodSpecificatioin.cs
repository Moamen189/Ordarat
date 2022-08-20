using Ordarat.DataAccessLayer.Entities.Order_Aggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordarat.BussniessLogicLayer.Specification.Order_Specification
{
    public class OrderWithItemAndDeleveryMethodSpecificatioin:BaseSpecification<Order>
    {

        //this ctor is Used for get all orders for a specific user
        public OrderWithItemAndDeleveryMethodSpecificatioin(string buyerEmail):base( O => O.BuyerEmail == buyerEmail )
        {
            AddInclude(O => O.Items);
            AddInclude(O => O.DelivaryMethod);
            AddOrderByDescending(O => O.OrderDate);
        }

        //this ctor is Used for get an order for a specific user
        public OrderWithItemAndDeleveryMethodSpecificatioin(int orderId, string buyerEmail) : base(O => O.BuyerEmail == buyerEmail && O.Id == orderId)
        {
            AddInclude(O => O.Items);
            AddInclude(O => O.DelivaryMethod);
            
        }
    }
}
