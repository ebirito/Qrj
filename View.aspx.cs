using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using QRJ.Models;


namespace QRJ
{
    public partial class View : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Set the link to the home page
            string url = string.Format("{0}{1}", "http://", Properties.Settings.Default.DomainName);
            homePage.HRef = url;
            homePage.InnerText = Properties.Settings.Default.DomainName;
            // Get the qr code id from the query string
            Guid qrCodeId;
            bool validGuid = Guid.TryParse(Request.QueryString["id"], out qrCodeId);
            // If the id is not a valid Guid then alert the user
            if (!validGuid)
            {
                Response.Redirect("~/NotFound");
            }

            // Get the QR Record from the DB
            QRCodeContext db = new QRCodeContext();
            QRCode qrCode = db.QRCodes.Where(q => q.Id == qrCodeId).FirstOrDefault();
            // If the qr code is not found then present user with an error message
            if (qrCode == null)
            {
                Response.Redirect("~/NotFound");
            }
            // If the code is not active or categories are not configured then the user must register/login
            else if (qrCode.ActivatedBy == null || qrCode.SuscribedCategories.Count == 0)
            {
                Session["qrCodeId"] = qrCode.Id;
                Response.Redirect("~/Account/Login");
            }
            // If no videos are found for any category then present the user with an error message
            else if (qrCode.SuscribedCategories.Sum(s => s.Contents.Count) == 0)
            {
                Response.Redirect("~/ContentNotFound");
            }
            // If the code is valid and activated and has categories then determine which video to show
            else
            {
                int indexNextCategory = 0;
                // Find the last category seen
                QRCodeViewedCategoryContent lastCategoryContentSeen = qrCode.ViewedCategoryContents.OrderByDescending(v => v.LastViewedOn).FirstOrDefault();
                List<Category> sortedSubscriptions = qrCode.SuscribedCategories.OrderBy(s => s.Name).ToList();
                if (lastCategoryContentSeen != null)
                {
                    // Move to the next category
                    indexNextCategory = sortedSubscriptions.IndexOf(lastCategoryContentSeen.CategoryContent.Category) + 1;
                    if (indexNextCategory == qrCode.SuscribedCategories.Count)
                        indexNextCategory = 0;
                }

                Category categoryToWatch = sortedSubscriptions[indexNextCategory];
                CategoryContent contentToWatch = null;
                // Special case for daily videos
                if (categoryToWatch.Frequency == Frequency.Daily)
                {
                    // For daily subscriptions, see if there is a video that has already been watched today
                    QRCodeViewedCategoryContent lastWatched = qrCode.ViewedCategoryContents.Where(v => v.CategoryContent.CategoryId == categoryToWatch.Id).OrderByDescending(v => v.LastViewedOn).FirstOrDefault();
                    // If there was a video watched today then display the same one
                    if (lastWatched != null && lastWatched.ExpiresOn > DateTime.Now)
                    {
                        contentToWatch = lastWatched.CategoryContent;
                        lastWatched.LastViewedOn = DateTime.Now;
                    }
                }
                
                // If not daily or if daily video expired then find next video to show
                if (contentToWatch == null)
                {
                    // Find a video in that category that has not been seen before
                    foreach (CategoryContent categoryContent in categoryToWatch.Contents)
                    {
                        if (qrCode.ViewedCategoryContents.Where(v => v.CategoryContentId == categoryContent.Id).FirstOrDefault() == null)
                        {
                            contentToWatch = categoryContent;
                            qrCode.ViewedCategoryContents.Add(
                                new QRCodeViewedCategoryContent { CategoryContentId = categoryContent.Id, 
                                    QrCodeId = qrCode.Id, LastViewedOn = DateTime.Now, ExpiresOn = DateTime.Now.AddDays(1) });
                            break;
                        }
                    }

                    // If all the videos in the category have been watched we just show the oldest one
                    if (contentToWatch == null)
                    {
                        QRCodeViewedCategoryContent watched = qrCode.ViewedCategoryContents.Where(v => v.CategoryContent.CategoryId == categoryToWatch.Id).OrderBy(v => v.LastViewedOn).FirstOrDefault();
                        // Update the watched date and expiration date
                        watched.LastViewedOn = DateTime.Now;
                        watched.ExpiresOn = DateTime.Now.AddDays(1);
                        contentToWatch = watched.CategoryContent;
                    }
                }
               
                // Update the viewed category contents
                db.SaveChanges();

                // Show the video
                Response.Redirect("~/Watch?filePath=" + contentToWatch.FilePath);
            }
        }
    }
}