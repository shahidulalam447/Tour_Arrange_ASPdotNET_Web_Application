namespace Tour_Arrange.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ScriptA : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BookingEntries",
                c => new
                    {
                        BookingEntryId = c.Int(nullable: false, identity: true),
                        ClientId = c.Int(nullable: false),
                        SpotId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.BookingEntryId)
                .ForeignKey("dbo.Clients", t => t.ClientId, cascadeDelete: true)
                .ForeignKey("dbo.Spots", t => t.SpotId, cascadeDelete: true)
                .Index(t => t.ClientId)
                .Index(t => t.SpotId);
            
            CreateTable(
                "dbo.Clients",
                c => new
                    {
                        ClientId = c.Int(nullable: false, identity: true),
                        ClientName = c.String(nullable: false, maxLength: 50),
                        BirthDate = c.DateTime(nullable: false),
                        Age = c.Int(nullable: false),
                        Picture = c.String(),
                        MaritalStatus = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ClientId);
            
            CreateTable(
                "dbo.Spots",
                c => new
                    {
                        SpotId = c.Int(nullable: false, identity: true),
                        SpotName = c.String(nullable: false, maxLength: 50),
                        SpotPicture = c.String(),
                    })
                .PrimaryKey(t => t.SpotId);
            
            CreateTable(
                "dbo.Departments",
                c => new
                    {
                        DepartmentId = c.Int(nullable: false, identity: true),
                        DepartmentName = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.DepartmentId);
            
            CreateTable(
                "dbo.ManagementEntries",
                c => new
                    {
                        ManagementEntryId = c.Int(nullable: false, identity: true),
                        EmployeeId = c.Int(nullable: false),
                        DepartmentId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ManagementEntryId)
                .ForeignKey("dbo.Departments", t => t.DepartmentId, cascadeDelete: true)
                .ForeignKey("dbo.Employees", t => t.EmployeeId, cascadeDelete: true)
                .Index(t => t.EmployeeId)
                .Index(t => t.DepartmentId);
            
            CreateTable(
                "dbo.Employees",
                c => new
                    {
                        EmployeeId = c.Int(nullable: false, identity: true),
                        EmployeeName = c.String(nullable: false, maxLength: 50),
                        BirthDate = c.DateTime(nullable: false),
                        JoinDate = c.DateTime(nullable: false, storeType: "date"),
                        Email = c.String(nullable: false),
                        EmployeePicture = c.String(),
                    })
                .PrimaryKey(t => t.EmployeeId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ManagementEntries", "EmployeeId", "dbo.Employees");
            DropForeignKey("dbo.ManagementEntries", "DepartmentId", "dbo.Departments");
            DropForeignKey("dbo.BookingEntries", "SpotId", "dbo.Spots");
            DropForeignKey("dbo.BookingEntries", "ClientId", "dbo.Clients");
            DropIndex("dbo.ManagementEntries", new[] { "DepartmentId" });
            DropIndex("dbo.ManagementEntries", new[] { "EmployeeId" });
            DropIndex("dbo.BookingEntries", new[] { "SpotId" });
            DropIndex("dbo.BookingEntries", new[] { "ClientId" });
            DropTable("dbo.Employees");
            DropTable("dbo.ManagementEntries");
            DropTable("dbo.Departments");
            DropTable("dbo.Spots");
            DropTable("dbo.Clients");
            DropTable("dbo.BookingEntries");
        }
    }
}
