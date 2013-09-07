namespace QRJ.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddUrlType : DbMigration
    {
        public override void Up()
        {
            AddColumn("QrCodes", "UrlType", c => c.Int());
        }
        
        public override void Down()
        {
            DropColumn("QRCodes", "UrlType");
        }
    }
}
