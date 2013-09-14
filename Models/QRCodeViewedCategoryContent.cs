using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace QRJ.Models
{
    public class QRCodeViewedCategoryContent
    {
        [Key, Column(Order = 0)]
        public Guid QrCodeId { get; set; }
        [Key, Column(Order = 1)]
        public Guid CategoryContentId { get; set; }

        public virtual QRCode QrCode { get; set; }
        public virtual CategoryContent CategoryContent { get; set; }

        public DateTime LastViewedOn { get; set; }
        public DateTime ExpiresOn { get; set; }
    }
}