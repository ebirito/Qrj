using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using QRJ.Models;

namespace QRJ
{
    public partial class _Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //new QRCodeDatabaseInitializer().InitializeDatabase(new QRCodeContext());
            // Redirect to Member r Admin home pages
            if (Request.IsAuthenticated)
            {
                if (Context.User.IsInRole("Administrator"))
                    Response.Redirect("~/AdminPages/Generate");
                else
                    Response.Redirect("~/MemberPages/Home");
            }
            else
                Response.Redirect("~/Account/Login.aspx");
        }
    }
}