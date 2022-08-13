using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordarat.DataAccessLayer.Entities.Order_Aggregate
{
    public class DelivaryMethod:BaseEntity
    {
        public DelivaryMethod()
        {

        }
        public DelivaryMethod(string shortName, string descrption, string delivaryTime, decimal cost)
        {
            ShortName = shortName;
            Descrption = descrption;
            DelivaryTime = delivaryTime;
            Cost = cost;
        }

        public string ShortName { get; set; }
        public string Descrption { get; set; }

        public string DelivaryTime { get; set; }

        public decimal Cost { get; set; }

    }
}
