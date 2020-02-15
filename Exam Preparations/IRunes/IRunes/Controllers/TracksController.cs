
using IRunes.Services.Albums;
using IRunes.Services.Tracks;
using IRunes.ViewModels.Tracks;
using SIS.HTTP;
using SIS.MvcFramework;
using System;
using System.Collections.Generic;
using System.Text;

namespace IRunes.Controllers
{
    public class TracksController : Controller
    {
        private readonly ITracksService tracksService;
        private readonly IAlbumsService albumService;

        public TracksController(ITracksService tracksService, IAlbumsService albumService)
        {
            this.tracksService = tracksService;
            this.albumService = albumService;
        }

        public HttpResponse Create(string albumId)
        {
            if (!this.IsUserLoggedIn())
            {
                return this.Redirect("/Users/Login");
            }

            var viewModel = new CreateTrackViewModel { AlbumId = albumId };

            return this.View(viewModel);
        }

        [HttpPost]
        public HttpResponse Create(string albumId, string name, string link, decimal price)
        {
            if (!this.IsUserLoggedIn())
            {
                return this.Redirect("/Users/Login");
            }

            if (!this.albumService.IsAlbumExist(albumId))
            {
                return this.View();
            }

            if (name.Length < 4 || name.Length > 20)
            {
                return this.View();
            }

            if (!link.Contains("http"))
            {
                return this.View();
            }

            if (price < 0)
            {
                return this.View();
            }

            this.tracksService.CreateTrack(albumId, name, link, price);

            return this.Redirect("/Albums/Details?id=" + albumId);
        }

        public HttpResponse Details(string albumId, string trackId)
        {
            if (!this.IsUserLoggedIn())
            {
                return this.Redirect("/Users/Login");
            }

            DetailsTrackViewModel trackViewModel = this.tracksService.GetTrackDetails(trackId);

            return this.View(trackViewModel);
        }
    }
}
