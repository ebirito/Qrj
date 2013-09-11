using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace QRJ.Models
{
    public class QRCode
    {
        public Guid Id { get; set; }

        public string ActivationCode { get; set; }

        public string FilePath { get; set; }

        public DateTime GeneratedOn { get; set; }

        public Guid GeneratedBy { get; set; }

        public DateTime? ActivatedOn { get; set; }

        public Guid? ActivatedBy { get; set; }

        public string ProductName { get; set; }

        public UrlType UrlType { get; set; }

        public virtual List<Category> SuscribedCategories { get; set; }
    }
}