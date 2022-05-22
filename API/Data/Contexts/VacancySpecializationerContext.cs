using API.Models;
using Microsoft.EntityFrameworkCore;

namespace API.Data.Contexts
{
    public class VacancySpecializationerContext:DbContext
    {
        public VacancySpecializationerContext(DbContextOptions<VacancySpecializationerContext> opt):base(opt)
        {

        }

        public DbSet<VacancySpecialization> VacancySpecializations { get; set; }
    }
}
