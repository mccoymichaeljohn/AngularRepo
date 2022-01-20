using Microsoft.EntityFrameworkCore;

namespace AngularRepo.Domain;

public class ApplicationContext : DbContext
{
    public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
    {

    }
    public DbSet<Note> Notes { get; set; }
}
