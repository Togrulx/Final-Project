using IydeParfume.Database.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IydeParfume.Database.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        private int _idCounter = 1;
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder
               .ToTable("Users");

            //builder
            //  .HasData(
            //      new User
            //      {
            //          Id = ++_idCounter,
            //          FirstName = "admin",
            //          LastName = "admin",
            //          IsActive = true,
            //          Phone = "admin",
            //          Email = "admin@gmail.com",
            //          Password = "i8chAALXJeydJf7bhE/1F0yUdz",
            //          RoleId = 2,


            //      }) ;

            //builder
            //.HasOne(u => u.Basket)
            //  .WithOne(b => b.User)
            //    .HasForeignKey<Basket>(u => u.UserId);
        }
    }
}
