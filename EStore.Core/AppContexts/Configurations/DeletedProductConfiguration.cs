using EStore.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EStore.Core.AppContexts.Configurations
{
    public class DeletedProductConfiguration : IEntityTypeConfiguration<DeletedProduct>
    {
        public void Configure(EntityTypeBuilder<DeletedProduct> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Id)
                .ValueGeneratedNever(); 
        }
    }
}
