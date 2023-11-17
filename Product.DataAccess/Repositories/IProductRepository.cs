using Product.DataTypes.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Product.DataAccess.Repositories
{
    public interface IProductRepository
    {
        Task<List<ProductModel>> GetAllProducts();
        Task<ProductModel> GetProductById(string id);
        Task<ProductModel> AddProduct(ProductModel product);
        Task<bool> DeleteProduct(string id);
        Task<ProductModel> EditProduct(ProductModel product);

        Task<ProductModel> AddProductRatings(ProductModel product);
    }
}
