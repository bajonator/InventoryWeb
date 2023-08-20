using InventoryWeb.Core.Models.Domains;
using InventoryWeb.Persistence;
using System.Collections.Generic;

namespace InventoryWeb.Core.Service
{
    public interface IUnitService
    {
        IEnumerable<Unit> GetUnit();
        Unit GetUnit(int id);
        IEnumerable<Unit> GetUnits();
        void Delete(int id);
        void Add(Unit unit);
    }
}
