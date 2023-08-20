using InventoryWeb.Core.Models.Domains;
using Microsoft.EntityFrameworkCore;

namespace InventoryWeb.Core
{
    public interface IApplicationDbContext
    {
        DbSet<Product> Products { get; set; }
        DbSet<Inventory> Inventories { get; set; }
        DbSet<Unit> Units { get; set; }
        DbSet<ProductsBase> ProductsBases { get; set; }
        int SaveChanges();
    }
}
