﻿using EStore.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EStore.Core.AppContexts.Configurations
{
    public class CartConfiguration:IEntityTypeConfiguration<Cart>
    {
        public void Configure(EntityTypeBuilder<Cart> builder)
        {
            builder.HasOne(c => c.User).WithOne(u => u.Cart).OnDelete(DeleteBehavior.ClientCascade);
        }
    }
}