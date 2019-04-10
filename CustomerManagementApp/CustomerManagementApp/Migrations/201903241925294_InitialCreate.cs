namespace CustomerManagementApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CustomerModels",
                c => new
                    {
                        CustomerID = c.Int(nullable: false, identity: true),
                        AddressLine1 = c.String(),
                        AddressLine2 = c.String(),
                        AddressLine3 = c.String(),
                        City = c.String(),
                        Country = c.String(),
                        EmailAddress = c.String(),
                        FirstName = c.String(),
                        MobileNumber = c.String(),
                        LandlineNumber = c.String(),
                        SurName = c.String(),
                    })
                .PrimaryKey(t => t.CustomerID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.CustomerModels");
        }
    }
}
