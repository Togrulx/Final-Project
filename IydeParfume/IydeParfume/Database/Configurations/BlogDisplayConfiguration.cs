using IydeParfume.Database.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IydeParfume.Database.Configuration
{
    public class BlogDisplayConfiguration : IEntityTypeConfiguration<BlogDisplay>
    {
        public void Configure(EntityTypeBuilder<BlogDisplay> builder)
        {
            builder
                .ToTable("BlogDisplays");
        }
    }
}
