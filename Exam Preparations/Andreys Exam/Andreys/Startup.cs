namespace Andreys.App
{
    using System.Collections.Generic;

    using Data;

    using SIS.HTTP;
    using SIS.MvcFramework;

    using Andreys.Services.Users;
    using Andreys.Services.Products;

    public class Startup : IMvcApplication
    {
        public void Configure(IList<Route> serverRoutingTable)
        {
            using (var db = new AndreysDbContext())
            {
                db.Database.EnsureCreated();
            }
        }

        public void ConfigureServices(IServiceCollection serviceCollection)
        {
            serviceCollection.Add<IUsersService, UsersService>();
            serviceCollection.Add<IProductsService, ProductsService>();
        }
    }
}
