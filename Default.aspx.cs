using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;

namespace QRJ
{
    public partial class _Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Redirect to Member r Admin home pages
            if (Request.IsAuthenticated)
            {
                if (Context.User.IsInRole("Administrator"))
                    Response.Redirect("~/AdminPages/Generate.aspx");
                else
                    Response.Redirect("~/MemberPages/Home.aspx");
            }
        }
    }
}