using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace QRJ.Models
{
    public class HoroscopeSet
    {
        public Guid Id { get; set; }

        public int SetNumber { get; set; }

        public DateTime GeneratedOn { get; set; }

        public virtual List<HoroscopeQrCode> HoroscopeQrCodes { get; set; }
    }
}