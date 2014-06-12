using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Xml;

namespace QRJ.Models
{
    public class Horoscope
    {
        public Guid Id { get; set; }

        public DateTime Date { get; set; }

        public string Data { get; set; }

        public string GetHoroscope(Sign sign)
        {
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(Data);

            return doc.SelectSingleNode("//item[title = \"" + sign.ToString() + "\"]/description").InnerText;
        }
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

    public enum HoroscopeStyle
    {
        Adult = 0,
        Teen = 1
    }
}