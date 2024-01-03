using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using University.Database.Entities;

namespace University.Database.Map;

public class FacultyConfiguration : BaseEntityConfiguration<Faculty>
{
    public override void Configure(EntityTypeBuilder<Faculty> builder)
    {
        base.Configure(builder);

        builder.ToTable("facultad");

        builder.HasKey(s => s.Id);

        builder.HasMany(s => s.Careers).WithOne(s => s.Faculty);

        builder.Property(s => s.Name)
            .IsRequired()
            .HasColumnName("nombre_facultad")
            .HasMaxLength(50);

        builder.Property(s => s.Code)
            .IsRequired()
            .HasColumnName("codigo_facultad")
            .HasMaxLength(5);

        builder.HasData(
            new Faculty { Id = 1, Name = "Facultad de Ciencias", Code = "FC" },
            new Faculty { Id = 2, Name = "Facultad de Arquitectura", Code = "FAUA" },
            new Faculty { Id = 3, Name = "Facultad de Ingenieria Civil", Code = "FIC" },
            new Faculty { Id = 4, Name = "Facultad de Ingenieria Mecánica", Code = "FIM" });
    }
}

