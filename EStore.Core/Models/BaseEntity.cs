﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EStore.Core.Models
{
    public class BaseEntity
    {
        public int Id { get; set; }
        //public bool IsDeleted { get; set; }
    }
}
