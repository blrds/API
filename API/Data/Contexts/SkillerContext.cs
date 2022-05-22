using API.Models;
using Microsoft.EntityFrameworkCore;

namespace API.Data.Contexts
{
    public class SkillerContext:DbContext
    {
        public SkillerContext(DbContextOptions<SkillerContext> opt):base(opt)
        {

        }

        public DbSet<Skill> Skills { get; set; }
    }
}
