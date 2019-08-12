using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
// new...
using AutoMapper;
using Assignment8.Models;
using System.Data.Entity.Validation;
using System.Security.Claims;

namespace Assignment8.Controllers
{
    
    public class Manager
    {
        // Reference to the data context
        private ApplicationDbContext ds = new ApplicationDbContext();
        public UserAccount userAccount { get; private set; }
       


        public Manager()
        {
            // If necessary, add constructor code here
            userAccount = new UserAccount(HttpContext.Current.User as ClaimsPrincipal);
            // Turn off the Entity Framework (EF) proxy creation features
            // We do NOT want the EF to track changes - we'll do that ourselves
            ds.Configuration.ProxyCreationEnabled = false;

            // Also, turn off lazy loading...
            // We want to retain control over fetching related objects
            ds.Configuration.LazyLoadingEnabled = false;
        }

        // Add methods below
        // Controllers will call these methods
        // Ensure that the methods accept and deliver ONLY view model objects and collections
        // The collection return type is almost always IEnumerable<T>
        // New "UserAccount" class for the authenticated user
        // Includes many convenient members to make it easier to render user account info
        // Study the properties and methods, and think about how you could use it
        public class UserAccount
        {
            // Constructor, pass in the security principal
            public UserAccount(ClaimsPrincipal user)
            {
                if (HttpContext.Current.Request.IsAuthenticated)
                {
                    Principal = user;

                    // Extract the role claims
                    RoleClaims = user.Claims.Where(c => c.Type == ClaimTypes.Role).Select(c => c.Value);

                    // User name
                    Name = user.Identity.Name;

                    // Extract the given name(s); if null or empty, then set an initial value
                    string gn = user.Claims.SingleOrDefault(c => c.Type == ClaimTypes.GivenName).Value;
                    if (string.IsNullOrEmpty(gn)) { gn = "(empty given name)"; }
                    GivenName = gn;

                    // Extract the surname; if null or empty, then set an initial value
                    string sn = user.Claims.SingleOrDefault(c => c.Type == ClaimTypes.Surname).Value;
                    if (string.IsNullOrEmpty(sn)) { sn = "(empty surname)"; }
                    Surname = sn;

                    IsAuthenticated = true;
                    IsAdmin = user.HasClaim(ClaimTypes.Role, "Admin") ? true : false;
                }
                else
                {
                    RoleClaims = new List<string>();
                    Name = "anonymous";
                    GivenName = "Unauthenticated";
                    Surname = "Anonymous";
                    IsAuthenticated = false;
                    IsAdmin = false;
                }

                NamesFirstLast = $"{GivenName} {Surname}";
                NamesLastFirst = $"{Surname}, {GivenName}";
            }

            // Public properties
            public ClaimsPrincipal Principal { get; private set; }
            public IEnumerable<string> RoleClaims { get; private set; }

            public string Name { get; set; }

            public string GivenName { get; private set; }
            public string Surname { get; private set; }

            public string NamesFirstLast { get; private set; }
            public string NamesLastFirst { get; private set; }

            public bool IsAuthenticated { get; private set; }

            // Add other role-checking properties here as needed
            public bool IsAdmin { get; private set; }

            public bool HasRoleClaim(string value)
            {
                if (!IsAuthenticated) { return false; }
                return Principal.HasClaim(ClaimTypes.Role, value) ? true : false;
            }

            public bool HasClaim(string type, string value)
            {
                if (!IsAuthenticated) { return false; }
                return Principal.HasClaim(type, value) ? true : false;
            }
        }

        // Suggested naming convention: Entity + task/action
        // For example:
        // ProductGetAll()
        // ProductGetById()
        // ProductAdd()
        // ProductEdit()
        // ProductDelete()

        public IEnumerable<AlbumBase> AlbumGetAll()
        {
            return Mapper.Map<IEnumerable<AlbumBase>>(ds.Albums.OrderBy(c => c.Name));
        }

        public AlbumWithDetail AlbumGetById(int id)
        {
            //Attempt to fetch the object
            var o = ds.Albums.Include("Tracks").Include("Artists").SingleOrDefault(t => t.Id == id);

            //Return the Result, or null if not found
            return (o == null) ? null : Mapper.Map<AlbumWithDetail>(o);
        }


