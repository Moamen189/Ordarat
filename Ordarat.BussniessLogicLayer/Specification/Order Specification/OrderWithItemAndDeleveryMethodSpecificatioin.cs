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
        public OrderWithItemAndDeleveryMethodSpecificatioin(string buyerEmail):base( O => O.BuyerEmail == buyerEmail )
        {
            AddInclude(O => O.Items);
            AddInclude(O => O.DelivaryMethod);
            AddOrderByDescending(O => O.OrderDate);
        }
    }
}
