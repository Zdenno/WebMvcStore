namespace WebApplication2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class prva : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Kategorija",
                c => new
                    {
                        KategorijaId = c.Int(nullable: false, identity: true),
                        NazivKategorije = c.String(nullable: false, maxLength: 40),
                        OpisKategorije = c.String(nullable: false, maxLength: 100),
                        DatumKreiranja = c.DateTime(nullable: false, storeType: "date"),
                    })
                .PrimaryKey(t => t.KategorijaId);
            
            CreateTable(
                "dbo.Proizvod",
                c => new
                    {
                        ProizvodId = c.Int(nullable: false, identity: true),
                        KategorijaId = c.Int(nullable: false),
                        NazivProizvodjaca = c.String(nullable: false, maxLength: 40),
                        Cena = c.Decimal(nullable: false, precision: 12, scale: 3),
                        KolicinaNaLageru = c.Int(nullable: false),
                        Slika = c.Binary(storeType: "image"),
                        TipSlike = c.String(),
                        OglasPostavio = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.ProizvodId)
                .ForeignKey("dbo.Kategorija", t => t.KategorijaId)
                .Index(t => t.KategorijaId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Proizvod", "KategorijaId", "dbo.Kategorija");
            DropIndex("dbo.Proizvod", new[] { "KategorijaId" });
            DropTable("dbo.Proizvod");
            DropTable("dbo.Kategorija");
        }
    }
}
