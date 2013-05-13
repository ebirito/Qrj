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

namespace QRJ.AdminPages
{
    public partial class Generate : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            var writer = new BarcodeWriter
            {
                Format = BarcodeFormat.QR_CODE
            };
            Bitmap qrCode = writer.Write("www.abeandkim.com");
            qrCode.Save(Server.MapPath("~/QRCode.jpg"));
        }
    }
}