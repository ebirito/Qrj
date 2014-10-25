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
            // Insure that the __doPostBack() JavaScript method is created...
            ClientScript.GetPostBackEventReference(this, string.Empty);

            if (this.IsPostBack)
            {
                string eventTarget = (this.Request["__EVENTTARGET"] == null) ? string.Empty : this.Request["__EVENTTARGET"];
                string eventArgument = (this.Request["__EVENTARGUMENT"] == null) ? string.Empty : this.Request["__EVENTARGUMENT"];

                // Get eastern date/time
                TimeZoneInfo easternZone = TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time");
                DateTime clientDateTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, easternZone).Date;

                if (eventTarget == "TimezoneOffsetPostBack")
                {
                    string timezoneOffset = eventArgument;
                    // get client date time
                    clientDateTime = DateTime.UtcNow.AddMinutes(-int.Parse(timezoneOffset) * 60);
                }

                Guid horoscopeQrCodeId = new Guid(Request.QueryString["id"]);
                QRJ.Models.Sign sign = GetSign(horoscopeQrCodeId);

                // Check if there is a horoscope for this date
                QRCodeContext db = new QRCodeContext();
                QRJ.Models.Horoscope horoscope = db.Horoscopes.Where(h => h.Date.Year == clientDateTime.Year && 
                    h.Date.Month == clientDateTime.Month && h.Date.Day == clientDateTime.Day).FirstOrDefault();
                // If we have the horoscope then just display it
                if (horoscope == null)
                {
                    // Otherwise get it from the service
                    WebRequest request = WebRequest.Create("http://horoscopeservices.co.uk/daily_delivery/xmlaccess.asp?uid=608364284&date=" + clientDateTime.ToString("yyyy-MM-dd"));
                    WebResponse response = request.GetResponse();
                    // Get the stream containing content returned by the server.
                    Stream dataStream = response.GetResponseStream();
                    // Open the stream using a StreamReader for easy access.
                    StreamReader reader = new StreamReader(dataStream);
                    // Read the content.
                    string responseFromServer = reader.ReadToEnd();

                    // Save the horoscope in the db
                    horoscope = new Models.Horoscope { Id = Guid.NewGuid(), Date = clientDateTime, Data = responseFromServer };
                    db.Horoscopes.Add(horoscope);
                    db.SaveChanges();
                }

                // Choose between the 12 background images
                int imageIndex = (clientDateTime.Day % 12) + 1;

                imgBackground.Src = "../../Content/themes/base/images/horoscopeBackgrounds/" + sign.ToString() + "/" + imageIndex + ".png";
                txtHoroscope.InnerHtml = string.Format("{0}:<br /><br />{1}", clientDateTime.ToString("D"), horoscope.GetHoroscope(sign));
                // Show the astrobanz link only for Set 1
                lnkAstrobanz.Visible = GetSet(horoscopeQrCodeId) == 1;
            }
            else
            {
                System.Text.StringBuilder javaScript = new System.Text.StringBuilder();

                javaScript.Append("var currentDate = new Date();");
                javaScript.Append("var timezoneOffset = currentDate.getTimezoneOffset() / 60;");
                javaScript.Append("__doPostBack('TimezoneOffsetPostBack', timezoneOffset);");;

                System.Web.UI.ScriptManager.RegisterStartupScript(this, GetType(), "TimezoneOffsetScript", javaScript.ToString(), true);
            }
        }

        private QRJ.Models.Sign GetSign(Guid horoscopeQrCodeId)
        {
            QRCodeContext db = new QRCodeContext();
            HoroscopeQrCode horoscopeQrCode = db.HoroscopeQrCodes.Where(h => h.Id == horoscopeQrCodeId).FirstOrDefault();
            return db.HoroscopeSigns.Where(s => s.Id == horoscopeQrCode.HoroscopeSignId).FirstOrDefault().Sign;
        }

        private int GetSet(Guid horoscopeQrCodeId)
        {
            QRCodeContext db = new QRCodeContext();
            HoroscopeQrCode horoscopeQrCode = db.HoroscopeQrCodes.Where(h => h.Id == horoscopeQrCodeId).FirstOrDefault();
            return db.HoroscopeSets.Where(s => s.Id == horoscopeQrCode.HoroscopeSetId).FirstOrDefault().SetNumber;
        }
    }
}