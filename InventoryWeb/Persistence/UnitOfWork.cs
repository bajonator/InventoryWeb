using InventoryWeb.Core;
using InventoryWeb.Core.Repositories;
using InventoryWeb.Persistence.Repositories;

namespace InventoryWeb.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IApplicationDbContext _context;
        public UnitOfWork(IApplicationDbContext context)
        {
            _context = context;
            Inventory = new InventoryRepository(context);
            Unit = new UnitRepository(context);
            ProductBase = new ProductBaseRepository(context);
        }

        public IInventoryRepository Inventory { get; set; }
        public IUnitRepository Unit { get; set; }
        public IProductBaseRepository ProductBase { get; set; }

        public void Complete()
        {
            _context.SaveChanges();
        }
    }
}
