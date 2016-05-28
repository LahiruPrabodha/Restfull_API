using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using MusicList.Models;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;

namespace MusicList.Controllers
{
    public class SongsController : ApiController
    {
        private MusicListContext db = new MusicListContext();
        /// <summary>
        /// Gets details from the server.
        /// </summary>
        // GET: api/Songs
        public IQueryable<SongDTO> GetSongs()
        {
            var songs = from b in db.Songs
                        select new SongDTO()
                        {
                            Id = b.Id,
                            Title = b.Title,
                            ArtistName = b.Artist.Name
                        };
            return songs;
        }
                
        /// <summary>
        /// Get songs ID from the server.
        /// </summary>
        // GET: api/Songs/5
        [ResponseType(typeof(SongDetailsDTO))]
        public async Task<IHttpActionResult> GetSongs(int id)
        {
            var song = await db.Songs.Include(b => b.Artist).Select(b => new SongDetailsDTO()
               {
                   Id = b.Id,
                   Title = b.Title,
                   Year = b.Year,
                   Price = b.Price,
                   ArtistName = b.Artist.Name,
                   Type = b.Type
               }).SingleOrDefaultAsync(b => b.Id == id);
            if (song == null)
            {
                return NotFound();
            }
            return Ok(song);
        }

        /// <summary>
        /// Put songs data from the server.
        /// </summary>
        // PUT: api/Songs/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutSongs(int id, Songs songs)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != songs.Id)
            {
                return BadRequest();
            }

            db.Entry(songs).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SongsExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }
                
        /// <summary>
        /// Post song data from the server.
        /// </summary>
        // POST: api/Songs
        [ResponseType(typeof(Songs))]
        public async Task<IHttpActionResult> PostSongs(Songs songs)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Songs.Add(songs);
            await db.SaveChangesAsync();

            db.Entry(songs).Reference(x => x.Artist).Load();

            var dto = new SongDTO()
            {
                Id = songs.Id,
                Title = songs.Title,
                ArtistName = songs.Artist.Name
            };

            return CreatedAtRoute("DefaultApi", new { id = songs.Id }, songs);
        }

        /// <summary>
        /// Delete Song from the server.
        /// </summary>
        // DELETE: api/Songs/5
        [ResponseType(typeof(Songs))]
        public async Task<IHttpActionResult> DeleteSongs(int id)
        {
            Songs songs = await db.Songs.FindAsync(id);
            if (songs == null)
            {
                return NotFound();
            }

            db.Songs.Remove(songs);
            await db.SaveChangesAsync();

            return Ok(songs);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool SongsExists(int id)
        {
            return db.Songs.Count(e => e.Id == id) > 0;
        }
    }
}