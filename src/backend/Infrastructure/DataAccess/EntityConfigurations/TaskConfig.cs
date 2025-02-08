using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.DataAccess.EntityConfigurations;

public class TasksConfiguration : IEntityTypeConfiguration<Tasks>
{
    public void Configure(EntityTypeBuilder<Tasks> builder)
    {
        builder.HasKey(t => t.Id);
        
        builder.Property(t => t.Title)
            .IsRequired();
        
        builder.Property(t => t.Description)
            .IsRequired();
        
        builder.Property(t => t.Status)
            .IsRequired();
        
        builder.Property(t => t.Priority)
            .IsRequired();
        
        builder.Property(t => t.DeadLine)
            .IsRequired();
        
        builder.HasOne(t => t.User)
            .WithMany()
            .HasForeignKey(t => t.UserId)
            .OnDelete(DeleteBehavior.Restrict);
        
        builder.HasMany(t => t.Comments)
            .WithOne(c=>c.Task)
            .HasForeignKey(c=>c.TasksId)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.HasMany(t => t.TaskHistories)
            .WithOne(th => th.Task)
            .HasForeignKey(th => th.TasksId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}