using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace University.Database.Map;

public abstract class BaseEntityConfiguration<T> : IEntityTypeConfiguration<T> where T : class
{
    public virtual void Configure(EntityTypeBuilder<T> builder)
    {
        builder.Property("Id")
            .HasColumnName("id");

        builder.Property("State")
            .HasColumnName("estado")
            .HasDefaultValue(true);

        builder.Property("CreationDate")
            .IsRequired()
            .HasDefaultValueSql("getdate()")
            .HasColumnName("creado_tmstp");

        builder.Property("ModificationDate")
            .HasColumnName("actualizado_tmstp");
    }
}

