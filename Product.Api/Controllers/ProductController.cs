using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Product.Api.Mapper;
using Product.DataAccess;
using Product.DataTypes.Models;
using Product.Services.Services;

namespace Product.Api.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private ProductService productService { get; set; }

        private ILogger _logger { get; set; }

        private IMapper _mapper { get; set; }
        public ProductController(ProductContext context, IMapper mapper , ILogger<ProductController> logger)
        {
            productService = new ProductService(context);
            _mapper = mapper;
            _logger = logger;
        }


        [HttpGet]
        [ProducesResponseType(200,Type= typeof(List<ProductModel>))]
        public async Task<IActionResult> Get()
        {
            List<ProductModel> products = new List<ProductModel>();
            try
            {
                products = await this.productService.GetAllProductsAsync();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            _logger.LogInformation("query all products");
            return Ok(products);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(ProductModel))]
        public async Task<IActionResult> GetProductById( string id)
        {
            var product = await this.productService.GetProductById(id);

            if(product == null) 
            { 
                return NotFound();
            }

            return Ok(product);
        }

        [HttpPost]
        [ProducesResponseType(200)]
        public async Task<IActionResult> AddProduct([FromBody] ProductModelAddDto addDto)
        {
            var mapped = _mapper.Map<ProductModel>(addDto);

            var result = await this.productService.AddProductAsync(mapped);

            return Ok(result);
        }

        [HttpPut]
        [ProducesResponseType(200)]
        public async Task<IActionResult> EditProduct([FromBody] ProductModelEditDto editDto, [FromQuery]string id)
        {
            if(editDto.Id.ToString().ToLower() != id.ToLower())
            {
                return BadRequest();    
            }
            var product = await this.productService.GetProductById(id);

            if(product == null)
            {
                return NotFound();
            }

            var mapped = _mapper.Map<ProductModel>(editDto);
            
            var resultModified = await this.productService.EditProductAsync(mapped);

            if(resultModified == null)
            {
                return BadRequest(resultModified);
            }

            return Ok(resultModified);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteProduct([FromQuery] string id)
        {
            var product = await this.productService.GetProductById(id);

            if(product == null)
            {
                return NotFound();
            }

            var isDeleted = await this.productService.DeleteProductAsync(id);

            if (!isDeleted)
            {
                return BadRequest();
            }



            return Ok(id + " Deleted");

        }
    }
}
