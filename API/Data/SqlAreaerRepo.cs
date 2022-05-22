using API.Data.Base;
using API.Data.Contexts;
using API.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace API.Data
{
    public class SqlAreaerRepo : IAreaerRepo
    {
        private AreaerContext _context;

        public SqlAreaerRepo(AreaerContext context)
        {
            _context = context;
        }

        public void CreateArea(Area area)
        {
            if (area == null)
            {
                throw new ArgumentNullException(nameof(area));
            }

            _context.Areas.Add(area);
        }

        public void DeleteArea(Area area)
        {
            if (area == null)
            {
                throw new ArgumentNullException(nameof(area));
            }
            _context.Areas.Remove(area);
        }

        public IEnumerable<Area> GetAllAreas()
        {
            return _context.Areas.ToList();
        }

        public Area GetAreaById(int id)
        {
            return _context.Areas.FirstOrDefault(p => p.Id == id);
        }

        public bool SaveChanges()
        {
            return (_context.SaveChanges()>=0);
        }

        public void UpdateArea(Area area)
        {
            //
        }
    }
}
