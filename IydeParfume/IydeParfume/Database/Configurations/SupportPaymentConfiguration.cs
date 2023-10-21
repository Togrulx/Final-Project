using IydeParfume.Database.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IydeParfume.Database.Configuration
{
    public class SupportPaymentConfiguration : IEntityTypeConfiguration<SupportPayment>
    {
        public void Configure(EntityTypeBuilder<SupportPayment> builder)
        {
            builder
                .ToTable("SupportPayments");
        }
    }
}
