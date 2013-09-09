using System;
using System.ComponentModel.DataAnnotations;

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
    }
}