using IRunes.Services.Albums;
using IRunes.Services.Tracks;
using IRunes.Services.Users;
using Microsoft.EntityFrameworkCore;
using SIS.HTTP;
using SIS.MvcFramework;
using System.Collections.Generic;

namespace IRunes
{
    public class StartUp : IMvcApplication
    {
        public void Configure(IList<Route> routeTable)
        {
            new IRunesDbContext().Database.Migrate();
        }

        public void ConfigureServices(IServiceCollection serviceCollection)
        {
            serviceCollection.Add<IUsersService, UsersService>();
            serviceCollection.Add<IAlbumsService, AlbumService>();
            serviceCollection.Add<ITracksService, TrackService>();
        }
    }
}
