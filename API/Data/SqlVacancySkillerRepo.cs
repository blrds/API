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

        public void CreateVacancySkill(VacancySkill vacancySkill)
        {
            if (vacancySkill == null)
            {
                throw new ArgumentNullException(nameof(vacancySkill));
            }

            _context.VacancySkills.Add(vacancySkill);
        }

        public void DeleteVacancySkill(VacancySkill vacancySkill)
        {
            if (vacancySkill == null)
            {
                throw new ArgumentNullException(nameof(vacancySkill));
            }
            _context.VacancySkills.Remove(vacancySkill);
        }

        public IEnumerable<VacancySkill> GetAllVacancySkills()
        {
            return _context.VacancySkills.ToList();
        }

        public IEnumerable<VacancySkill> GetVacancySkillsByIdSkill(string sid)
        {
            return _context.VacancySkills.Where(x => x.IdSkill.ToString() == sid).ToList();
        }

        public IEnumerable<VacancySkill> GetVacancySkillsByIdVacancy(string vid)
        {
            return _context.VacancySkills.Where(x => x.IdVacancy.ToString() == vid).ToList();
        }

        public IEnumerable<VacancySkill> GetVacancySkillsByIds(string vid, string sid)
        {
            return _context.VacancySkills.Where(x => x.IdVacancy.ToString() == vid && x.IdSkill.ToString()==sid).ToList();
        }

        public bool SaveChanges()
        {
            return (_context.SaveChanges()>=0);
        }

        public void UpdateVacancySkill(VacancySkill vacancySkill)
        {
            //
        }
    }
}
