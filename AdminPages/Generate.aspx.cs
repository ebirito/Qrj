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

namespace QRJ.AdminPages
{
    public partial class Generate : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            // Get the user input
            int numberToGenerate = int.Parse(Number.Text);

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
            }

            // Zip the package
            string zipFileName = folderBatch + ".zip";
            ZipFile.CreateFromDirectory(folderBatch, zipFileName);

            //Download file
            Response.ContentType = "application/x-zip-compressed";
            Response.AppendHeader("Content-Disposition", "attachment; filename=QRCodes.zip");
            Response.TransmitFile(zipFileName);
            Response.End();
        }
    }
}