using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HRLeaveManagement.Identity.Configurations;

public class UserRoleConfiguration : IEntityTypeConfiguration<IdentityUserRole<string>>
{
    public void Configure(EntityTypeBuilder<IdentityUserRole<string>> builder)
    {
        builder.HasData(
            new IdentityUserRole<string>
            {
                UserId = "1a2b3c4d-5e6f-7g8h-9i0j-k1l2m3n4o5p6",
                RoleId = "d4f3b2c1-1a2d-4f8b-9c5e-7a2d3f4b0c3e"
            },
            new IdentityUserRole<string>
            {
                UserId = "2b3c4d5e-6f7g-8h9i-0j1k-2l3m4n5o6p7q",
                RoleId = "cf4b0c3e-1a2d-4f8b-9c5e-7a2d3f4b0c3e"
            }
        );
    }
}
