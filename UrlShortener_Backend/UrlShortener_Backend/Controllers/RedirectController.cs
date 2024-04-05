using Microsoft.AspNetCore.Mvc;
using UrlShortener_Backend.Services;

namespace UrlShortener_Backend.Controllers;
public class RedirectController : Controller
{
    [ProducesResponseType(StatusCodes.Status301MovedPermanently)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [HttpGet]
    public IActionResult Redirect(string ShortUrl)
    {
        var result = UrlService.FindForRedirect(ShortUrl);
        if (result.IsSuccess)
        {
            return RedirectPermanent(result.Data);
        }
        return NotFound(result.Message);
    }
}
