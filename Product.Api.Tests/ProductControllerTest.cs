using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Elfie.Diagnostics;
using Microsoft.Extensions.Logging;
using Moq;
using Product.Api.Controllers;
using Product.Api.Mapper;
using Product.DataAccess;
using Product.DataAccess.Repositories;
using Product.DataTypes.Models;
using Product.Services.Services;
using System.Net;
using System.Security.Policy;

namespace Product.Api.Tests
{
    public class ProductControllerTest
    {
        ProductController _productController;
        private Mock<IProductService> _mockProductService;

        //private Mock<IMapper> mockMapper;
        private static IMapper mapper;

        private static List<ProductModel> ProductsTest()
        {
            var products = new List<ProductModel>
            {
                new ProductModel() {
                    Id = new Guid(),
                    Maker = "test maker1",
                    img = "test img1",
                    Url = "test url1",
                    Title = "test Title1",
                    Description = "test decription1",
                    Ratings = new List<int> { }
                },
                  new ProductModel() {
                    Id = new Guid(),
                    Maker = "test maker2",
                    img = "test img2",
                    Url = "test url2",
                    Title = "test Title2",
                    Description = "test decription2",
                    Ratings = new List<int> { }
                },
                    new ProductModel() {
                    Id = new Guid(),
                    Maker = "test maker3",
                    img = "test img3",
                    Url = "test url3",
                    Title = "test Title3",
                    Description = "test decription3",
                    Ratings = new List<int> { }
                },
            };

            return products;
        }

        private static ProductModel ProductAddTest()
        {
            return new ProductModel()
            {
                Id = new Guid(),
                Maker = "new maker",
                img = "new img",
                Url = "new url",
                Title = "new Title",
                Description = "new decription",
            };
        }


        private static ProductModel ProductEditTest()
        {
            var editProduct = ProductsTest().First();
            editProduct.Maker = "modified maker";
            editProduct.Title = "modified title";

            return editProduct;
        }


        public ProductControllerTest()
        {
            _mockProductService = new Mock<IProductService>();

            var mockLogger = Mock.Of<ILogger<ProductController>>();
            //mockMapper = new Mock<IMapper>();

            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new AutoMapperProfile());
            });
            mapper = mockMapper.CreateMapper();

            _mockProductService.Setup(p => p.GetAllProductsAsync()).ReturnsAsync(ProductsTest());

            _mockProductService.Setup(p => p.GetProductById("werwe")).ReturnsAsync(ProductsTest().First());

            _mockProductService.Setup(p => p.AddProductAsync(It.IsAny<ProductModel>())).ReturnsAsync(ProductAddTest);

            _mockProductService.Setup(p => p.EditProductAsync(It.IsAny<ProductModel>())).ReturnsAsync(ProductEditTest);

            _productController = new ProductController(_mockProductService.Object, mapper, mockLogger);

        }

        [Fact]
        public async Task GetAllProduct_Test()
        {
            // Arrange

            // Act
            var result = await _productController.GetAllProduct();
            var resultType = result as OkObjectResult;
            var resultList = resultType.Value as List<ProductModel>;

            // Assert
            Assert.NotNull(result);
            Assert.IsType<List<ProductModel>>(resultType.Value);
            Assert.True(resultList.Count() == 3);
        }

        [Fact]
        public async Task GetProductById_Test()
        {
            // Arrange

            // Act
            var result = await _productController.GetProductById("werwe");
            var resultType = result as OkObjectResult;
            var resultList = resultType.Value as ProductModel;

            // Assert
            Assert.NotNull(result);
            Assert.IsType<ProductModel>(resultType.Value);
            Assert.True(resultList.Maker == "test maker1");
        }

        [Fact]
        public async Task AddProduct_Test()
        {
            // Arrange
            var addProductTest = mapper.Map<ProductModelAddDto>(ProductAddTest());

            // Act
            var result = await _productController.AddProduct(addProductTest);
            var resultType = result as OkObjectResult;
            var resultValue = resultType.Value as ProductModel;

            // Assert
            Assert.NotNull(result);
            Assert.IsType<ProductModel>(resultType.Value);
            Assert.Equal(ProductAddTest().Maker, resultValue.Maker);
            Assert.Equal(ProductAddTest().img, resultValue.img);
            Assert.Equal(ProductAddTest().Url, resultValue.Url);
            Assert.Equal(ProductAddTest().Title, resultValue.Title);
            Assert.Equal(ProductAddTest().Description, resultValue.Description);
        }


        [Fact]
        public async Task EditProduct_Test()
        {
            // Arrange
            var editProductTest = mapper.Map<ProductModelEditDto>(ProductsTest().First());
            editProductTest.Maker = "modified maker";
            editProductTest.Title = "modified title";

            // Act
            var result = await _productController.EditProduct(editProductTest, ProductEditTest().Id.ToString());
            var resultType = result as OkObjectResult;
            var resultValue = resultType.Value as ProductModel;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(ProductEditTest().Maker, editProductTest.Maker);
            Assert.Equal(ProductEditTest().Title, editProductTest.Title);

        }


        [Fact]
        public async Task DeleteProduct_Test()
        {
            // Arrange
            await _productController.DeleteProduct(ProductEditTest().Id.ToString());

            // Act

            // Assert

        }
    }
}