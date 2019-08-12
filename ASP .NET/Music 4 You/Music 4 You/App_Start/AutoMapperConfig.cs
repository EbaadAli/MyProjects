using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
// new...
using AutoMapper;

namespace Assignment8
{
    public static class AutoMapperConfig
    {
        public static void RegisterMappings()
        {
            // Add map creation statements here
            // Mapper.CreateMap< FROM , TO >();

            // Disable AutoMapper v4.2.x warnings
#pragma warning disable CS0618

            // Attention - AutoMapper create map statements

            // Mapper.CreateMap<Models.Genre, Controllers.GenreBase>();

            Mapper.CreateMap<Models.Track, Controllers.TrackBase>();
            Mapper.CreateMap<Models.Track, Controllers.TrackAddForm>();
            Mapper.CreateMap<Models.Genre, Controllers.GenreAdd>();
            Mapper.CreateMap<Models.Artist, Controllers.ArtistBase>();
            Mapper.CreateMap<Models.Artist, Controllers.ArtistWithDetail>();
            Mapper.CreateMap<Models.Album, Controllers.AlbumWithDetail>();
            Mapper.CreateMap<Models.Track, Controllers.TrackWithDetail>();
            Mapper.CreateMap<Models.Artist, Controllers.ArtistAddForm>();
            Mapper.CreateMap<Models.Album, Controllers.AlbumBase>();
            Mapper.CreateMap<Models.Genre, Controllers.GenreBase>();
            Mapper.CreateMap<Models.Album, Controllers.TrackBase>();
            Mapper.CreateMap<Controllers.TrackAddForm, Models.Track>();
            Mapper.CreateMap<Controllers.AlbumAddForm, Models.Track>();
            Mapper.CreateMap<Controllers.AlbumBase, Models.Album>();
            Mapper.CreateMap<Controllers.ArtistAddForm, Models.Artist>();
            Mapper.CreateMap<Controllers.TrackAdd, Models.Track>();
            Mapper.CreateMap<Controllers.AlbumAddForm, Models.Album>();
            Mapper.CreateMap<Controllers.AlbumAdd, Models.Album>();
            Mapper.CreateMap<Models.Artist, Controllers.ArtistWithMediaID>();
            Mapper.CreateMap<Models.MediaItem, Controllers.MediaItemBase>();
            Mapper.CreateMap<Models.MediaItem, Controllers.MediaItemContent>();
            Mapper.CreateMap<Models.RegisterViewModel, Models.RegisterViewModelForm>();
           

#pragma warning restore CS0618
        }
    }
}