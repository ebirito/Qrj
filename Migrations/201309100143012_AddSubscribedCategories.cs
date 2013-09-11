namespace QRJ.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddSubscribedCategories : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CategoryQRCodes",
                c => new
                    {
                        Category_Id = c.Guid(nullable: false),
                        QRCode_Id = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.Category_Id, t.QRCode_Id })
                .ForeignKey("dbo.Categories", t => t.Category_Id, cascadeDelete: true)
                .ForeignKey("dbo.QRCodes", t => t.QRCode_Id, cascadeDelete: true)
                .Index(t => t.Category_Id)
                .Index(t => t.QRCode_Id);
            
        }
        
        public override void Down()
        {
            DropIndex("dbo.CategoryQRCodes", new[] { "QRCode_Id" });
            DropIndex("dbo.CategoryQRCodes", new[] { "Category_Id" });
            DropForeignKey("dbo.CategoryQRCodes", "QRCode_Id", "dbo.QRCodes");
            DropForeignKey("dbo.CategoryQRCodes", "Category_Id", "dbo.Categories");
            DropTable("dbo.CategoryQRCodes");
        }
    }
}
