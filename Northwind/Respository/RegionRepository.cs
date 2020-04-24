using Mapster;
using Northwind.Database;
using Northwind.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Northwind.Respository
{
    public class RegionRepository : IRegionRepository
    {
        private readonly NorthwindContext _context;

        public RegionRepository(NorthwindContext context)
        {
            _context = context;
        }

        public RegionModel Add(RegionModel model)
        {
            var dbModel = model.Adapt<Database.Region>();
            _context.Region.Add(dbModel);
            _context.SaveChanges();
            return dbModel.Adapt<RegionModel>();
        }

        public RegionModel Delete(int id)
        {
            var dbModel = _context.Region.SingleOrDefault(x => x.RegionId == id);
            _context.Region.Remove(dbModel);
            _context.SaveChanges();
            return dbModel.Adapt<RegionModel>();
        }

        public IEnumerable<RegionModel> GetAll()
        {
            var regions = _context.Region.ToList();
            return regions.Adapt<IEnumerable<RegionModel>>();
        }

        public RegionModel GetDetail(int id)
        {
            var regions = _context.Region.SingleOrDefault(x => x.RegionId == id);
            return regions.Adapt<RegionModel>();
        }

        public RegionModel Update(RegionModel model)
        {
            var dbModel = _context.Region.SingleOrDefault(x => x.RegionId == model.RegionId);
            dbModel.RegionDescription = model.RegionDescription;
            _context.Region.Update(dbModel);
            _context.SaveChanges();
            return model;
        }
    }
    public interface IRegionRepository
    {
        IEnumerable<RegionModel> GetAll();
        RegionModel GetDetail(int id);
        RegionModel Add(RegionModel model);
        RegionModel Update(RegionModel model);
        RegionModel Delete(int id);
    }
}
