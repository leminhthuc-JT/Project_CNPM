namespace G6_Website_BQA.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ANHSANPHAMs",
                c => new
                    {
                        MASP = c.Int(nullable: false),
                        ANH = c.String(nullable: false, maxLength: 128),
                        TENANH = c.String(),
                    })
                .PrimaryKey(t => new { t.MASP, t.ANH })
                .ForeignKey("dbo.SANPHAMs", t => t.MASP, cascadeDelete: true)
                .Index(t => t.MASP);
            
            CreateTable(
                "dbo.SANPHAMs",
                c => new
                    {
                        MASP = c.Int(nullable: false, identity: true),
                        TENSP = c.String(),
                        SOLUONG = c.Int(nullable: false),
                        MOTA = c.String(),
                        MALOAISP = c.Int(nullable: false),
                        MADM = c.Int(nullable: false),
                        MATH = c.Int(nullable: false),
                        MANCC = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.MASP)
                .ForeignKey("dbo.DANHMUCs", t => t.MADM, cascadeDelete: true)
                .ForeignKey("dbo.LOAISPs", t => t.MALOAISP, cascadeDelete: true)
                .ForeignKey("dbo.NHACUNGCAPs", t => t.MANCC, cascadeDelete: true)
                .ForeignKey("dbo.THUONGHIEUx", t => t.MATH, cascadeDelete: true)
                .Index(t => t.MALOAISP)
                .Index(t => t.MADM)
                .Index(t => t.MATH)
                .Index(t => t.MANCC);
            
            CreateTable(
                "dbo.CHITIETHDs",
                c => new
                    {
                        MAHD = c.Int(nullable: false),
                        MASP = c.Int(nullable: false),
                        SOLUONG = c.Int(nullable: false),
                        DONGIA = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => new { t.MAHD, t.MASP })
                .ForeignKey("dbo.HOADONs", t => t.MAHD, cascadeDelete: true)
                .ForeignKey("dbo.SANPHAMs", t => t.MASP, cascadeDelete: true)
                .Index(t => t.MAHD)
                .Index(t => t.MASP);
            
            CreateTable(
                "dbo.HOADONs",
                c => new
                    {
                        MAHD = c.Int(nullable: false, identity: true),
                        NGAYLAP = c.DateTime(),
                        MAGG = c.Int(nullable: false),
                        TONGTIEN = c.Decimal(nullable: false, precision: 18, scale: 2),
                        PTTHANHTOAN = c.String(),
                        TRANGTHAI = c.String(),
                    })
                .PrimaryKey(t => t.MAHD)
                .ForeignKey("dbo.GIAMGIAs", t => t.MAGG, cascadeDelete: true)
                .Index(t => t.MAGG);
            
            CreateTable(
                "dbo.GIAMGIAs",
                c => new
                    {
                        MAGG = c.Int(nullable: false, identity: true),
                        TENMGG = c.String(),
                        MUCGIAM = c.Int(nullable: false),
                        MOTA = c.String(),
                        NGAYBD = c.DateTime(nullable: false),
                        NGAYKT = c.DateTime(nullable: false),
                        MALOAISP = c.Int(nullable: false),
                        MASK = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.MAGG)
                .ForeignKey("dbo.LOAISPs", t => t.MALOAISP)
                .ForeignKey("dbo.SUKIENs", t => t.MASK, cascadeDelete: true)
                .Index(t => t.MALOAISP)
                .Index(t => t.MASK);
            
            CreateTable(
                "dbo.LOAISPs",
                c => new
                    {
                        MALOAISP = c.Int(nullable: false, identity: true),
                        TENLOAISP = c.String(),
                    })
                .PrimaryKey(t => t.MALOAISP);
            
            CreateTable(
                "dbo.SUKIENs",
                c => new
                    {
                        MASK = c.Int(nullable: false, identity: true),
                        TENSK = c.String(),
                        NGAYBD = c.DateTime(nullable: false),
                        NGAYKT = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.MASK);
            
            CreateTable(
                "dbo.ANHSUKIENs",
                c => new
                    {
                        MASK = c.Int(nullable: false),
                        ANH = c.String(nullable: false, maxLength: 128),
                        TENANH = c.String(),
                    })
                .PrimaryKey(t => new { t.MASK, t.ANH })
                .ForeignKey("dbo.SUKIENs", t => t.MASK, cascadeDelete: true)
                .Index(t => t.MASK);
            
            CreateTable(
                "dbo.CHITIETSPs",
                c => new
                    {
                        MASP = c.Int(nullable: false),
                        MAU = c.String(nullable: false, maxLength: 128),
                        KICHTHUOC = c.String(nullable: false, maxLength: 128),
                        DONGIA = c.Decimal(nullable: false, precision: 18, scale: 2),
                        SOLUONG = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.MASP, t.MAU, t.KICHTHUOC })
                .ForeignKey("dbo.SANPHAMs", t => t.MASP, cascadeDelete: true)
                .Index(t => t.MASP);
            
            CreateTable(
                "dbo.DANHMUCs",
                c => new
                    {
                        MADM = c.Int(nullable: false, identity: true),
                        TENDM = c.String(),
                    })
                .PrimaryKey(t => t.MADM);
            
            CreateTable(
                "dbo.NHACUNGCAPs",
                c => new
                    {
                        MANCC = c.Int(nullable: false, identity: true),
                        TENNCC = c.String(),
                    })
                .PrimaryKey(t => t.MANCC);
            
            CreateTable(
                "dbo.THUONGHIEUx",
                c => new
                    {
                        MATH = c.Int(nullable: false, identity: true),
                        TENTH = c.String(),
                    })
                .PrimaryKey(t => t.MATH);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SANPHAMs", "MATH", "dbo.THUONGHIEUx");
            DropForeignKey("dbo.SANPHAMs", "MANCC", "dbo.NHACUNGCAPs");
            DropForeignKey("dbo.SANPHAMs", "MALOAISP", "dbo.LOAISPs");
            DropForeignKey("dbo.SANPHAMs", "MADM", "dbo.DANHMUCs");
            DropForeignKey("dbo.CHITIETSPs", "MASP", "dbo.SANPHAMs");
            DropForeignKey("dbo.CHITIETHDs", "MASP", "dbo.SANPHAMs");
            DropForeignKey("dbo.GIAMGIAs", "MASK", "dbo.SUKIENs");
            DropForeignKey("dbo.ANHSUKIENs", "MASK", "dbo.SUKIENs");
            DropForeignKey("dbo.GIAMGIAs", "MALOAISP", "dbo.LOAISPs");
            DropForeignKey("dbo.HOADONs", "MAGG", "dbo.GIAMGIAs");
            DropForeignKey("dbo.CHITIETHDs", "MAHD", "dbo.HOADONs");
            DropForeignKey("dbo.ANHSANPHAMs", "MASP", "dbo.SANPHAMs");
            DropIndex("dbo.CHITIETSPs", new[] { "MASP" });
            DropIndex("dbo.ANHSUKIENs", new[] { "MASK" });
            DropIndex("dbo.GIAMGIAs", new[] { "MASK" });
            DropIndex("dbo.GIAMGIAs", new[] { "MALOAISP" });
            DropIndex("dbo.HOADONs", new[] { "MAGG" });
            DropIndex("dbo.CHITIETHDs", new[] { "MASP" });
            DropIndex("dbo.CHITIETHDs", new[] { "MAHD" });
            DropIndex("dbo.SANPHAMs", new[] { "MANCC" });
            DropIndex("dbo.SANPHAMs", new[] { "MATH" });
            DropIndex("dbo.SANPHAMs", new[] { "MADM" });
            DropIndex("dbo.SANPHAMs", new[] { "MALOAISP" });
            DropIndex("dbo.ANHSANPHAMs", new[] { "MASP" });
            DropTable("dbo.THUONGHIEUx");
            DropTable("dbo.NHACUNGCAPs");
            DropTable("dbo.DANHMUCs");
            DropTable("dbo.CHITIETSPs");
            DropTable("dbo.ANHSUKIENs");
            DropTable("dbo.SUKIENs");
            DropTable("dbo.LOAISPs");
            DropTable("dbo.GIAMGIAs");
            DropTable("dbo.HOADONs");
            DropTable("dbo.CHITIETHDs");
            DropTable("dbo.SANPHAMs");
            DropTable("dbo.ANHSANPHAMs");
        }
    }
}
