using LoginSystemBackEnd.Models;
using Microsoft.EntityFrameworkCore;

namespace LoginSystemBackEnd.Data
{
    public class IdentityContext:DbContext
    {
        public IdentityContext(DbContextOptions<IdentityContext> options): base(options)    
        {

        }
        public DbSet<User> Users { get; set; }
        //kullanılan güncel sınıf
        public DbSet<Users> LoginUsers { get; set; }
    }
}
