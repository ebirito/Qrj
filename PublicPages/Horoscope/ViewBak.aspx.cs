using System;
using System.Collections.Generic;
using System.Linq;
using QRJ.Models;

namespace QRJ.PublicPages.Horoscope
{
    public partial class ViewBak : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                Sign sign = GetSign(new Guid(Request.QueryString["id"]));

                if (sign == Sign.Aquarius)
                {
                    // Check to see if cookie is present
                    HoroscopeStyle? style = null;
                    if (Request.Cookies["HoroscopeStyle"] != null)
                        style = (HoroscopeStyle)Enum.Parse(typeof(HoroscopeStyle), Request.Cookies["HoroscopeStyle"].Value);
                    else if (Request.QueryString["horoscopeStyle"] != null)
                        style = (HoroscopeStyle)Enum.Parse(typeof(HoroscopeStyle), Request.QueryString["horoscopeStyle"]);

                    // If cookie or user selection in querystring is not found, then redirect to the selection page
                    if (!style.HasValue)
                    {
                        Session["SignId"] = Request.QueryString["id"];
                        Response.Redirect("Select");
                    }

                    // Get the timezone from the cookie
                    DateTime clientDateTime = DateTime.UtcNow.AddMinutes(-int.Parse(Request.Cookies["TimeZone"].Value.ToString()) * 60);
                    // For now, we only have 4 images per category
                    int imageIndex = (clientDateTime.Day % 4) + 1;

                    imgBackground.Src = "../../Content/themes/base/images/demo/MOCK " + style.ToString().ToUpper() + " DAY " +  imageIndex + ".jpg";
                }

                signText.InnerText = sign.ToString();
                divBackgroundImage.Visible = sign == Sign.Aquarius;
                divUnderConstruction.Visible = sign != Sign.Aquarius;
            }
            catch
            {
                signText.InnerText = "EXTRA";
                divBackgroundImage.Visible = false;
            }
        }

        private QRJ.Models.Sign GetSign(Guid signId)
        {
            if (signId == Guid.Parse("55c56a74-48ae-47e2-a113-4b5621986c64"))
                return Sign.Aries;
            if (signId == Guid.Parse("6a9286e9-de79-482f-9922-1aec4dd33de1"))
                return Sign.Taurus;
            if (signId == Guid.Parse("3f379b6b-b421-4fdf-9805-f0f54f4a3c9c"))
                return Sign.Gemini;
            if (signId == Guid.Parse("b6671247-a5c2-4536-a343-b0904ad6dbef"))
                return Sign.Cancer;
            if (signId == Guid.Parse("5ed2ee99-f54b-484b-9ffc-9b3c82863a0e"))
                return Sign.Leo;
            if (signId == Guid.Parse("a9c6219a-49c8-4eb0-b5e5-22302d2620ee"))
                return Sign.Virgo;
            if (signId == Guid.Parse("959d6231-091d-468c-9f14-1bb129134344"))
                return Sign.Libra;
            if (signId == Guid.Parse("f31f19bc-56d6-4d12-ba24-4dfc046ec5d5"))
                return Sign.Scorpio;
            if (signId == Guid.Parse("08a3daaa-0c35-493f-ae65-2b3324cd6664"))
                return Sign.Sagittarius;
            if (signId == Guid.Parse("4e4675df-3550-4cd7-9543-f34648c765d2"))
                return Sign.Capricorn;
            if (signId == Guid.Parse("10580727-904c-4dbb-9f76-8510a1ebb38c"))
                return Sign.Aquarius;
            if (signId == Guid.Parse("e1345010-6038-4655-9a18-02b4a04fcd5a"))
                return Sign.Pisces;

            throw new InvalidOperationException();
        }
    }
}