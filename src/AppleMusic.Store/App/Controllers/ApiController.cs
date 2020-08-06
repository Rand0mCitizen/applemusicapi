using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using AppleMusic.Domain.Model;
using AppleMusic.Store.App.Helpers.Dto;
using AppleMusic.Store.App.Service;
using Microsoft.AspNetCore.Mvc;

namespace AppleMusic.Store.App.Controllers
{
    [Route("/api")]
    [Produces("application/json")]
    public class ApiController : ControllerBase
    {
        private readonly IArtistService _artistService;

        public ApiController(IArtistService service) => _artistService = service;

        [HttpGet("{id:int}")]
        public async Task<Artist> GetByIdAsync(int id)
        {
            var result = await _artistService.GetAsync(id);
            return result;
        }

        [HttpGet("{term?}")]
        public async Task<IEnumerable<Artist>> ListAsync(string term)
        {
            var result = string.IsNullOrWhiteSpace(term) ? await _artistService.ListAsync() : await _artistService.FindByTitleAsync(term);
            return result;
        }

        [HttpPut]
        public async Task<IActionResult> AddAsync([FromBody] ArtistDto artist)
        {
            await _artistService.AddAsync(artist.Title);
            return new StatusCodeResult((int)HttpStatusCode.Created);
        }

        [HttpPut("{artistId}")]
        public async Task<IActionResult> AddAsync([FromBody] AlbumDto album, int artistId)
        {
            await _artistService.AddAlbumAsync(artistId, album);
            return new StatusCodeResult((int)HttpStatusCode.Created);
        }
    }
}
