using API.Data.Base;
using API.Data.Contexts;
using API.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace API.Data
{
    public class SqlExperienceerRepo : IExperienceerRepo
    {
        private ExperienceerContext _context;

        public SqlExperienceerRepo(ExperienceerContext context)
        {
            _context = context;
        }

        public void CreateExperience(Experience experience)
        {
            if (experience == null)
            {
                throw new ArgumentNullException(nameof(experience));
            }

            _context.Experiences.Add(experience);
        }

        public void DeleteExperience(Experience experience)
        {
            if (experience == null)
            {
                throw new ArgumentNullException(nameof(experience));
            }
            _context.Experiences.Remove(experience);
        }

        public IEnumerable<Experience> GetAllExperiences()
        {
            return _context.Experiences.ToList();
        }

        public Experience GetExperienceById(string id)
        {
            return _context.Experiences.FirstOrDefault(p => p.Id == id);
        }

        public IEnumerable<Experience> GetExperiencesById(string id)
        {
            return _context.Experiences.Where(x => x.Id == id).ToList();
        }

        public IEnumerable<Experience> GetExperiencesByName(string name)
        {
            return _context.Experiences.Where(x => x.Name == name).ToList();
        }

        public bool SaveChanges()
        {
            return (_context.SaveChanges()>=0);
        }

        public void UpdateExperience(Experience experience)
        {
            //
        }
    }
}