        public IEnumerable<ArtistBase> ArtistGetAll()
        {
            return Mapper.Map<IEnumerable<ArtistBase>>(ds.Artists.OrderBy(c => c.Name));
        }

        public ArtistWithDetail ArtistGetById(int id)
        {
            //Attempt to fetch the object
            var o = ds.Artists.Include("Albums").SingleOrDefault(t => t.Id == id);

            //Return the Result, or null if not found
            return (o == null) ? null : Mapper.Map<ArtistWithDetail>(o);
        }

        public IEnumerable<TrackBase> TrackGetAll()
        {
            return Mapper.Map<IEnumerable<TrackBase>>(ds.Tracks.OrderBy(c => c.Name));
        }

        public TrackWithDetail TrackGetById(int id)
        {
            //Attempt to fetch the object
            var o = ds.Tracks.Include("Albums").SingleOrDefault(t => t.Id == id);

            //Return the Result, or null if not found
            return (o == null) ? null : Mapper.Map<TrackWithDetail>(o);
        }

        public IEnumerable<GenreBase> GenreGetAll()
        {
            return Mapper.Map<IEnumerable<GenreBase>>(ds.Genres.OrderBy(c => c.Id));
        }

        public GenreBase GenreGetById(int id)
        {
            //Attempt to fetch the object
            var o = ds.Genres.SingleOrDefault(t => t.Id == id);

            //Return the Result, or null if not found
            return (o == null) ? null : Mapper.Map<GenreBase>(o);
        }

        public AlbumWithDetail AlbumGetAllDetails(int id)
        {
            return Mapper.Map<AlbumWithDetail>(ds.Albums.SingleOrDefault(v => v.Id == id));
        }

        public ArtistWithDetail ArtistGetAllDetails(int id)
        {
            return Mapper.Map<ArtistWithDetail>(ds.Artists.SingleOrDefault(v => v.Id == id));

        }

        public MediaItemContent ArtistMediaItemGetById(string stringId)
        {
            var o = ds.MediaItems.SingleOrDefault(p => p.StringId == stringId);

            return (o == null) ? null : Mapper.Map<MediaItemContent>(o);
        }

        public ArtistWithMediaID ArtistGetByIdWithMediaItemInfo(int id)
        {
            var o = ds.Artists.Include("MediaItems").SingleOrDefault(p => p.Id == id);

            return (o == null) ? null : Mapper.Map<ArtistWithMediaID>(o);
        }
        public TrackWithDetail TrackGetAllDetails(int id)
        {
            return Mapper.Map<TrackWithDetail>(ds.Tracks.SingleOrDefault(v => v.Id == id));
        }

        public Artist ArtistAddNew(ArtistAdd newItem)
        {
         
            var addedItem = ds.Artists.Add(Mapper.Map<Artist>(newItem));

            addedItem.Genre = newItem.Genre;
            addedItem.Executive = userAccount.Name;
            addedItem.BirthOrStartDate = newItem.BirthOrStartDate;

            ds.SaveChanges();

            return (addedItem == null) ? null : Mapper.Map<Artist>(addedItem);
        }

        public bool ArtistDelete(int id)
        {
            var item= ds.Artists.Find(id);

            if (item == null)
            {
                return false;
            }
            else
            {
                ds.Artists.Remove(item);
                ds.SaveChanges();

                return true;
            }
        }
       

        public ArtistBase ArtistMediaItemAdd(MediaItemAdd newItem)
        { 
            var a = ds.Artists.Find(newItem.ArtistId);

            if (a == null)
            {
                return null;
            }
            else
            {
                // Attempt to add the new item
                var addedItem = new MediaItem();
                ds.MediaItems.Add(addedItem);

                addedItem.Caption = newItem.Caption;
                addedItem.Artist = a;

                // Handle the uploaded photo...

                // First, extract the bytes from the HttpPostedFile object
                byte[] photoBytes = new byte[newItem.MediaItemUpload.ContentLength];
                newItem.MediaItemUpload.InputStream.Read(photoBytes, 0, newItem.MediaItemUpload.ContentLength);

                // Then, configure the new object's properties
                addedItem.Content = photoBytes;
                addedItem.ContentType = newItem.MediaItemUpload.ContentType;

                ds.SaveChanges();

                return (addedItem == null) ? null : Mapper.Map<ArtistBase>(a);
            }
        }

