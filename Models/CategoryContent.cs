using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace QRJ.Models
{
    public class CategoryContent
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        // Foreign key
        public Guid CategoryId { get; set; }

        public virtual Category Category { get; set; }

        public string FilePath { get; set; }

        public virtual List<QRCodeViewedCategoryContent> ViewedByProducts { get; set; }
    }
}