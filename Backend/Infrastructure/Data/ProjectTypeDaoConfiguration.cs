using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data;

public class ProjectTypeDaoConfiguration : IEntityTypeConfiguration<ProjectTypeDao>
{
    public void Configure(EntityTypeBuilder<ProjectTypeDao> builder)
    {
        builder.Property(p => p.ProjectTypeColor)
            .HasConversion<string>()
            .IsRequired();
    }
}