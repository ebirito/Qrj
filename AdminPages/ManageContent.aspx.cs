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
    public partial class ManageContent : System.Web.UI.Page
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
        public IQueryable Categories_GetData(int maximumRows, int startRowIndex, out int totalRowCount)
        {
            QRCodeContext db = new QRCodeContext();
            var categories = from c in db.Categories
                             select new
                             {
                                 Id = c.Id,
                                 Name = c.Name,
                                 Frequency = c.Frequency == Frequency.Daily ? "Daily" : "On-Demand",
                                 NumberOfContents = c.Contents.Count
                             };
            totalRowCount = categories.Count();
            return categories.OrderBy(c => c.Name).Skip(startRowIndex).Take(maximumRows);
        }

        // The id parameter name should match the DataKeyNames value set on the control
        public void Categories_DeleteItem(Guid id)
        {
            QRCodeContext db = new QRCodeContext();
            QRJ.Models.Category category = db.Categories.Where(c => c.Id == id).First();

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

            foreach(QRJ.Models.CategoryContent categoryContent in category.Contents)
            {
                try
                {
                    CloudBlockBlob deleteBlob = container.GetBlockBlobReference(categoryContent.FilePath);
                    deleteBlob.DeleteIfExists();
                }
                catch { }
            }

            db.Categories.Remove(category);
            db.SaveChanges();
        }

        protected void Categories_RowEditing(object sender, GridViewEditEventArgs e)
        {
            Response.Redirect("Category?id=" + Categories.DataKeys[e.NewEditIndex]["Id"].ToString());
        }
    }
}