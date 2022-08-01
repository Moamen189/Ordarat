using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordarat.DataAccessLayer.Entities
{
    public class Employee : BaseEntity
    {
        public string Name { get; set; }

        public Department Department { get; set; }
    }
}
