using BAL;
using Microsoft.AspNetCore.Mvc;

namespace BestPractices.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        IProductInterface _productService;
        public ProductController(IProductInterface productService)
        {
            _productService = productService;

        }
    }
}
