using Ordarat.DataAccessLayer.Entities.Order_Aggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordarat.BussniessLogicLayer.Specification
{
    public class OrderWithItemByPaymentIntentSpecification:BaseSpecification<Order>
    {
        public OrderWithItemByPaymentIntentSpecification(string paymentIntentId):base(Order => Order.PaymentIntentId == paymentIntentId)
        {

        }
    }
}
