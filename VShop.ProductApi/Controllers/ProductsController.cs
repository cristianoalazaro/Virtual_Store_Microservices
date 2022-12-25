using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VShop.ProductApi.DTOs;
using VShop.ProductApi.Services;

namespace VShop.ProductApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductDTO>>> Get()
        {
            var productsDto = await _productService.GetProducts();

            if (productsDto is null)
                return NotFound("Products not found");

            return Ok(productsDto);
        }

        [HttpGet("{id:int}", Name = "GetProduct")]
        public async Task<ActionResult<ProductDTO>> getById(int id)
        {
            var productDto = await _productService.GetProductById(id);

            if (productDto is null)
                return NotFound("Product not found");

            return Ok(productDto);
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] ProductDTO productDTO)
        {
            if (productDTO is null)
                return BadRequest("Invalid Data");

            await _productService.AddProduct(productDTO);

            return new CreatedAtRouteResult("GetProduct", new { id = productDTO.Id }, productDTO);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Update(int id, [FromBody] ProductDTO productDTO)
        {
            if (productDTO is null || id != productDTO.Id)
                return BadRequest();

            await _productService.UpdateProduct(productDTO);
            return Ok(productDTO);
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<ProductDTO>> RemoveProduct(int id)
        {
            var productDto = await getById(id);

            if (productDto is null)
                return NotFound("Product not found");

            await _productService.RemoveProduct(id);
            return Ok(productDto);
        }
    }
}
