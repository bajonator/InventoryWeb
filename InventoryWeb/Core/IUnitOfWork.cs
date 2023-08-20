using InventoryWeb.Core.Repositories;
using InventoryWeb.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;

namespace InventoryWeb.Core
{
    public interface IUnitOfWork
    {
        IInventoryRepository Inventory { get; }
        IUnitRepository Unit { get; }
        IProductBaseRepository ProductBase { get; }

        void Complete();
       
    }
}
