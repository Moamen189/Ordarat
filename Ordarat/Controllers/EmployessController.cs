using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Ordarat.BussniessLogicLayer.Interfaces;
using Ordarat.BussniessLogicLayer.Specification;
using Ordarat.DataAccessLayer.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Ordarat.Controllers
{
    
    public class EmployessController : BaseApiController
    {
        private readonly IGenericRepository<Employee> _employeesRepo;

        public EmployessController(IGenericRepository<Employee> employeesRepo)
        {
            _employeesRepo = employeesRepo;
        }


        [HttpGet]

        public async Task<ActionResult<IReadOnlyList<Employee>>> GetEmployees()
        {
            var spec = new EmployeeWithDepartmentSepcification();
            var Employess = await _employeesRepo.GetAllWithSpecAsync(spec);


            return Ok(Employess);
        }
    }
}
