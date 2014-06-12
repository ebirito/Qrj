using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using QRJ.Models;

namespace QRJ.PublicPages.Horoscope
{
    public partial class Select : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        protected void btnAdult_Click(object sender, EventArgs e)
        {
            SaveCookie(HoroscopeStyle.Adult);
        }

        protected void btnTeen_Click(object sender, EventArgs e)
        {
            SaveCookie(HoroscopeStyle.Teen);
        }

        private void SaveCookie(HoroscopeStyle style)
        {
            if (chkRemember.Checked)
            {
                HttpCookie cookie = new HttpCookie("HoroscopeStyle");
                cookie.Value = style.ToString();
                cookie.Expires = DateTime.Now.AddYears(50);
                Response.Cookies.Add(cookie);
            }
            Response.Redirect("View?id=" + Session["SignId"].ToString() + "&horoscopeStyle=" + style.ToString());
        }
    }
}