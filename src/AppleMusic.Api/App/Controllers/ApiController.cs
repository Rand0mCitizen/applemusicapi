using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppleMusic.Domain.Model;
using Microsoft.AspNetCore.Mvc;

namespace AppleMusic.Store.App.Controllers
{
    [Produces("application/json")]
    public class ApiController : ControllerBase
    {
        private readonly IArtistRepository _artistRepository;
        public ApiController(IArtistRepository repository)
        {
            _artistRepository = repository;
        }

        [HttpGet("/Api/{term}")]
        public async Task<IEnumerable<Artist>> ListAsync(string term)
        {
            var result = _artistRepository.List(a => a.Title.ToLower().Contains(term)).ToArray();
            return result;
        }
    }
}
