using InventoryWeb.Core.Models.Domains;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace InventoryWeb.Core.Repositories
{
    public interface IUnitRepository
    {
        IEnumerable<Unit> GetUnit();
        IEnumerable<Unit> GetUnits();
        void Delete(int id);
        void Add(Unit unit);
        Unit GetUnit(int id);
    }
}
