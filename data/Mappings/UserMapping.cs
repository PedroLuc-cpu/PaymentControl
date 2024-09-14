using cadastro.Database.Mappings;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PaymentControl.model;

namespace PaymentControl.data.Mappings
{
    public class UserMapping : BaseMap<User>
    {
        public UserMapping() : base("users")
        { }

        public override void Configure(EntityTypeBuilder<User> builder)
        {
            base.Configure(builder);
            builder.Property(x => x.Username).HasColumnName("Username").HasColumnType("varchar");
            builder.Property(x => x.Password).HasColumnName("password").HasColumnType("varchar");
            builder.Property(x => x.Email).HasColumnName("email").HasColumnType("varchar");
            builder.Property(x => x.Role).HasColumnName("role").HasColumnType("varchar");
        }
    }
}