using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.AspNet.Membership.OpenAuth;
using System.Configuration;
using System.Web.Profile;

namespace QRJ.Account
{
    public partial class Register : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            RegisterUser.ContinueDestinationPageUrl = Request.QueryString["ReturnUrl"];
        }

        protected void RegisterUser_CreatedUser(object sender, EventArgs e)
        {
            // Create an empty Profile for the newly created user
            ProfileBase p = (ProfileBase)ProfileBase.Create(RegisterUser.UserName, true);

            // Populate some Profile properties off of the create user wizard
            p.SetPropertyValue("FirstName", ((TextBox)RegisterUser.CreateUserStep.ContentTemplateContainer.FindControl("FirstName")).Text);
            p.SetPropertyValue("LastName", ((TextBox)RegisterUser.CreateUserStep.ContentTemplateContainer.FindControl("LastName")).Text);

            // Save profile - must be done since we explicitly created it
            p.Save();

            Roles.AddUserToRole(RegisterUser.UserName, "Member");

            FormsAuthentication.SetAuthCookie(RegisterUser.UserName, createPersistentCookie: false);

            string continueUrl = RegisterUser.ContinueDestinationPageUrl;
            if (!OpenAuth.IsLocalUrl(continueUrl))
            {
                continueUrl = "~/";
            }
            Response.Redirect(continueUrl);
        }

        protected void RegisterUser_CreatingUser(object sender, LoginCancelEventArgs e)
        {
            // Username/email are the same
            RegisterUser.Email = RegisterUser.UserName;
        }
    }
}