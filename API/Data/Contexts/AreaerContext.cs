using API.Models;
using Microsoft.EntityFrameworkCore;

namespace API.Data.Contexts
{
    public class AreaerContext : DbContext
    {
        public AreaerContext(DbContextOptions<AreaerContext> opt):base(opt)
        {

        }

        public DbSet<Area> Areas { get; set; }
    }
}
