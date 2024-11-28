
using EStore.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EStore.Core.AppContexts.Configurations
{
    internal class AdminIdentifierConfiguration : IEntityTypeConfiguration<AdminIdentitfier>
    {
        public void Configure(EntityTypeBuilder<AdminIdentitfier> builder)
        {
            builder.HasOne(ai => ai.Admin).WithOne().IsRequired(false);
            builder.HasOne(ai=>ai.Manager).WithOne();
        }
    }
}
