namespace CustomerManagementApp.Migrations
{
    using System.Data.Entity.Migrations;

    internal sealed class Configuration : DbMigrationsConfiguration<CustomerManagementApp.Models.CustomerDBContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "CustomerManagementApp.Models.CustomerDBContext";
        }

        protected override void Seed(CustomerManagementApp.Models.CustomerDBContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.
        }
    }
}
