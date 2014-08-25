using System;
using System.ComponentModel.DataAnnotations;

namespace QRJ.Models
{
    public class HoroscopeQrCode
    {
        public Guid Id { get; set; }

        public Guid HoroscopeSignId { get; set; }

        public Guid HoroscopeSetId { get; set; }

        public string LongURL { get; set; }

        public string ShortURL { get; set; }
    }
}