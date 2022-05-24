using API.Models;
using Microsoft.EntityFrameworkCore;

namespace API.Data.Contexts
{
    public class VacancySkillerContext:DbContext
    {
        public VacancySkillerContext(DbContextOptions<VacancySkillerContext> opt):base(opt)
        {

        }
        
        public DbSet<VacancySkill> VacancySkills { get; set; }
    }
}
