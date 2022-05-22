using API.Data.Base;
using API.Data.Contexts;
using API.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace API.Data
{
    public class SqlSpecializationerRepo : ISpecializationerRepo
    {
        private SpecializationerContext _context;

        public SqlSpecializationerRepo(SpecializationerContext context)
        {
            _context = context;
        }

        public void CreateSpecialization(Specialization specialization)
        {
            if (specialization == null)
            {
                throw new ArgumentNullException(nameof(specialization));
            }

            _context.Specializations.Add(specialization);
        }

        public void DeleteSpecialization(Specialization specialization)
        {
            if (specialization == null)
            {
                throw new ArgumentNullException(nameof(specialization));
            }
            _context.Specializations.Remove(specialization);
        }

        public IEnumerable<Specialization> GetAllSpecializations()
        {
            return _context.Specializations.ToList();
        }

        public Specialization GetSpecializationById(string id)
        {
            return _context.Specializations.FirstOrDefault(p => p.Id == id);
        }

        public bool SaveChanges()
        {
            return (_context.SaveChanges()>=0);
        }

        public void UpdateSpecialization(Specialization specialization)
        {
            //
        }
    }
}
