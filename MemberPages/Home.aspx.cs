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
            // If the user logged in after scanning an item then it needs to be activated
            if (Session["qrCodeId"] != null)
            {
                Guid qrCodeId = (Guid)Session["qrCodeId"];
                Session["qrCodeId"] = null;
                Response.Redirect("Product?id=" + qrCodeId.ToString());
            }
        }

        // The return type can be changed to IEnumerable, however to support
        // paging and sorting, the following parameters must be added:
        //     int maximumRows
        //     int startRowIndex
        //     out int totalRowCount
        //     string sortByExpression
        public object Products_GetData()
        {
            Guid userId = (Guid)System.Web.Security.Membership.GetUser(User.Identity.Name).ProviderUserKey;
            QRCodeContext db = new QRCodeContext();
            var products = db.QRCodes.Where(c => c.ActivatedBy == userId).ToList().Select(p => new
                           {
                               Id = p.Id,
                               ProductName = p.ProductName,
                               ActivatedOn = p.ActivatedOn.Value.ToLocalTime(),
                               Subscriptions = string.Join(",", p.SuscribedCategories.Select(s => s.Name).ToArray())
                           });

            return products;
        }

        protected void Products_RowEditing(object sender, GridViewEditEventArgs e)
        {
            Response.Redirect("Product?id=" + Products.DataKeys[e.NewEditIndex]["Id"].ToString());
        }

        // The id parameter name should match the DataKeyNames value set on the control
        public void Products_DeleteItem(Guid id)
        {
            QRCodeContext db = new QRCodeContext();
            QRJ.Models.QRCode qrCode = db.QRCodes.Where(c => c.Id == id).First();
            qrCode.ProductName = "";
            qrCode.ActivatedBy = null;
            qrCode.ActivatedOn = null;
            qrCode.SuscribedCategories.RemoveRange(0, qrCode.SuscribedCategories.Count);
            db.SaveChanges();
        }
    }
}