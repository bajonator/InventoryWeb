using InventoryWeb.Core;
using InventoryWeb.Core.Models.Domains;
using InventoryWeb.Core.Service;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace InventoryWeb.Persistence.Services
{
    public class UnitService : IUnitService
    {
        private readonly IUnitOfWork _unitOfWork;
        
        public UnitService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IEnumerable<Unit> GetUnit()
        {
            return _unitOfWork.Unit.GetUnit();
        }
        public Unit GetUnit(int id)
        {
            return _unitOfWork.Unit.GetUnit(id);
        }
        public IEnumerable<Unit> GetUnits()
        {
            return _unitOfWork.Unit.GetUnits();
        }

        public void Delete(int id)
        {
            _unitOfWork.Unit.Delete(id);
            _unitOfWork.Complete();
        }

        public void Add(Unit unit)
        {
            _unitOfWork.Unit.Add(unit);
            _unitOfWork.Complete();
        }
    }
}