        public Album AlbumAdd(AlbumAdd newItem)
        {

            // Attempt to add the new item
            var addedItem = ds.Albums.Add(Mapper.Map<Album>(newItem));

            addedItem.Genre = newItem.Genre;

            addedItem.Coordinator = userAccount.Name;
            ds.SaveChanges();

            return (addedItem == null) ? null : Mapper.Map<Album>(addedItem);
        }

        public bool AlbumDelete(int id)
        {
            // Attempt to fetch the object to be deleted
            var item = ds.Artists.Find(id);

            if (item == null)
            {
                return false;
            }
            else
            {
                // Remove the object
                ds.Artists.Remove(item);
                ds.SaveChanges();

                return true;
            }
        }
        public TrackBase TrackAddNew(TrackAdd newItem)
        {
            var addedItem = ds.Tracks.Add(Mapper.Map<Track>(newItem));

            addedItem.Genre = newItem.Genre;
            addedItem.Clerk = userAccount.Name;

            // First, extract the bytes from the HttpPostedFile object
            byte[] photoBytes = new byte[newItem.AudioUpload.ContentLength];
            newItem.AudioUpload.InputStream.Read(photoBytes, 0, newItem.AudioUpload.ContentLength);

            // Then, configure the new object's properties
            addedItem.Audio = photoBytes;
            addedItem.AudioContentType = newItem.AudioUpload.ContentType;

            ds.SaveChanges();

            return (addedItem == null) ? null : Mapper.Map<TrackBase>(addedItem);
        }

        public bool TrackDelete(int id)
        {
            var item = ds.Artists.Find(id);

            if (item == null)
                return false;
            else
            {
                ds.Artists.Remove(item);
                ds.SaveChanges();

                return true;
            }
        }
        public TrackAudio TrackAudioGetById(int id)
        {
            var o = ds.Tracks.Find(id);

            return (o == null) ? null : Mapper.Map<TrackAudio>(o);
        }

       public Album AlbumAddNew(AlbumAdd newItem)
        {
            var addeditem = ds.Albums.Add(Mapper.Map<Album>(newItem));

            addeditem.Genre = newItem.Genre;
            addeditem.Coordinator = userAccount.Name;

            ds.SaveChanges();

            return (addeditem == null) ? null : Mapper.Map<Album>(addeditem);

        }
 
        public TrackWithDetail TrackEdit(TrackEdit newItem)
        {
            var o = ds.Tracks.Include("Albums").SingleOrDefault(v => v.Id == newItem.Id);

            if (o== null)
            {
                return null;
            }
            else
            {
                ds.Entry(o).CurrentValues.SetValues(newItem);
                ds.SaveChanges();

                return Mapper.Map<TrackWithDetail>(o);
            }
        }


        
        public AlbumWithDetail AlbumEdit(AlbumEdit newItem)
        {
            var o = ds.Albums.Include("Track").SingleOrDefault(v => v.Id == newItem.Id);

            if (o == null)
            {
                return null;
            }
            else
            {
                ds.Entry(o).CurrentValues.SetValues(newItem);
                ds.SaveChanges();

                return Mapper.Map<AlbumWithDetail>(o);
            }

        }


        public ArtistWithDetail ArtistEdit(AlbumEdit newItem)
        {
            var o = ds.Albums.Include("Album").SingleOrDefault(v => v.Id == newItem.Id);

            if (o == null)
            {
                return null;
            }
            else
            {
                ds.Entry(o).CurrentValues.SetValues(newItem);
                ds.SaveChanges();

                return Mapper.Map<ArtistWithDetail>(o);
            }

        }

    

        // Attention - 13 - Add some programmatically-generated objects to the data store
        // Can write one method, or many methods - your decision
        // The important idea is that you check for existing data first
        // Call this method from a controller action/method
        public bool RemoveAll()
        {
           foreach(var e in ds.Tracks)
            {
                ds.Entry(e).State = System.Data.Entity.EntityState.Deleted;
            }
          
            foreach (var e in ds.Genres)
            {
                ds.Entry(e).State = System.Data.Entity.EntityState.Deleted;
            }
           
            foreach (var e in ds.Albums)
            {
                ds.Entry(e).State = System.Data.Entity.EntityState.Deleted;
            }
            foreach (var e in ds.Artists)
            {
                ds.Entry(e).State = System.Data.Entity.EntityState.Deleted;
            }
            ds.SaveChanges();

            return true;
        }


