using IydeParfume.Database.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IydeParfume.Database.Configurations
{
    public class UsageTimeConfiguration : IEntityTypeConfiguration<UsageTime>
    {
        public void Configure(EntityTypeBuilder<UsageTime> builder)
        {
            builder
               .ToTable("UsageTimes");
        }
    }
}
