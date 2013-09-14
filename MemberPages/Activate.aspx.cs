using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZXing;
using ZXing.Common;
using System.Drawing;
using System.IO;
using System.IO.Compression;
using QRJ.Models;

namespace QRJ.AdminPages
{
    public partial class Activate : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            // Get the QR Record from the DB
            string activationCode = ActivationCode.Text;
            QRCodeContext db = new QRCodeContext();
            QRCode qrCode = db.QRCodes.Where(q => q.ActivationCode == activationCode).FirstOrDefault();
            if (qrCode == null)
            {
                //Invaid activation code (not found)
                ErrorMessage.Text = "The activation code is invalid.";
                errorsDiv.Visible = true;
            }
            else if (qrCode.ActivatedOn != null)
            {
                // Product has already been activated
                ErrorMessage.Text = "The activation code entered belongs to a product that has already been activated.";
                errorsDiv.Visible = true;
            }
            else
            {
                // Check that the name is unique for the user
                Guid userId = (Guid)System.Web.Security.Membership.GetUser(User.Identity.Name).ProviderUserKey;
                string productName = ProductName.Text;
                if (db.QRCodes.Where(q => q.ActivatedBy == userId && q.ProductName == productName).FirstOrDefault() != null)
                {
                    // The product name is in use
                    ErrorMessage.Text = "Your already have an activated product with that product name. Please enter a different product name.";
                    errorsDiv.Visible = true;
                }
                else
                {
                    // Activate the product
                    qrCode.ActivatedBy = userId;
                    qrCode.ActivatedOn = DateTime.Now.ToUniversalTime();
                    qrCode.ProductName = ProductName.Text;
                    db.SaveChanges();
                    Response.Redirect("~/MemberPages/Home.aspx");
                }
            }
        }
    }
}