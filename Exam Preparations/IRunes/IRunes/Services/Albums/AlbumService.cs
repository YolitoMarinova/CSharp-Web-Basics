using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IRunes.Models;
using IRunes.ViewModels.Albums;

namespace IRunes.Services.Albums
{
    public class AlbumService : IAlbumsService
    {
        private readonly IRunesDbContext db;

        public AlbumService(IRunesDbContext db)
        {
            this.db = db;
        }

        public bool IsAlbumExist(string id)
            => this.db.Albums.Any(a => a.Id == id);

        public void CreateAlbum(string name, string cover)
        {
            var album = new Album
            {
                Name = name,
                Cover = cover
            };

            this.db.Albums.Add(album);
            this.db.SaveChanges();
        }

        public DetailsAlbumViewModel GetAlbumWithDetails(string id)
        {
            var model = this.db.Albums.Where(a => a.Id == id).Select(a => new DetailsAlbumViewModel
            {
                Id = a.Id,
                Name = a.Name,
                Cover = a.Cover,
                Price = a.Price,
                Tracks = a.Tracks.Select(t => new TrackAlbumsDetailsViewModel
                {
                    Id = t.Id,
                    Name = t.Name
                })
            })
                .FirstOrDefault();

            return model;
        }

        public AllAlbumsViewModel GettAllAlbums()
        {
            var allAlbums = new AllAlbumsViewModel();

            allAlbums.Albums = this.db.Albums.Select(a => new AlbumViewModel
            {
                Id = a.Id,
                Name = a.Name
            })
            .ToList();

            return allAlbums;
        }
    }
}
