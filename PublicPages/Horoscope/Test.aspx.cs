using System;
using System.Collections.Generic;
using System.Linq;
using QRJ.Models;
using System.Net;
using System.IO;

namespace QRJ.PublicPages.Horoscope
{
    public partial class Test : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Guid signId = new Guid(Request.QueryString["id"]);
            QRJ.Models.Sign sign = GetSign(signId);

            // Get easter date/time
            TimeZoneInfo easternZone = TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time");
            DateTime today = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, easternZone).Date;

            // Check if there is a horoscope for this date
            QRCodeContext db = new QRCodeContext();
            QRJ.Models.Horoscope horoscope = db.Horoscopes.Where(h => h.Date == today).FirstOrDefault();
            // If we have the horoscope then just display it
            if (horoscope == null)
            {
                // Otherwise get it from the service
                WebRequest request = WebRequest.Create("http://horoscopeservices.co.uk/daily_delivery/xmlaccess.asp?uid=220104397&date=" + today.ToString("yyyy-MM-dd"));
                WebResponse response = request.GetResponse();
                // Get the stream containing content returned by the server.
                Stream dataStream = response.GetResponseStream();
                // Open the stream using a StreamReader for easy access.
                StreamReader reader = new StreamReader(dataStream);
                // Read the content.
                string responseFromServer = reader.ReadToEnd();

                // Save the horoscope in the db
                horoscope = new Models.Horoscope { Id = Guid.NewGuid(), Date = today, Data = responseFromServer };
                db.Horoscopes.Add(horoscope);
                db.SaveChanges();
            }

            txtSign.InnerText = sign.ToString();
            txtHoroscope.InnerText = horoscope.GetHoroscope(sign);
        }

        private QRJ.Models.Sign GetSign(Guid signId)
        {
            if (signId == Guid.Parse("c5d327a7-51b1-423b-a451-3b23649780c2"))
                return Sign.Aries;
            if (signId == Guid.Parse("7167cc36-9000-4169-b920-ea61327513bf"))
                return Sign.Taurus;
            if (signId == Guid.Parse("326f422f-7e4c-430c-9adc-44ca1f14fd0d"))
                return Sign.Gemini;
            if (signId == Guid.Parse("cd5843d6-2bc6-40d0-8bcd-e475fbf0911e"))
                return Sign.Cancer;
            if (signId == Guid.Parse("4b518cbb-6d73-4d2b-95e8-b2fc59264ce3"))
                return Sign.Leo;
            if (signId == Guid.Parse("0efadfa3-ed4a-46cc-80d3-9af9c9dbb461"))
                return Sign.Virgo;
            if (signId == Guid.Parse("a711a4b7-bb5f-4874-a712-ccc70843e869"))
                return Sign.Libra;
            if (signId == Guid.Parse("c35d28d4-6d74-4dd9-bd32-4b69a9cd7b78"))
                return Sign.Scorpio;
            if (signId == Guid.Parse("28e1457c-d324-4af3-995d-fffb59f03b59"))
                return Sign.Sagittarius;
            if (signId == Guid.Parse("d6947cf3-c3bf-4448-bf4b-aa1d81802573"))
                return Sign.Capricorn;
            if (signId == Guid.Parse("19ce4807-995c-40ca-8d95-13b3545aa61b"))
                return Sign.Aquarius;
            if (signId == Guid.Parse("73432d79-f4cd-4b7f-81d1-826ebe6aeb42"))
                return Sign.Pisces;

            throw new InvalidOperationException();
        }
    }
}