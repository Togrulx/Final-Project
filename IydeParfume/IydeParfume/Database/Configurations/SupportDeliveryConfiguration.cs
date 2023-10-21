using IydeParfume.Database.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IydeParfume.Database.Configuration
{
    public class SupportDeliveryConfiguration : IEntityTypeConfiguration<SupportDelivery>
    {
        public void Configure(EntityTypeBuilder<SupportDelivery> builder)
        {
            builder
                .ToTable("SupportDeliverys");
        }
    }
}
