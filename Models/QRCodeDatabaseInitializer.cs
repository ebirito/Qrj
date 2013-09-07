using System;
using System.Collections.Generic;
using System.Data.Entity;

namespace QRJ.Models
{
    public class QRCodeDatabaseInitializer : DropCreateDatabaseAlways<QRCodeContext>
    {
        private static Guid _categoryId = Guid.NewGuid();

        protected override void Seed(QRCodeContext context)
        {
            GetQRCodes().ForEach(c => context.QRCodes.Add(c));
            GetCategoryContents().ForEach(c => context.CategoryContents.Add(c));
            context.SaveChanges();
        }

        private static List<QRCode> GetQRCodes()
        {
            var qrCodes = new List<QRCode> {
                new QRCode
                {
                    Id = Guid.NewGuid(),
                    ActivationCode = "ABCD-EFGH-IJKL-MNOP",
                    GeneratedBy = Guid.NewGuid(),
                    GeneratedOn = DateTime.Now
                }
            };

            return qrCodes;
        }

        private static List<CategoryContent> GetCategoryContents()
        {
            var categoryContents = new List<CategoryContent> {
                new CategoryContent
                {
                    Id = Guid.NewGuid(),
                    Name = "Libra",
                    Category =  new Category
                        {
                            Id = Guid.NewGuid(),
                            Name= "Horoscope",
                            Frequency = Frequency.Daily
                        }
                }
            };

            return categoryContents;
        }
    }
}