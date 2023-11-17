using Microsoft.EntityFrameworkCore;
using Product.DataTypes.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Product.DataAccess.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ProductContext _context;
        public ProductRepository(ProductContext context)
        {
            _context = context;
        }

        public async Task<List<ProductModel>> GetAllProducts()
        {
            return await _context.Products.ToListAsync();
        }

        public async Task<ProductModel> GetProductById(string id)
        {
            var product = await _context.Products.AsNoTracking().SingleOrDefaultAsync(x => x.Id.ToString().ToLower() == id.ToLower());
            return product;

        }

        public async Task<ProductModel> AddProduct(ProductModel product)
        {
            await _context.Products.AddAsync(product);

            await _context.SaveChangesAsync();

            return product;
        }

        public async Task<bool> DeleteProduct(string id)
        {
            var product = _context.Products.FirstOrDefault(prod => prod.Id.ToString().ToLower() == id.ToLower());


            if (product != null)
            {
                _context.Products.Remove(product);
                await _context.SaveChangesAsync();
                return true;
            }

            return false;
        }

        public async Task<ProductModel> EditProduct(ProductModel product)
        {
            try
            {

                _context.Entry(product).State = EntityState.Modified;

                await _context.SaveChangesAsync();

                return product;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<ProductModel> AddProductRatings(ProductModel product)
        {
            try
            {

                _context.Entry(product).State = EntityState.Modified;

                await _context.SaveChangesAsync();

                return product;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
