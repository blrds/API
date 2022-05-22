using API.Models;
using Microsoft.EntityFrameworkCore;

namespace API.Data.Contexts
{
    public class ExperienceerContext:DbContext
    {
        public ExperienceerContext(DbContextOptions<ExperienceerContext> opt):base(opt)
        {

        }

        public DbSet<Experience> Experiences { get; set; }
    }
}
