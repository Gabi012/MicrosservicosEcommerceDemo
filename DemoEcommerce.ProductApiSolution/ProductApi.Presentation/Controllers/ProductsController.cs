using ECommerce.SharedLibrary.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using ProductApi.Application.DTOs;
using ProductApi.Application.DTOs.Convertions;
using ProductApi.Application.Interface;

namespace ProductApi.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class ProductsController(IProduct productInterface) : ControllerBase
    {
        //Primary Constructor (IProduct productInterface)
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductDTO>>> GetProducts()
        {
            var products = await productInterface.GetAllAsync();
            if (!products.Any())
            {
                return NotFound("No products found");
            }
            var (_, list) = ProductConversion.FromEntity(null, products);
            return list.Any() ? Ok(list) : NotFound("No products found.");
        }
        [HttpGet("{id:int}")]
        public async Task<ActionResult<ProductDTO>> GetProduct(int id)
        {
            var product = await productInterface.FindByIdAsync(id);
            if (product is null)
            {
                return NotFound("Product requested not found");
            }
            var (_product, _) = ProductConversion.FromEntity(product, null);
            return _product is not null ? Ok(product) : NotFound("No product found.");
        }
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<Response>> CreateProduct(ProductDTO product)
        {
            if (!ModelState.IsValid) { return BadRequest(ModelState); }
            var getEntity = ProductConversion.ToEntity(product);
            var response = await productInterface.CreateAsync(getEntity);
            return response.Flag is true ? Ok(response) : BadRequest(response);
        }
        [HttpPut]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<Response>> UpdateProduct(ProductDTO product)
        {
            if (!ModelState.IsValid) { return BadRequest(ModelState); }
            var getEntity = ProductConversion.ToEntity(product);
            var response = await productInterface.UpdateAsync(getEntity);
            return response.Flag is true ? Ok(response) : BadRequest(response);
        }
        [HttpDelete]
        public async Task<ActionResult<Response>> DeleteProduct(ProductDTO product)
        {
            var getEntity = ProductConversion.ToEntity(product); ;

            var response = await productInterface.DeleteAsync(getEntity);
            return response.Flag is true ? Ok(response) : BadRequest(response);

        }
        [HttpDelete("{id:int}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<Response>> DeleteProductById(int id)
        {
            var product = await productInterface.FindByIdAsync(id);
            if (product is null)
            {
                return NotFound("Product requested not found");
            }
            var response = await productInterface.DeleteAsync(product);
            return response.Flag is true ? Ok(response) : BadRequest(response);

        }
        //[HttpGet("get2")]
        //public ActionResult Get2()
        //{
        //    var list = new
        //    {
        //        t1 = 1,
        //        t2 = 2
        //    };
        //    return Ok(list);
        //}
    }
}
