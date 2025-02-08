using System.Runtime.InteropServices;
using Domain.Entities;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.DataAccess.EntityConfigurations;

public sealed class CommentConfig : IEntityTypeConfiguration<Comment>
{
    public void Configure(EntityTypeBuilder<Comment> builder)
    {
        builder.ToTable("Comments");

        builder.HasKey(c => c.Id);

        builder.Property(c => c.Content)
            .IsRequired()
            .HasMaxLength(200); 
        

        builder.HasOne(c => c.Task)
            .WithMany(t => t.Comments) 
            .HasForeignKey(c => c.TasksId) 
            .OnDelete(DeleteBehavior.Cascade); 

        
        builder.HasOne(c => c.User)
            .WithMany(u => u.Comments) 
            .HasForeignKey(c => c.UserId) 
            .OnDelete(DeleteBehavior.Restrict); 
    }
}