namespace CustomerManagementApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialMigration : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CustomerModels", "Title", c => c.String());
            AlterColumn("dbo.CustomerModels", "AddressLine1", c => c.String(nullable: false));
            AlterColumn("dbo.CustomerModels", "City", c => c.String(nullable: false));
            AlterColumn("dbo.CustomerModels", "Country", c => c.String(nullable: false));
            AlterColumn("dbo.CustomerModels", "EmailAddress", c => c.String(nullable: false));
            AlterColumn("dbo.CustomerModels", "FirstName", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.CustomerModels", "FirstName", c => c.String());
            AlterColumn("dbo.CustomerModels", "EmailAddress", c => c.String());
            AlterColumn("dbo.CustomerModels", "Country", c => c.String());
            AlterColumn("dbo.CustomerModels", "City", c => c.String());
            AlterColumn("dbo.CustomerModels", "AddressLine1", c => c.String());
            DropColumn("dbo.CustomerModels", "Title");
        }
    }
}
