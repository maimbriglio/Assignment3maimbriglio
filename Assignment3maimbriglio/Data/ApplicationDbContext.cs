using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Assignment3maimbriglio.Models;

namespace Assignment3maimbriglio.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Assignment3maimbriglio.Models.Actor>? Actor { get; set; }
        public DbSet<Assignment3maimbriglio.Models.Movie>? Movie { get; set; }
        public DbSet<Assignment3maimbriglio.Models.ActorMovie>? ActorMovie { get; set; }
    }
}