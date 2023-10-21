using IydeParfume.Database.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IydeParfume.Database.Configurations
{
    public class UserActivationConfiguration : IEntityTypeConfiguration<UserActivation>
    {
        public void Configure(EntityTypeBuilder<UserActivation> builder)
        {
            builder
               .ToTable("UserActivations");
        }
    }
}
