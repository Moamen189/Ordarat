using Ordarat.DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordarat.BussniessLogicLayer.Specification
{
    public class EmployeeWithDepartmentSepcification:BaseSpecification<Employee>
    {
        public EmployeeWithDepartmentSepcification()
        {
            AddInclude(D => D.Department);
        }
    }
}
