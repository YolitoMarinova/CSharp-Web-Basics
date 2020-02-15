using IRunes.Services.Albums;
using IRunes.ViewModels.Albums;
using SIS.HTTP;
using SIS.MvcFramework;
using System.Collections.Generic;

namespace IRunes.Controllers
{
    public class AlbumsController : Controller
    {
        private readonly IAlbumsService albumsService;

        public AlbumsController(IAlbumsService albumsService)
        {
            this.albumsService = albumsService;
        }

        public HttpResponse All()
        {
            if (!this.IsUserLoggedIn())
            {
                return this.Redirect("/Users/Login");
            }

            var allAlbums = this.albumsService.GettAllAlbums();

            return this.View(allAlbums);
        }

        public HttpResponse Create()
        {
            if (!this.IsUserLoggedIn())
            {
                return this.Redirect("/Users/Login");
            }

            return this.View();
        }

        [HttpPost]
        public HttpResponse Create(string name, string cover)
        {
            if (!this.IsUserLoggedIn())
            {
                return this.Redirect("/Users/Login");
            }

            if (name.Length < 4 || name.Length > 20)
            {
                return this.View();
            }

            if (string.IsNullOrWhiteSpace(cover))
            {
                return this.View();
            }

            this.albumsService.CreateAlbum(name, cover);

            return this.Redirect("All");
        }

        public HttpResponse Details(string id)
        {
            if (!this.IsUserLoggedIn())
            {
                return this.Redirect("/Users/Login");
            }

            var albumWithDetails = this.albumsService.GetAlbumWithDetails(id);

            return this.View(albumWithDetails);
        }
    }
}
