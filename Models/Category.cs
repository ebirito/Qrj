using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace QRJ.Models
{
    public class Category
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public Frequency Frequency { get; set; }

        public virtual List<CategoryContent> Contents { get; set; }
    }
}