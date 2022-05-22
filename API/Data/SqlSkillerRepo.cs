using API.Data.Base;
using API.Data.Contexts;
using API.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace API.Data
{
    public class SqlSkillerRepo : ISkillerRepo
    {
        private SkillerContext _context;

        public SqlSkillerRepo(SkillerContext context)
        {
            _context = context;
        }

        public void CreateSkill(Skill skill)
        {
            if (skill == null)
            {
                throw new ArgumentNullException(nameof(skill));
            }

            _context.Skills.Add(skill);
        }

        public void DeleteSkill(Skill skill)
        {
            if (skill == null)
            {
                throw new ArgumentNullException(nameof(skill));
            }
            _context.Skills.Remove(skill);
        }

        public IEnumerable<Skill> GetAllSkills()
        {
            return _context.Skills.ToList();
        }

        public Skill GetSkillById(int id)
        {
            return _context.Skills.FirstOrDefault(p => p.Id == id);
        }

        public bool SaveChanges()
        {
            return (_context.SaveChanges()>=0);
        }

        public void UpdateSkill(Skill skill)
        {
            //
        }
    }
}
