
using AuthenticationApi.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace AuthenticationApi.Infraestructure.Data
{
    public class AutheticationDbContext(DbContextOptions<AutheticationDbContext> options): DbContext(options)
    {
        public DbSet<AppUser> Users { get; set; }
    }
}
