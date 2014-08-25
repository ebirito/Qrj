namespace QRJ.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddHoroscopeSignSetQrCode : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.HoroscopeSigns",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Sign = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.HoroscopeQrCodes",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        HoroscopeSignId = c.Guid(nullable: false),
                        HoroscopeSetId = c.Guid(nullable: false),
                        LongURL = c.String(),
                        ShortURL = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.HoroscopeSigns", t => t.HoroscopeSignId, cascadeDelete: true)
                .ForeignKey("dbo.HoroscopeSets", t => t.HoroscopeSetId, cascadeDelete: true)
                .Index(t => t.HoroscopeSignId)
                .Index(t => t.HoroscopeSetId);
            
            CreateTable(
                "dbo.HoroscopeSets",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        SetNumber = c.Int(nullable: false),
                        GeneratedOn = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropIndex("dbo.HoroscopeQrCodes", new[] { "HoroscopeSetId" });
            DropIndex("dbo.HoroscopeQrCodes", new[] { "HoroscopeSignId" });
            DropForeignKey("dbo.HoroscopeQrCodes", "HoroscopeSetId", "dbo.HoroscopeSets");
            DropForeignKey("dbo.HoroscopeQrCodes", "HoroscopeSignId", "dbo.HoroscopeSigns");
            DropTable("dbo.HoroscopeSets");
            DropTable("dbo.HoroscopeQrCodes");
            DropTable("dbo.HoroscopeSigns");
        }
    }
}
