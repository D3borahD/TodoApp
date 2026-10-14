using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data;

public class ProjectDaoConfiguration : IEntityTypeConfiguration<ProjectDao>
{
    public void Configure(EntityTypeBuilder<ProjectDao> builder)
    {
        builder.ToTable("Projects");
        
        builder.HasKey(p => p.Id);
        
        builder.Property(p => p.Label)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(p => p.Status)
            .HasConversion<string>()
            .IsRequired();

        builder.HasMany(p => p.StepList)
            .WithOne()
            .HasForeignKey(s => s.ProjectId)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.HasMany(p => p.ProjectTypes)
            .WithMany(t => t.Projects)
            .UsingEntity<Dictionary<string, object>>(
                "ProjectProjectTypes",
                right => right
                    .HasOne<TypeDao>()
                    .WithMany()
                    .HasForeignKey("ProjectTypeId"),
                left => left
                    .HasOne<ProjectDao>()
                    .WithMany()
                    .HasForeignKey("ProjectId"),
                joinEntity =>
                {
                    joinEntity.ToTable("ProjectProjectTypes");
                    joinEntity.HasKey("ProjectId", "ProjectTypeId");
                });
    }
}