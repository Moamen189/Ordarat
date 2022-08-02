using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Ordarat.BussniessLogicLayer.Interfaces;
using Ordarat.DataAccessLayer;
using Ordarat.DataAccessLayer.Entities;
using Ordarat.Errors;
using System.Threading.Tasks;

namespace Ordarat.Controllers
{
   
    public class BuggyController : BaseApiController
    {
        //private readonly IGenericRepository<Product> _productRepo;
        private readonly StroreContext _context;

        public BuggyController(StroreContext context)
        {
            
            _context = context;
        }

        [HttpGet("notfound")]

        public  ActionResult GetNotFoundRequest()
        {
            var product = _context.Product.Find(42);
            if(product == null)
                return NotFound(new ApiResponse(404));
            return Ok();
        }


        [HttpGet("servererror")]

        public ActionResult GetServerError()
        {
            var product = _context.Product.Find(42);
            var productToReturn = product.ToString();
            return Ok();  
        }

        [HttpGet("badrequest")]

        public ActionResult GetBadRequest()
        {
         
            return BadRequest(new ApiResponse(400));
        }

        [HttpGet("badrequest/{id}")]

        public ActionResult GetBadRequest(int id)
        {

            return Ok();
        }
    }
}
