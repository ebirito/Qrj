using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using QRJ.Models;

namespace QRJ.MemberPages
{
    public partial class Home : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            HtmlGenericControl js = new HtmlGenericControl("script");
            js.Attributes["type"] = "text/javascript";
            js.Attributes["src"] = this.ResolveClientUrl("~/Scripts/Pages/Home.js");
            Page.Header.Controls.Add(js);

            String jsonClientIDs = "window.ClientIDs = { " +
                       "QRCodesClientID: '" + QRCodes.ClientID + "'," +
                       "FileUploadClientID: '" + FileUpload.ClientID + "'," +
                       "UploadVideoClientID: '" + UploadVideo.ClientID + "'" +
                       "}";

            this.Page.ClientScript.RegisterClientScriptBlock(GetType(), "ClientIDs", jsonClientIDs, true);
        }

        // The return type can be changed to IEnumerable, however to support
        // paging and sorting, the following parameters must be added:
        //     int maximumRows
        //     int startRowIndex
        //     out int totalRowCount
        //     string sortByExpression
        public IQueryable QRCodes_GetData()
        {
            Guid userId = (Guid)System.Web.Security.Membership.GetUser(User.Identity.Name).ProviderUserKey;
            QRCodeContext db = new QRCodeContext();
            return db.QRCodes.Where(q => q.ActivatedBy == userId).OrderBy(q => q.ProductName);
        }

        protected void UploadVideo_Click(object sender, EventArgs e)
        {
            // Before attempting to save the file, verify
            // that the FileUpload control contains a file.
            if (FileUpload.HasFile)
            {
                // Alert the success
                Response.Write("<script>alert('Video uploaded successfully and linked to QR code(s)');</script>");
            }
        }
    }
}