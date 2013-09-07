using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using QRJ.Models;

namespace QRJ.AdminPages
{
    public partial class CategoryContent : System.Web.UI.Page
    {
        Guid _categoryId = Guid.Empty;
        Guid _categoryContentId = Guid.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            _categoryId = new Guid(Request.QueryString["categoryId"]);
            if (Request.QueryString["id"] != null)
                _categoryContentId = new Guid(Request.QueryString["id"]);

            if (!IsPostBack)
            {
                if (_categoryContentId != Guid.Empty)
                {
                    QRCodeContext db = new QRCodeContext();
                    QRJ.Models.CategoryContent categoryContent = db.CategoryContents.Where(c => c.Id == _categoryContentId).FirstOrDefault();

                    Name.Text = categoryContent.Name;
                }
                btnCancel.OnClientClick = "window.location.href='Category?id=" + _categoryId.ToString() + "'; return false;";
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            QRCodeContext db = new QRCodeContext();
            // Check that the name is unique
            string name = Name.Text;
            if (db.CategoryContents.Where(c => c.Name == name && c.CategoryId == _categoryId && c.Id != _categoryContentId).FirstOrDefault() != null)
            {
                // The name is in use
                ErrorMessage.Text = "Your already have a video with that name in this category. Please enter a different name.";
                errorsDiv.Visible = true;
            }
            else     
            {
                if (_categoryContentId == Guid.Empty)
                {
                    // Create the new category content
                    // Save the item to the database
                    db.CategoryContents.Add(new QRJ.Models.CategoryContent
                    {
                        Id = Guid.NewGuid(),
                        CategoryId = _categoryId,
                        Name = name
                    });
                }
                else
                {
                    QRJ.Models.CategoryContent categoryContent = db.CategoryContents.Where(c => c.Id == _categoryContentId).FirstOrDefault();
                    categoryContent.Name = Name.Text;
                }
                db.SaveChanges();
                Response.Redirect("Category?id=" + _categoryId.ToString());
            }
        }

        // The return type can be changed to IEnumerable, however to support
        // paging and sorting, the following parameters must be added:
        //     int maximumRows
        //     int startRowIndex
        //     out int totalRowCount
        //     string sortByExpression
        public IQueryable CategoryContents_GetData(int maximumRows, int startRowIndex, out int totalRowCount)
        {
            QRCodeContext db = new QRCodeContext();
            var categoryContents = from c in db.CategoryContents
                                   where c.CategoryId == _categoryId
                                select new
                                {
                                    Id = c.Id,
                                    Name = c.Name
                                };

            totalRowCount = categoryContents.Count();
            return categoryContents.OrderBy(c => c.Name).Skip(startRowIndex).Take(maximumRows);
        }

        // The id parameter name should match the DataKeyNames value set on the control
        public void CategoryContents_DeleteItem(Guid id)
        {
            QRCodeContext db = new QRCodeContext();
            db.CategoryContents.Remove(db.CategoryContents.Where(c => c.Id == id).First());
            db.SaveChanges();
        }

        protected void CategoryContents_RowEditing(object sender, GridViewEditEventArgs e)
        {
            //Response.Redirect("Category?id=" + Categories.DataKeys[e.NewEditIndex]["Id"].ToString());
        }
    }
}