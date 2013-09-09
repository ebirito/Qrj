using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using QRJ.Models;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage.Blob;
using System.Configuration;

namespace QRJ.AdminPages
{
    public partial class Category : System.Web.UI.Page
    {
        Guid _categoryId = Guid.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["id"] != null)
                _categoryId = new Guid(Request.QueryString["id"]);

            if (!IsPostBack)
            {
                if (_categoryId != Guid.Empty)
                {
                    QRCodeContext db = new QRCodeContext();
                    QRJ.Models.Category category = db.Categories.Where(c => c.Id == _categoryId).FirstOrDefault();

                    Name.Text = category.Name;
                    Frequency.SelectedValue = category.Frequency.ToString();
                }
                categoryContentsDiv.Visible = _categoryId != Guid.Empty;
                btnContent.OnClientClick = "window.location.href='CategoryContent?categoryId=" + _categoryId.ToString() + "'; return false;";
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            QRCodeContext db = new QRCodeContext();
            // Check that the name is unique
            string name = Name.Text;
            if (db.Categories.Where(c => c.Name == name && c.Id != _categoryId).FirstOrDefault() != null)
            {
                // The name is in use
                ErrorMessage.Text = "Your already have a category with that name. Please enter a different name.";
                errorsDiv.Visible = true;
            }
            else     
            {
                if (_categoryId == Guid.Empty)
                {
                    // Create the new category
                    // Save the item to the database
                    db.Categories.Add(new QRJ.Models.Category
                    {
                        Id = Guid.NewGuid(),
                        Name = name,
                        Frequency = (Frequency)Enum.Parse(typeof(Frequency), Frequency.SelectedValue)
                    });
                }
                else
                {
                    QRJ.Models.Category category = db.Categories.Where(c => c.Id == _categoryId).FirstOrDefault();
                    category.Name = Name.Text;
                    category.Frequency = (Frequency)Enum.Parse(typeof(Frequency), Frequency.SelectedValue);
                }
                db.SaveChanges();
                Response.Redirect("ManageContent");
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
                                    Name = c.Name,
                                    FilePath = c.FilePath
                                };

            totalRowCount = categoryContents.Count();
            return categoryContents.OrderBy(c => c.Name).Skip(startRowIndex).Take(maximumRows);
        }

        // The id parameter name should match the DataKeyNames value set on the control
        public void CategoryContents_DeleteItem(Guid id)
        {
            QRCodeContext db = new QRCodeContext();
            QRJ.Models.CategoryContent categoryContent = db.CategoryContents.Where(c => c.Id == id).First();
            // Remove the blob from storage
            // Gdt the storage connection string
            CloudStorageAccount storageAccount =
                CloudStorageAccount.Parse(ConfigurationManager.AppSettings["StorageConnectionString"]);
            // Create the blob client.
            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();
            // Retrieve a reference to a container. 
            CloudBlobContainer container = blobClient.GetContainerReference("videos");
            // Set permission to public
            container.SetPermissions(
            new BlobContainerPermissions
            {
                PublicAccess =
                    BlobContainerPublicAccessType.Blob
            });
            try
            {
                CloudBlockBlob deleteBlob = container.GetBlockBlobReference(categoryContent.FilePath);
                deleteBlob.DeleteIfExists();
            }
            catch { }
            db.CategoryContents.Remove(categoryContent);
            db.SaveChanges();
        }

        protected void CategoryContents_RowEditing(object sender, GridViewEditEventArgs e)
        {
            Response.Redirect("CategoryContent?categoryId=" + _categoryId.ToString() +"&id=" + CategoryContents.DataKeys[e.NewEditIndex]["Id"].ToString());
        }
    }
}