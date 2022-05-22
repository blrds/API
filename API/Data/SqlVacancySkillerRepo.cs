using API.Data.Base;
using API.Data.Contexts;
using API.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace API.Data
{
    public class SqlVacancySkillerRepo : IVacancySkillerRepo
    {
        private VacancySkillerContext _context;

        public SqlVacancySkillerRepo(VacancySkillerContext context)
        {
            _context = context;
        }

        public void CreateVacancySkill(VacancySkill vacancyskill)
        {
            if (vacancyskill == null)
            {
                throw new ArgumentNullException(nameof(vacancyskill));
            }

            _context.VacancySkills.Add(vacancyskill);
        }

        public void DeleteVacancySkill(VacancySkill vacancyskill)
        {
            if (vacancyskill == null)
            {
                throw new ArgumentNullException(nameof(vacancyskill));
            }
            _context.VacancySkills.Remove(vacancyskill);
        }

        public IEnumerable<VacancySkill> GetAllVacancySkills()
        {
            return _context.VacancySkills.ToList();
        }

        public VacancySkill GetVacancySkillById(int vid, int  sid)
        {
            return _context.VacancySkills.FirstOrDefault(p => p.IdVacancy == vid && p.IdSkill==sid);
        }

        public bool SaveChanges()
        {
            return (_context.SaveChanges()>=0);
        }

        public void UpdateVacancySkill(VacancySkill vacancyskill)
        {
            //
        }
    }
}
