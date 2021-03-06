﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eTicaret.DAL.Core
{
    public class SatisDetay
    {
        [Key]
        public int id { get; set; }
        [Required]
        public int adet { get; set; }
        [Required]
        public decimal birimfiyat { get; set; }
        [Required]
        public decimal tutar { get; set; }
        [Required]
        [DefaultValue(false)]
        public bool silindi { get; set; }

        [Required]
        public virtual Urun Urun { get; set; }
        [Required]
        public virtual Satis Satis { get; set; }
    }
}
