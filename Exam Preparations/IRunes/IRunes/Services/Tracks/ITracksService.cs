using IRunes.ViewModels.Tracks;
using System;
using System.Collections.Generic;
using System.Text;

namespace IRunes.Services.Tracks
{
    public interface ITracksService
    {
        void CreateTrack(string albumId, string name, string link, decimal price);

        DetailsTrackViewModel GetTrackDetails(string trackId);
    }
}
