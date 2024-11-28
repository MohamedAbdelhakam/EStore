using EStore.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EStore.Core.AppContexts.Configurations
{
    public class DeletedCategoryConfiguration : IEntityTypeConfiguration<DeletedCategory>
    {
        public void Configure(EntityTypeBuilder<DeletedCategory> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Id)
                .ValueGeneratedNever();
        }
    }
}
