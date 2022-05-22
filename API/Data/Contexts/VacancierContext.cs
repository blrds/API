using API.Models;
using Microsoft.EntityFrameworkCore;

namespace API.Data.Contexts
{
    public class VacancierContext:DbContext
    {
        public VacancierContext(DbContextOptions<VacancierContext> opt):base(opt)
        {

        }

        public DbSet<Vacancy> Vacancies { get; set; }
    }
}
