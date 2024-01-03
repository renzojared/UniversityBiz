using Microsoft.EntityFrameworkCore;
using University.Database.Entities;
using University.Database.Map;

namespace University.Database;

public class Context : DbContext
{
    public DbSet<Faculty> Faculties { get; set; }
    public DbSet<Career> Careers { get; set; }

    public Context(DbContextOptions opt)
        : base(opt) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new FacultyConfiguration());
        modelBuilder.ApplyConfiguration(new CareerConfiguration());

        base.OnModelCreating(modelBuilder);
    }
}

