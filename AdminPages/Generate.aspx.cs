using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZXing;
using ZXing.Common;
using System.Drawing;
using System.IO;
using System.IO.Compression;
using QRJ.Models;

namespace QRJ.AdminPages
{
    public partial class Generate : System.Web.UI.Page
    {
        private readonly Random _rng = new Random();
        private const string _chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            // Get the user input
            int numberToGenerate = int.Parse(Number.Text);
            // Database management
            QRCodeContext db = new QRCodeContext();
            // Get the generator user Id
            Guid userId = (Guid)System.Web.Security.Membership.GetUser(User.Identity.Name).ProviderUserKey;

            // Generate QR code imges
            string folderBatch = Path.Combine(System.IO.Path.GetTempPath(), Guid.NewGuid().ToString());
            System.IO.Directory.CreateDirectory(folderBatch);
            for (int i = 0; i < numberToGenerate; i++)
            {
                // Initialize the QR witer
                BarcodeWriter writer = new BarcodeWriter
                {
                    Format = BarcodeFormat.QR_CODE
                };
                Guid qrId = Guid.NewGuid();
                string url = string.Format(Properties.Settings.Default.ViewPath, Properties.Settings.Default.DomainName, qrId.ToString());
                Bitmap qrCode = writer.Write(url);
                qrCode.Save(Path.Combine(folderBatch, qrId.ToString()) + ".jpg");

                // Save the item to the database
                db.QRCodes.Add(new QRCode { 
                    Id = qrId, 
                    ActivationCode = GenerateActivationCode(),
                    GeneratedBy = userId,
                    GeneratedOn = DateTime.Now
                });
            }

            // Save the database changes
            db.SaveChanges();

            // Zip the package
            string zipFileName = folderBatch + ".zip";
            ZipFile.CreateFromDirectory(folderBatch, zipFileName);

            //Download file
            Response.ContentType = "application/x-zip-compressed";
            Response.AppendHeader("Content-Disposition", "attachment; filename=QRCodes.zip");
            Response.TransmitFile(zipFileName);
            Response.End();
        }

        private string GenerateActivationCode()
        {
            return string.Format("{0}-{1}-{2}-{3}", RandomString(4), RandomString(4), RandomString(4), RandomString(4));
        }

        private string RandomString(int size)
        {
            char[] buffer = new char[size];

            for (int i = 0; i < size; i++)
            {
                buffer[i] = _chars[_rng.Next(_chars.Length)];
            }
            return new string(buffer);
        }
    }
}