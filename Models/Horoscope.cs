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

    public enum HoroscopeStyle
    {
        Adult = 0,
        Teen = 1
    }
}