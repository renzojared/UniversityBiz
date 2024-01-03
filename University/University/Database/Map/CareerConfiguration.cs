using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using University.Database.Entities;

namespace University.Database.Map;

public class CareerConfiguration : BaseEntityConfiguration<Career>
{
    public override void Configure(EntityTypeBuilder<Career> builder)
    {
        base.Configure(builder);

        builder.ToTable("carrera");

        builder.HasKey(s => s.Id);

        builder.Property(s => s.FacultyId)
            .HasColumnName("facultad_id");

        builder.HasOne(s => s.Faculty)
            .WithMany(s => s.Careers);

        builder.Property(s => s.Name)
            .IsRequired()
            .HasColumnName("nombre_carrera")
            .HasMaxLength(50);

        builder.Property(s => s.Code)
            .IsRequired()
            .HasColumnName("codigo_carrera")
            .HasMaxLength(5);

        builder.HasData(
            new Career { Id = 1, Code = "EF", Name = "Escuela de Física", FacultyId = 1 },
            new Career { Id = 2, Code = "EM", Name = "Escuela de Matemática", FacultyId = 1 },
            new Career { Id = 3, Code = "EA", Name = "Escuela de Arquitectura", FacultyId = 2 });
    }
}

