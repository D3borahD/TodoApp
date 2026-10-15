using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data;

public class ProjectTypeDaoConfiguration : IEntityTypeConfiguration<TypeDao>
{
    public void Configure(EntityTypeBuilder<TypeDao> builder)
    {
        builder.Property(p => p.ProjectTypeColor)
            .HasConversion<string>()
            .IsRequired();
    }
}