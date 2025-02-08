using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.DataAccess.EntityConfigurations;

public class TaskHistoryConfiguration : IEntityTypeConfiguration<TaskHistory>
{
    public void Configure(EntityTypeBuilder<TaskHistory> builder)
    {
        builder.HasKey(th => th.Id);
        
        builder.Property(th => th.ChangeDescription)
            .IsRequired();
        
        builder.Property(th => th.ChangedAt)
            .IsRequired();
        
        builder.HasOne(th => th.Task)
            .WithMany(t => t.TaskHistories)
            .HasForeignKey(th => th.TasksId)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.HasOne(th => th.User)
            .WithMany()
            .HasForeignKey(th => th.UserId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}