using IRunes.Models;
using IRunes.ViewModels.Tracks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IRunes.Services.Tracks
{
    public class TrackService : ITracksService
    {
        private readonly IRunesDbContext db;

        public TrackService(IRunesDbContext db)
        {
            this.db = db;
        }

        public void CreateTrack(string albumId, string name, string link, decimal price)
        {
            var track = new Track
            {
                AlbumId = albumId,
                Name = name,
                Link = link,
                Price = price
            };

            this.db.Tracks.Add(track);

            var allTrackPricesSum = this.db.Tracks
                .Where(x => x.AlbumId == albumId)
                .Sum(x => x.Price) + price;
            var album = this.db.Albums.Find(albumId);

            album.Price = allTrackPricesSum - (allTrackPricesSum * 0.13m);

            this.db.SaveChanges();
        }

        public DetailsTrackViewModel GetTrackDetails(string trackId)
        {
            return this.db.Tracks.Where(t => t.Id == trackId).Select(t => new DetailsTrackViewModel
            {
                AlbumId = t.AlbumId,
                Name = t.Name,
                Link = t.Link,
                Price = t.Price
            })
                .FirstOrDefault();
        }
    }
}
