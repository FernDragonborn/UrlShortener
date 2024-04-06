using Microsoft.AspNetCore.Mvc;
using UrlShortener_Backend.Services;

namespace UrlShortener_Backend.Controllers;
[ApiController]
[Route("api/redirect")]
public class RedirectController : Controller
{
    [ProducesResponseType(StatusCodes.Status301MovedPermanently)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [HttpGet("{ShortUrl}")]
    public IActionResult Redirect([FromRoute] string ShortUrl)
    {
        var result = UrlService.FindForRedirect(ShortUrl);
        if (result.IsSuccess)
        {
            return RedirectPermanent(result.Data);
        }
        return NotFound(result.Message);
    }
}