        public bool RemoveDatabase()
        {
            try
            {
                return ds.Database.Delete();
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool LoadGenre()
        {
            //    // Return if there's existing data
               if (ds.Genres.Count() > 0) { return false; }

            //    // Otherwise...
            //    // Create and add objects
            ds.Genres.Add(new Genre { Name = "Pop" });
            ds.Genres.Add(new Genre { Name = "Rock" });
            ds.Genres.Add(new Genre { Name = "Hip-Hop" });
            ds.Genres.Add(new Genre { Name = "Reggae" });
            ds.Genres.Add(new Genre { Name = "Soca" });
            ds.Genres.Add(new Genre { Name = "Alternative" });
            ds.Genres.Add(new Genre { Name = "Country" });
            ds.Genres.Add(new Genre { Name = "Techno" });
            ds.Genres.Add(new Genre { Name = "Soft-Rock" });
            ds.Genres.Add(new Genre { Name = "Dubsteb" });

            //    // Save changes
            ds.SaveChanges();

                return true;
        }


        public bool LoadArtists()
        {
            var user = HttpContext.Current.User.Identity.Name;
            //// Return if there's existing data
            if (ds.Artists.Count() > 0) { return false; }

            //// Otherwise...
            //// Create and add objects
          
            ds.Artists.Add(new Artist { Name = "Taylor Swift", BirthName = "Taylor Alison Swift", BirthOrStartDate = new DateTime(1989, 12, 13), Genre = "Pop", UrlArtist = "http://www.tvweek.com/wp-content/uploads/2014/09/taylor-swift.jpg", Executive = user });
            ds.Artists.Add(new Artist { Name = "Kanye West", BirthName = "Kanye West", BirthOrStartDate = new DateTime(1986, 11, 24), Genre = "Hip-Hop", UrlArtist = "https://assets.vg247.com/current//2015/02/kanye-west.jpg", Executive = user });
            ds.Artists.Add(new Artist { Name = "Lorde", BirthName = "Ella Marija Lani Yelich-O'Connor", BirthOrStartDate = new DateTime(1996, 10, 07), Genre = "Alternative", UrlArtist = "http://vignette2.wikia.nocookie.net/southpark/images/8/83/Lorde_AKA_Randy.png/revision/latest?cb=20141206231728", Executive = user });


            //// Save changes
            ds.SaveChanges();
            return true;
           
        }

        public bool LoadAlbum()
        {
            var user = HttpContext.Current.User.Identity.Name;

            try {
                if (ds.Albums.Count() > 0)
                    return false;

                var tSwift = ds.Artists.SingleOrDefault(a => a.Name == "Taylor Swift");

                if (tSwift == null)
                    return false;

                ds.Albums.Add(new Album
                {
                    Artists = new List<Artist> { tSwift },
                    Name = "1989",
                    Genre = "Pop",
                    Coordinator = user,
                    ReleaseDate = new DateTime(2013, 10, 27),
                    UrlAlbum = "http://cdn.totalsororitymove.com/wp-content/uploads/2014/10/ba0c12c92290af11d5e24eafcf5513ee.png"
                });

                ds.Albums.Add(new Album
                {
                    Artists = new List<Artist> { tSwift },
                    Name = "Red",
                    Genre = "Pop",
                    Coordinator = user,
                    ReleaseDate = new DateTime(2012, 10, 22),
                    UrlAlbum = "https://upload.wikimedia.org/wikipedia/en/e/e8/Taylor_Swift_-_Red.png"
                });


                var kWest = ds.Artists.SingleOrDefault(a => a.Name == "Kanye West");
                if (kWest == null)
                    return false;

                ds.Albums.Add(new Album
                {
                    Artists = new List<Artist> { kWest },
                    Name = "The Life of Pablo",
                    Genre = "Rap",
                    Coordinator = user,
                    ReleaseDate = new DateTime(2016, 02, 14),
                    UrlAlbum = "http://assets.rollingstone.com/assets/2016/media/228426/_original/1455632463/1035x1035-8c74ca69.jpeg"
                });

                ds.Albums.Add(new Album
                {
                    Artists = new List<Artist> { kWest },
                    Name = "College Dropout",
                    Genre = "Rap",
                    Coordinator = user,
                    ReleaseDate = new DateTime(2004, 02, 10),
                    UrlAlbum = "https://upload.wikimedia.org/wikipedia/en/a/a3/Kanyewest_collegedropout.jpg"
                });

                var Lorde = ds.Artists.SingleOrDefault(a => a.Name == "Lorde");
                if (Lorde == null)
                    return false;

                ds.Albums.Add(new Album
                {
                    Artists = new List<Artist> { Lorde },
                    Name = "Pure Heroine",
                    Genre = "Alternative",
                    Coordinator = user,
                    ReleaseDate = new DateTime(2013, 09, 27),
                    UrlAlbum = "https://upload.wikimedia.org/wikipedia/en/f/fe/Lorde_Pure_Heroine.png"
                });

                ds.Albums.Add(new Album
                {
                    Artists = new List<Artist> { Lorde },
                    Name = "The Love Club Ep",
                    Genre = "Alternative",
                    Coordinator = user,
                    ReleaseDate = new DateTime(2013, 03, 08),
                    UrlAlbum = "https://upload.wikimedia.org/wikipedia/en/d/dc/Lorde-The-Love-Club-EP.jpg"
                });
                ds.SaveChanges();

                return true;
            }
            catch (DbEntityValidationException ex)
            {
                // Retrieve the error messages as a list of strings.
                var errorMessages = ex.EntityValidationErrors
                        .SelectMany(x => x.ValidationErrors)
                        .Select(x => x.ErrorMessage);

                // Join the list to a single string.
                var fullErrorMessage = string.Join("; ", errorMessages);

                // Combine the original exception message with the new one.
                var exceptionMessage = string.Concat(ex.Message, " The validation errors are: ", fullErrorMessage);

                // Throw a new DbEntityValidationException with the improved exception message.
                throw new DbEntityValidationException(exceptionMessage, ex.EntityValidationErrors);
            }

          
        }


        public bool LoadTracks()
        {
            var user = HttpContext.Current.User.Identity.Name;
            var t1 = ds.Albums.SingleOrDefault(t => t.Name == "1989");

            if (t1 == null)
                return false;

            ds.Tracks.Add(new Track
            {
                Name = "Welcome to New York",
                Composers = "Taylor Swift",
                Clerk = user,
                Genre = "Pop"
            });

            ds.Tracks.Add(new Track
            {
                Name = "Blank Space",
                Composers = "Taylor Swift",
                Clerk = user,
                Genre = "Pop"
            });

            ds.Tracks.Add(new Track
            {
                Name = "Style",
                Composers = "Taylor Swift",
                Clerk = user,
                Genre = "Pop"
            });

            ds.Tracks.Add(new Track
            {
                Name = "Out of the Woods",
                Composers = "Taylor Swift",
                Clerk = user,
                Genre = "Pop"
            });

            ds.Tracks.Add(new Track
            {
                Name = "All You Had To Do Was",
                Composers = "Taylor Swift",
                Clerk = user,
                Genre = "Pop"
            });


            var t2 = ds.Albums.SingleOrDefault(t => t.Name == "Red");

            if (t2 == null)
                return false;

            ds.Tracks.Add(new Track
            {
                Name = "State of Grace",
                Composers = "Taylor Swift",
                Clerk = user,
                Genre = "Pop"
            });

            ds.Tracks.Add(new Track
            {
                Name = "Red",
                Composers = "Taylor Swift",
                Clerk = user,
                Genre = "Pop"
            });

            ds.Tracks.Add(new Track
            {
                Name = "Treacherous",
                Composers = "Taylor Swift",
                Clerk = user,
                Genre = "Pop"
            });

            ds.Tracks.Add(new Track
            {
                Name = "I Knew You Were Trouble",
                Composers = "Taylor Swift",
                Clerk = user,
                Genre = "Pop"
            });

            ds.Tracks.Add(new Track
            {
                Name = "All Too Well",
                Composers = "Taylor Swift",
                Clerk = user,
                Genre = "Pop"
            });

            var k1 = ds.Albums.SingleOrDefault(k => k.Name == "The Life of Pablo");

            if (k1 == null)
                return false;

            ds.Tracks.Add(new Track
            {
                Name = "Pt.2",
                Composers = "Kanye West",
                Clerk = user,
                Genre = "Rap"
            });

            ds.Tracks.Add(new Track
            {
                Name = "LowLights",
                Composers = "Kanye West",
                Clerk = user,
                Genre = "Rap"
            });

            ds.Tracks.Add(new Track
            {
                Name = "HighLights",
                Composers = "Kanye West",
                Clerk = user,
                Genre = "Rap"
            });

            ds.Tracks.Add(new Track
            {
                Name = "Ultralight Beam",
                Composers = "Kanye West",
                Clerk = user,
                Genre = "Rap"
            });

            ds.Tracks.Add(new Track
            {
                Name = "Famous",
                Composers = "Kanye West",
                Clerk = user,
                Genre = "Rap"
            });

            var k2 = ds.Albums.SingleOrDefault(k => k.Name == "College Dropout");

            if (k2 == null)
                return false;


            ds.Tracks.Add(new Track
            {
                Name = "Graduation Day",
                Composers = "Kanye West",
                Clerk = user,
                Genre = "Rap"
            });

            ds.Tracks.Add(new Track
            {
                Name = "All Falls Down",
                Composers = "Kanye West",
                Clerk = user,
                Genre = "Rap"
            });

            ds.Tracks.Add(new Track
            {
                Name = "I'll Fly",
                Composers = "Kanye West",
                Clerk = user,
                Genre = "Rap"
            });


            ds.Tracks.Add(new Track
            {
                Name = "Intro",
                Composers = "Kanye West",
                Clerk = user,
                Genre = "Rap"
            });


            ds.Tracks.Add(new Track
            {
                Name = "We Don't Care",
                Composers = "Kanye West",
                Clerk = user,
                Genre = "Rap"
            });


            var lrd1 = ds.Albums.SingleOrDefault(l => l.Name == "Pure Heroine");

            if (lrd1 == null)
                return false;

            ds.Tracks.Add(new Track
            {
                Name = "Tennis Courts",
                Composers = "Lorde",
                Clerk = user,
                Genre = "Alternative"
            });

            ds.Tracks.Add(new Track
            {
                Name = "400 Lux",
                Composers = "Lorde",
                Clerk = user,
                Genre = "Alternative"
            });

            ds.Tracks.Add(new Track
            {
                Name = "Royals",
                Composers = "Lorde",
                Clerk = user,
                Genre = "Alternative"
            });

            ds.Tracks.Add(new Track
            {
                Name = "Ribs",
                Composers = "Lorde",
                Clerk = user,
                Genre = "Alternative"
            });

            ds.Tracks.Add(new Track
            {
                Name = "Buzzcut Season",
                Composers = "Lorde",
                Clerk = user,
                Genre = "Alternative"
            });

            var lrd2 = ds.Albums.SingleOrDefault(l => l.Name == "The Love Club Ep");

            if (lrd2 == null)
                return false;

            ds.Tracks.Add(new Track
            {
                Name = "Bravado",
                Composers = "Lorde",
                Clerk = user,
                Genre = "Alternative"
            });

            ds.Tracks.Add(new Track
            {
                Name = "Million Dollar Bills",
                Composers = "Lorde",
                Clerk = user,
                Genre = "Alternative"
            });

            ds.Tracks.Add(new Track
            {
                Name = "Biting Down",
                Composers = "Lorde",
                Clerk = user,
                Genre = "Alternative"
            });

            ds.Tracks.Add(new Track
            {
                Name = "Royals",
                Composers = "Lorde",
                Clerk = user,
                Genre = "Alternative"
            });

            ds.Tracks.Add(new Track
            {
                Name = "The Love Club",
                Composers = "Lorde",
                Clerk = user,
                Genre = "Alternative"
            });




            ds.SaveChanges();
            return true;
        }
    }
}
 