using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Product.Api.Controllers;
using Product.DataAccess;
using Product.DataAccess.Repositories;
using Product.DataTypes.Models;
using Product.Services.Services;
using System.Net;

namespace Product.Api.Tests
{
    public class ProductControllerTest
    {
        ProductController _productController;
        IProductService _productService;
        IProductRepository _productRepository;

        public ProductControllerTest(ProductContext productContext, IMapper mapper, ILogger<ProductController> logger)
        {
            _productRepository = new ProductRepository(productContext);
            _productService = new ProductService(_productRepository);
            _productController = new ProductController(_productService, mapper, logger);
        }

        [Fact]
        public async Task GetApiEndPoint_ReturnsSuccess()
        {
            // Arrange


            // Act
            var result = _productController.Get();
            var resultType = result.Result as OkObjectResult;
            var resultList = resultType.Value as List<ProductModel>;

            // Assert
            Assert.NotNull(result);
            Assert.IsType<List<ProductModel>>(resultType.Value);

        }
    }
}