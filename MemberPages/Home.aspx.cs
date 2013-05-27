using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using QRJ.Models;

namespace QRJ.MemberPages
{
    public partial class Home : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

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
        }

        protected void ProductSelector_CheckedChanged(object sender, EventArgs e)
        {
            //Enable/disable upload video button. Enable only if at least ne row is checked
            bool atLeastOneRowChecked = false;
            // Iterate through the Products.Rows property
            foreach (GridViewRow row in QRCodes.Rows)
            {
                // Access the CheckBox
                CheckBox cb = (CheckBox)row.FindControl("ProductSelector");
                if (cb.Checked)
                {
                    atLeastOneRowChecked = true;
                    break;
                }
            }

            UploadVideo.Enabled = atLeastOneRowChecked;
        }
    }
}