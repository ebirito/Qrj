using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using QRJ.Models;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage.Blob;
using System.Configuration;

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
                       "};";


            successMessage.Visible = Page.IsPostBack;

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
                // Gdt the storage connection string
                CloudStorageAccount storageAccount = 
                    CloudStorageAccount.Parse(ConfigurationManager.AppSettings["StorageConnectionString"]);
                // Create the blob client.
                CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();
                // Retrieve a reference to a container. 
                CloudBlobContainer container = blobClient.GetContainerReference("videos");
                // Create the container if it doesn't already exist.
                container.CreateIfNotExists();
                // Set permission to public
                container.SetPermissions(
                new BlobContainerPermissions
                {
                    PublicAccess =
                        BlobContainerPublicAccessType.Blob
                });
                // Create a new file name
                string fileName = Guid.NewGuid().ToString() + System.IO.Path.GetExtension(FileUpload.FileName).ToLower();
                CloudBlockBlob blockBlob = container.GetBlockBlobReference(fileName);
                // Create or overwrite the blob.
                blockBlob.UploadFromStream(FileUpload.FileContent);
                blockBlob.Properties.ContentType = "video/mp4";
                blockBlob.SetProperties();
               
                // Save the video path for each selected QECode
                QRCodeContext db = new QRCodeContext();
                foreach (GridViewRow row in QRCodes.Rows)
                {
                    // Access the CheckBox
                    CheckBox cb = (CheckBox)row.FindControl("ProductSelector");
                    if (cb.Checked)
                    {
                        Guid rowId = new Guid(QRCodes.DataKeys[row.RowIndex]["Id"].ToString());
                        db.QRCodes.Where(q => q.Id == rowId).First().FilePath = fileName;
                    }
                }

                // Save the updates in the db
                db.SaveChanges();
            }
        }
    }
}