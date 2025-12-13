using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Namotion.Reflection;
using BackEnd.Services.Core.Api.Domain.Common;

namespace BackEnd.Services.Core.Api.Configuration
{

    //this class sync database configuration for AssetMapping entity

    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users");
            builder.HasIndex(p => p.Id).IsUnique();
            builder.HasKey(p => p.SequentialId);
            builder.Property(p => p.SequentialId).ValueGeneratedOnAdd();
           
        }
    }
}