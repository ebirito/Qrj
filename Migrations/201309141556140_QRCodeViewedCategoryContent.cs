namespace QRJ.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class QRCodeViewedCategoryContent : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.QRCodeViewedCategoryContents",
                c => new
                    {
                        QrCodeId = c.Guid(nullable: false),
                        CategoryContentId = c.Guid(nullable: false),
                        LastViewedOn = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => new { t.QrCodeId, t.CategoryContentId })
                .ForeignKey("dbo.QRCodes", t => t.QrCodeId, cascadeDelete: true)
                .ForeignKey("dbo.CategoryContents", t => t.CategoryContentId, cascadeDelete: true)
                .Index(t => t.QrCodeId)
                .Index(t => t.CategoryContentId);
            
        }
        
        public override void Down()
        {
            DropIndex("dbo.QRCodeViewedCategoryContents", new[] { "CategoryContentId" });
            DropIndex("dbo.QRCodeViewedCategoryContents", new[] { "QrCodeId" });
            DropForeignKey("dbo.QRCodeViewedCategoryContents", "CategoryContentId", "dbo.CategoryContents");
            DropForeignKey("dbo.QRCodeViewedCategoryContents", "QrCodeId", "dbo.QRCodes");
            DropTable("dbo.QRCodeViewedCategoryContents");
        }
    }
}
