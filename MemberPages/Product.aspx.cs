using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using QRJ.Models;

namespace QRJ.AdminPages
{
    public partial class Product : System.Web.UI.Page
    {
        Guid _productId = Guid.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            _productId = new Guid(Request.QueryString["id"]);

            if (!IsPostBack)
            {
                QRCodeContext db = new QRCodeContext();
                QRJ.Models.QRCode product = db.QRCodes.Where(c => c.Id == _productId).FirstOrDefault();

                Name.Text = product.ProductName;
                if (product.SuscribedCategories.Count > 0)
                    Frequency.SelectedValue = product.SuscribedCategories[0].Frequency.ToString();
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            QRCodeContext db = new QRCodeContext();
            // Check that the name is unique for the user
            Guid userId = (Guid)System.Web.Security.Membership.GetUser(User.Identity.Name).ProviderUserKey;
            string productName = Name.Text;
            if (db.QRCodes.Where(q => q.ActivatedBy == userId && q.Id != _productId && q.ProductName == productName).FirstOrDefault() != null)
            {
                // The name is in use
                ErrorMessage.Text = "Your already have a product with that name. Please enter a different name.";
                errorsDiv.Visible = true;
            }
            else     
            {
                QRJ.Models.QRCode product = db.QRCodes.Where(c => c.Id == _productId).FirstOrDefault();
                product.ProductName = Name.Text;
                product.SuscribedCategories.RemoveRange(0, product.SuscribedCategories.Count);
                if (product.ActivatedBy == null)
                {
                    product.ActivatedBy = userId;
                    product.ActivatedOn = DateTime.Now;
                }
                foreach (GridViewRow row in Subscriptions.Rows)
                {
                    // Access the CheckBox
                    CheckBox cb = (CheckBox)row.FindControl("SubscriptionSelector");
                    if (cb.Checked)
                    {
                        Guid rowId = new Guid(Subscriptions.DataKeys[row.RowIndex]["Id"].ToString());
                        QRJ.Models.Category category = db.Categories.Where(c => c.Id == rowId).FirstOrDefault();
                        product.SuscribedCategories.Add(category);
                    }
                }
                db.SaveChanges();
                Response.Redirect("Home");
            }
        }

        // The return type can be changed to IEnumerable, however to support
        // paging and sorting, the following parameters must be added:
        //     int maximumRows
        //     int startRowIndex
        //     out int totalRowCount
        //     string sortByExpression
        public IQueryable Subscriptions_GetData()
        {
            Frequency frequency = (Frequency)Enum.Parse(typeof(Frequency), Frequency.SelectedValue);
            QRCodeContext db = new QRCodeContext();
            var subscriptions = from c in db.Categories
                                where c.Frequency == frequency
                                select new
                                {
                                    Id = c.Id,
                                    Name = c.Name
                                };

            return subscriptions;
        }

        protected void Frequency_SelectedIndexChanged(object sender, EventArgs e)
        {
            Subscriptions.DataBind();
        }

        protected void Subscriptions_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowIndex >= 0)
            {
                QRCodeContext db = new QRCodeContext();
                QRJ.Models.QRCode product = db.QRCodes.Where(c => c.Id == _productId).FirstOrDefault();
                Guid rowId = new Guid(Subscriptions.DataKeys[e.Row.RowIndex]["Id"].ToString());

                CheckBox cb = (CheckBox)e.Row.FindControl("SubscriptionSelector");
                cb.Checked = product.SuscribedCategories.Exists(s => s.Id == rowId);
            }
        }
    }
}