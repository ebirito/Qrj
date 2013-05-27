namespace QRJ.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddProductName : DbMigration
    {
        public override void Up()
        {
            AddColumn("QrCodes", "ProductName", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("QRCodes", "ProductName");
        }
    }
}
