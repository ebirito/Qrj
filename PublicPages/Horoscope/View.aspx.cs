using System;
using System.Collections.Generic;
using System.Linq;
using QRJ.Models;
using System.Net;
using System.IO;

namespace QRJ.PublicPages.Horoscope
{
    public partial class View : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Guid horoscopeQrCodeId = new Guid(Request.QueryString["id"]);
            QRJ.Models.Sign sign = GetSign(horoscopeQrCodeId);

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
                WebRequest request = WebRequest.Create("http://horoscopeservices.co.uk/daily_delivery/xmlaccess.asp?uid=608364284&date=" + today.ToString("yyyy-MM-dd"));
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

            imgBackground.Src = "../../Content/themes/base/images/horoscopeBackgrounds/" + sign.ToString() + ".jpg";
            txtSign.InnerText = sign.ToString();
            txtHoroscope.InnerText = horoscope.GetHoroscope(sign);
        }

        private QRJ.Models.Sign GetSign(Guid horoscopeQrCodeId)
        {
            QRCodeContext db = new QRCodeContext();
            HoroscopeQrCode horoscopeQrCode = db.HoroscopeQrCodes.Where(h => h.Id == horoscopeQrCodeId).FirstOrDefault();
            return db.HoroscopeSigns.Where(s => s.Id == horoscopeQrCode.HoroscopeSignId).FirstOrDefault().Sign;
        }
    }
}