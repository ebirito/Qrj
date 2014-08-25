using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace QRJ.Models
{
    public class HoroscopeSign
    {
        public Guid Id { get; set; }

        public Sign Sign { get; set; }

        public virtual List<HoroscopeQrCode> HoroscopeQrCodes { get; set; }
    }

    public enum Sign
    {
        Aries = 0,
        Taurus = 1,
        Gemini = 2,
        Cancer = 3,
        Leo = 4,
        Virgo = 5,
        Libra = 6,
        Scorpio = 7,
        Sagittarius = 8,
        Capricorn = 9,
        Aquarius = 10,
        Pisces = 11
    }
}