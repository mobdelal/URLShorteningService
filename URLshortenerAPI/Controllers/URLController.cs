using Application.Services;
using Context;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models;

namespace URLshortenerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class URLController : ControllerBase
    {
        private readonly URLShortining _urlShortining;
        private readonly ApplicationDbContext _dBContext;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public URLController(URLShortining urlShortining, ApplicationDbContext dBContext, IHttpContextAccessor httpContextAccessor)
        {
            _dBContext = dBContext;
            _urlShortining = urlShortining;
            _httpContextAccessor = httpContextAccessor;
        }
        [HttpPost]
        public async Task<IActionResult> URLshortener (URLRequest URL) 
        {
            if (!Uri.TryCreate(URL.Url, UriKind.Absolute, out _))
            {
                return BadRequest("Invalid URL");
            }
            var code = await _urlShortining.CreateUniqueCode();
            var _httpContext = _httpContextAccessor.HttpContext;

            var shortenedurl = new ShortenedURL
            {
                Code = code,
                LongUrl = URL.Url,
                ShortdUrl = $"{_httpContext.Request.Scheme}://{_httpContext.Request.Host}/api/URL/{code}",
                CreatedOn = DateTime.Now,
            };
            _dBContext.ShortenedURLs.Add(shortenedurl);
            await _dBContext.SaveChangesAsync();

            return Ok(shortenedurl.ShortdUrl);
        }
        [HttpGet("{code}")]
        public async Task<IActionResult> URLRedirect(string code) 
        {
            var shortenedurl = await _dBContext.ShortenedURLs.FirstOrDefaultAsync(x => x.Code == code);

            if (shortenedurl == null)
            {
                return NotFound();
            }

            return Redirect(shortenedurl.LongUrl);
        }


    }
}
