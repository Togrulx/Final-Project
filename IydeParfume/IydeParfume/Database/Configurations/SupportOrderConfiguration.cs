using IydeParfume.Database.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IydeParfume.Database.Configuration
{
    public class SupportOrderConfiguration : IEntityTypeConfiguration<SupportOrder>
    {
        public void Configure(EntityTypeBuilder<SupportOrder> builder)
        {
            builder
                .ToTable("SupportOrders");
        }
    }
}
