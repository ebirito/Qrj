namespace QRJ.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class QRCodeViewedCategoryContentAddExpiresOn : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.QRCodeViewedCategoryContents", "ExpiresOn", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.QRCodeViewedCategoryContents", "ExpiresOn");
        }
    }
}
