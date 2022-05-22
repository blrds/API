using API.Models;
using Microsoft.EntityFrameworkCore;

namespace API.Data.Contexts
{
    public class SpecializationerContext:DbContext
    {
        public SpecializationerContext(DbContextOptions<SpecializationerContext> opt):base(opt)
        {

        }

        public DbSet<Specialization> Specializations { get; set; }
    }
}
