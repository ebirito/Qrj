namespace QRJ.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddFilePath : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CategoryContents", "FilePath", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.CategoryContents", "FilePath");
        }
    }
}
