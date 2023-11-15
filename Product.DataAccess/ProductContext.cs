using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Product.DataTypes;
using Product.DataTypes.Models;

namespace Product.DataAccess
{
    public class ProductContext : DbContext
    {
        public ProductContext() { 
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(@"Data Source=Product.db");
            
        }

        public DbSet<ProductModel> Products { get; set; }
    }
}