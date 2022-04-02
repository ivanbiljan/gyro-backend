using Gyro.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gyro.Infrastructure.Persistence.Configurations
{
    public sealed class IssueConfiguration : IEntityTypeConfiguration<Issue>
    {
        public void Configure(EntityTypeBuilder<Issue> builder)
        {
            builder.HasOne(i => i.Reporter)
                .WithMany(u => u.ReportedIssues);

            builder.HasOne(i => i.Assignee)
                .WithMany(u => u.AssignedIssues);
        }
    }
}