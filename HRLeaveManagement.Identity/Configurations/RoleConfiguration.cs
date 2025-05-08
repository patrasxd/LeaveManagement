using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HRLeaveManagement.Identity.Configurations;

public class RoleConfiguration : IEntityTypeConfiguration<IdentityRole>
{
    public void Configure(EntityTypeBuilder<IdentityRole> builder)
    {
        builder.HasData(
            new IdentityRole
            {
                Id = "d4f3b2c1-1a2d-4f8b-9c5e-7a2d3f4b0c3e",
                Name = "Administrator",
                NormalizedName = "ADMINISTRATOR"
            },
            new IdentityRole
            {
                Id = "cf4b0c3e-1a2d-4f8b-9c5e-7a2d3f4b0c3e",
                Name = "Employee",
                NormalizedName = "EMPLOYEE"
            }
        );
    }
}
