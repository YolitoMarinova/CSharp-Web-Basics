using IRunes.ViewModels.Albums;
using System.Collections.Generic;

namespace IRunes.Services.Albums
{
    public interface IAlbumsService
    {
        AllAlbumsViewModel GettAllAlbums();

        void CreateAlbum(string name, string cover);

        DetailsAlbumViewModel GetAlbumWithDetails(string id);

        bool IsAlbumExist(string id);
    }
}
