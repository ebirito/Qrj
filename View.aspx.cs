using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using QRJ.Models;


namespace QRJ
{
    public partial class View : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Set the link to the home page
            string url = string.Format("{0}{1}", "http://", Properties.Settings.Default.DomainName);
            homePage.HRef = url;
            homePage.InnerText = Properties.Settings.Default.DomainName;
            // Get the qr code id from the query string
            Guid qrCodeId;
            bool validGuid = Guid.TryParse(Request.QueryString["id"], out qrCodeId);
            // If the id is not a valid Guid then alert the user
            if (!validGuid)
            {
                Response.Redirect("~/NotFound");
            }

            // Get the QR Record from the DB
            QRCodeContext db = new QRCodeContext();
            QRCode qrCode = db.QRCodes.Where(q => q.Id == qrCodeId).FirstOrDefault();
            // If the qr code is not found then present user with an error message
            if (qrCode == null)
            {
                Response.Redirect("~/NotFound");
            }
            // If the code is not active or categories are not configured then the user must register/login
            else if (qrCode.ActivatedBy == null || qrCode.SuscribedCategories.Count == 0)
            {
                Session["qrCodeId"] = qrCode.Id;
                Response.Redirect("~/Account/Login");
            }
            // If the code is valid and activated and has categories then determine which video to show
            else
            {
                //Response.Redirect("~/Watch?filePath=" + qrCode.FilePath);
            }
        }
    }
}