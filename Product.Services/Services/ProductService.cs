using Product.DataAccess;
using Product.DataAccess.Repositories;
using Product.DataTypes.Models;

namespace Product.Services.Services
{
    public class ProductService : IProductService
    {
        private IProductRepository _productRepository { get; set; }
        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<List<ProductModel>> GetAllProductsAsync()
        {
            return await _productRepository.GetAllProducts();
        }

        public async Task<ProductModel> GetProductById(string id)
        {
            return await _productRepository.GetProductById(id);
        }

        public async Task<ProductModel> AddProductAsync(ProductModel product)
        {
            var result = await _productRepository.AddProduct(product);

            return result;
        }

        public async Task<bool> DeleteProductAsync(string id)
        {
            var result = await _productRepository.DeleteProduct(id);

            return result;
        }

        public async Task<ProductModel> EditProductAsync(ProductModel product)
        {
            var isExist = await _productRepository.GetProductById(product.Id.ToString());


            if (isExist == null)
            {
                return null;
            }

            var result = await _productRepository.EditProduct(product);

            return result;
        }


        public async Task<ProductModel> AddProductRatingsAsync(ProductModel product)
        {
            var result = await _productRepository.AddProductRatings(product);

            return result;
        }
    }
}