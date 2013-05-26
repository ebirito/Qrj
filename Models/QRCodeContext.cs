using System;
using System.Data.Entity;

namespace QRJ.Models
{
    public class QRCodeContext : DbContext
    {
        public QRCodeContext() : base("QRJ") 
        {
        }
        public DbSet<QRCode> QRCodes { get; set; }
    }
}