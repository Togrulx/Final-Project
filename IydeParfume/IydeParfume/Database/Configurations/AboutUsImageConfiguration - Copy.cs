using IydeParfume.Database.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IydeParfume.Database.Configuration
{
    public class AboutUsImageConfiguration : IEntityTypeConfiguration<AboutUsImage>
    {
        public void Configure(EntityTypeBuilder<AboutUsImage> builder)
        {
            builder
                .ToTable("AboutUsImages");
        }
    }
}
