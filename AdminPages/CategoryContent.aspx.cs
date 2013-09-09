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
    public partial class CategoryContent : System.Web.UI.Page
    {
        Guid _categoryId = Guid.Empty;
        Guid _categoryContentId = Guid.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!FileUpload.IsInFileUploadPostBack)
            {
                _categoryId = new Guid(Request.QueryString["categoryId"]);
                Session["categoryId"] = _categoryId;

                if (Request.QueryString["id"] != null)
                {
                    _categoryContentId = new Guid(Request.QueryString["id"]);
                    Session["categoryContentId"] = _categoryContentId;
                }

                if (!IsPostBack)
                {
                    QRCodeContext db = new QRCodeContext();
                    btnCancel.OnClientClick = "window.location.href='Category?id=" + _categoryId.ToString() + "'; return false;";
                    divVideo.Visible = _categoryContentId != Guid.Empty;
                    divUploader.Visible = _categoryContentId != Guid.Empty;

                    if (_categoryContentId != Guid.Empty)
                    {
                        QRJ.Models.CategoryContent categoryContent = db.CategoryContents.Where(c => c.Id == _categoryContentId).FirstOrDefault();

                        Name.Text = categoryContent.Name;
                        divVideo.Visible = !string.IsNullOrEmpty(categoryContent.FilePath);

                        if (!string.IsNullOrEmpty(categoryContent.FilePath))
                        {
                            // Retrieve storage account from connection string.
                            CloudStorageAccount storageAccount =
                                CloudStorageAccount.Parse(ConfigurationManager.AppSettings["StorageConnectionString"]);

                            // Create the blob client.
                            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();

                            // Retrieve reference to a previously created container.
                            CloudBlobContainer container = blobClient.GetContainerReference("videos");

                            // Retrieve reference to the video
                            CloudBlockBlob blockBlob = container.GetBlockBlobReference(categoryContent.FilePath);

                            // Set the video source
                            videoSource.Src = blockBlob.Uri.ToString();
                        }
                    }
                }
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
                    Guid categoryContentId = Guid.NewGuid();
                    // Create the new category content
                    // Save the item to the database
                    db.CategoryContents.Add(new QRJ.Models.CategoryContent
                    {
                        Id = categoryContentId,
                        CategoryId = _categoryId,
                        Name = name
                    });
                    db.SaveChanges();
                    Response.Redirect("CategoryContent?categoryId=" + _categoryId.ToString() + "&id=" + categoryContentId);
                }
                else
                {
                    QRJ.Models.CategoryContent categoryContent = db.CategoryContents.Where(c => c.Id == _categoryContentId).FirstOrDefault();
                    categoryContent.Name = Name.Text;
                    db.SaveChanges();
                    Response.Redirect("Category?id=" + _categoryId.ToString());
                }
            }
        }

        protected void FileUpload_UploadComplete(object sender, AjaxControlToolkit.AjaxFileUploadEventArgs e)
        {
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

            // Rename the blob
            //grab the existing blob
            CloudBlockBlob oldBlockBlob = container.GetBlockBlobReference(e.FileName);
            // Create a new file name
            string fileName = Guid.NewGuid().ToString() + System.IO.Path.GetExtension(e.FileName).ToLower();
            CloudBlockBlob newBlockBlob = container.GetBlockBlobReference(fileName);
            //create a new blob
            newBlockBlob.StartCopyFromBlob(oldBlockBlob);
            //delete the old
            oldBlockBlob.Delete();

            // Set the blob properties
            newBlockBlob.Properties.ContentType = "video/mp4";
            newBlockBlob.SetProperties();

            QRCodeContext db = new QRCodeContext();
            Guid categoryId = (Guid)Session["categoryId"];
            Guid categoryContentId = (Guid)Session["categoryContentId"];
            QRJ.Models.CategoryContent categoryContent = db.CategoryContents.Where(c => c.Id == categoryContentId).FirstOrDefault();
            // Delete old video if it exists
            if (!string.IsNullOrEmpty(categoryContent.FilePath))
            {
                CloudBlockBlob deleteBlob = container.GetBlockBlobReference(categoryContent.FilePath);
                deleteBlob.DeleteIfExists();
            }
            categoryContent.FilePath = fileName;
            db.SaveChanges();
        }
    }
}