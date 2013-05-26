using System;
using System.Collections.Generic;
using System.Data.Entity;

namespace QRJ.Models
{
    public class QRCodeDatabaseInitializer : DropCreateDatabaseAlways<QRCodeContext>
    {
        protected override void Seed(QRCodeContext context)
        {
            GetQRCodes().ForEach(c => context.QRCodes.Add(c));
            context.SaveChanges();
        }

        private static List<QRCode> GetQRCodes()
        {
            var qrCodes = new List<QRCode> {
                new QRCode
                {
                    Id = Guid.NewGuid(),
                    ActivationCode = "ABCD-EFGH-IJKL-MNOP",
                    GeneratedBy= Guid.NewGuid(),
                    GeneratedOn = DateTime.Now
                }
            };

            return qrCodes;
        }
    }
}