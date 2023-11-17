using Product.DataTypes.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Product.Services.Services
{
    public interface IProductService
    {
        Task<List<ProductModel>> GetAllProductsAsync();
        Task<ProductModel> GetProductById(string id);
        Task<ProductModel> AddProductAsync(ProductModel product);
        Task<bool> DeleteProductAsync(string id);
        Task<ProductModel> EditProductAsync(ProductModel product);
        Task<ProductModel> AddProductRatingsAsync(ProductModel product);
    }
}
