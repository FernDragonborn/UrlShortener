using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UrlShortener_Backend.DTOs;
using UrlShortener_Backend.Services;
using static UrlShortener_Backend.Identity.IdentityData;

namespace UrlShortener_Backend.Controllers;

[ApiController]
[Route("api/url")]
public class UrlController : Controller
{
    [ProducesResponseType(StatusCodes.Status200OK)]
    [HttpGet]
    public IActionResult GetUrls()
    {
        return Ok(UrlService.GetUrls());
    }

    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [Authorize]
    [HttpPost("register")]
    public IActionResult Register(UrlDto urlDto)
    {
        var result = UrlService.Register(urlDto);
        if (result.IsSuccess)
        {
            return Created(result.Message, result.Data);
        }
        return BadRequest(result.Message);
    }


    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [Authorize]
    [HttpPost("deleteAny")]
    public IActionResult DeleteAny(UrlDto urlDto)
    {
        var isAdmin = User.Claims.Any(c => c is { Type: "role", Value: AdminUserClaimName });
        if (!isAdmin) return Unauthorized();

        var result = UrlService.DeleteAny(urlDto);
        if (result.IsSuccess)
        {
            return Ok(result.Message);
        }
        return BadRequest(result.Message);
    }


    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [Authorize]
    [HttpPost("delete")]
    public IActionResult Delete(UrlDto urlDto)
    {
        urlDto.UserId = User.Claims.FirstOrDefault(c => c.Type == "userId").Value;

        var result = UrlService.Delete(urlDto);
        if (result.IsSuccess)
        {
            return Ok(result.Message);
        }
        return BadRequest(result.Message);
    }
}

