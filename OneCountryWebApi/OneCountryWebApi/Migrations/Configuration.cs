namespace OneCountryWebApi.Migrations
{
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<OneCountryWebApi.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(OneCountryWebApi.Models.ApplicationDbContext context)
        {
            context.Roles.AddOrUpdate(new IdentityRole("OneCountry"));
            context.Roles.AddOrUpdate(new IdentityRole("Public"));

            context.Actions.AddOrUpdate(new Models.Action() { ActionName = "Open" });
            context.Actions.AddOrUpdate(new Models.Action() { ActionName = "InProgress" });
            context.Actions.AddOrUpdate(new Models.Action() { ActionName = "Closed" });
            context.Actions.AddOrUpdate(new Models.Action() { ActionName = "Rejected" });
        }
    }
}
