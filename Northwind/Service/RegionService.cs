using Northwind.Models;
using Northwind.Respository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Northwind.Service
{
    public class RegionService : IRegionService
    {
        private readonly IRegionRepository _repository;

        public RegionService(IRegionRepository repository)
        {
            _repository = repository;
        }

        public RegionModel Add(RegionModel model)
        {
            return _repository.Add(model);
        }

        public RegionModel Delete(int id)
        {
            return _repository.Delete(id);
        }

        public IEnumerable<RegionModel> GetAll()
        {
            return _repository.GetAll();
        }

        public RegionModel GetDetail(int id)
        {
            return _repository.GetDetail(id);
        }

        public RegionModel Update(RegionModel model)
        {
            return _repository.Update(model);
        }
    }
    public interface IRegionService
    {
        IEnumerable<RegionModel> GetAll();
        RegionModel GetDetail(int id);
        RegionModel Add(RegionModel model);
        RegionModel Update(RegionModel model);
        RegionModel Delete(int id);
    }
}
