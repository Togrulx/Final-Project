using IydeParfume.Database.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IydeParfume.Database.Configurations
{
    public class NavbarConfiguration : IEntityTypeConfiguration<Navbar>
    {
        public void Configure(EntityTypeBuilder<Navbar> builder)
        {
            builder
               .ToTable("Navbars");
        }
    }
}
