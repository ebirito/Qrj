using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;

namespace QRJ.Account
{
    public partial class Login : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                bool adminLogin = Request.QueryString["Role"] == "Administrator";
                if (adminLogin)
                {
                    lnkRegister.Visible = false;
                    divRegister.Visible = false;
                }
            }

            RegisterHyperLink.NavigateUrl = "Register";
            OpenAuthLogin.ReturnUrl = Request.QueryString["ReturnUrl"];

            var returnUrl = HttpUtility.UrlEncode(Request.QueryString["ReturnUrl"]);
            if (!String.IsNullOrEmpty(returnUrl))
            {
                RegisterHyperLink.NavigateUrl += "?ReturnUrl=" + returnUrl;
            }
        }

        protected void OnLoggedIn(object sender, EventArgs e)
        {
            if (Request.QueryString["ReturnUrl"] != null)
            {
                LoginForm.DestinationPageUrl = Request.QueryString["ReturnUrl"].ToString();
            }
            else
            {

                //-- check if login user in Admin role
                if (Roles.IsUserInRole(LoginForm.UserName, "Administrator"))
                {
                    LoginForm.DestinationPageUrl = "~/AdminPages/Generate.aspx";

                }
                //-- check if login user in Member role
                else if (Roles.IsUserInRole(LoginForm.UserName, "Member"))
                {
                    LoginForm.DestinationPageUrl = "~/MemberPages/Home.aspx";
                }
            }
        }
    }
}