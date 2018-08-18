namespace WebApplication2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class druga : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Proizvod", "Slika", c => c.Binary());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Proizvod", "Slika", c => c.Binary(storeType: "image"));
        }
    }
}
