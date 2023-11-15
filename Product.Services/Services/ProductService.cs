using Product.DataAccess;
using Product.DataAccess.Repositories;
using Product.DataTypes.Models;

namespace Product.Services.Services
{
    public class ProductService
    {
        private ProductRepository productRepository { get; set; }
        public ProductService(ProductContext context)
        {
            productRepository = new ProductRepository(context);
        }

        public async Task<List<ProductModel>> GetAllProductsAsync()
        {
            return await productRepository.GetAllProducts();
        }

        public async Task<ProductModel> GetProductById(string id)
        {
            return await productRepository.GetProductById(id);
        }

        public async Task<ProductModel> AddProductAsync(ProductModel product)
        {
            var result = await productRepository.AddProduct(product);

            return result;
        }

        public async Task<bool> DeleteProductAsync(string id)
        {
            var result = await productRepository.DeleteProduct(id);

            return result;
        }

        public async Task<ProductModel> EditProductAsync(ProductModel product)
        {
            var result = await productRepository.EditProduct(product);

            return result;
        }


        public async Task<ProductModel> AddProductRatingsAsync(ProductModel product)
        {
            var result = await productRepository.AddProductRatings(product);

            return result;
        }
    }
}