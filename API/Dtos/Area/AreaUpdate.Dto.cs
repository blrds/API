﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace API.Dtos
{
    public class AreaUpdateDto
    {
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
    }
}
