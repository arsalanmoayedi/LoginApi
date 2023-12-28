
using Microsoft.EntityFrameworkCore;
using UserTrain.Model;

namespace UserTrain.Data
{
    public class Context:DbContext
    {
        public Context(DbContextOptions<Context> options):base(options)
        {
                
        }
        public DbSet<User> users { get; set; }
    }
}
