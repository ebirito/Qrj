namespace QRJ.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.QRCodes",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        ActivationCode = c.String(),
                        FilePath = c.String(),
                        GeneratedOn = c.DateTime(nullable: false),
                        GeneratedBy = c.Guid(nullable: false),
                        ActivatedOn = c.DateTime(),
                        ActivatedBy = c.Guid(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.QRCodes");
        }
    }
}
