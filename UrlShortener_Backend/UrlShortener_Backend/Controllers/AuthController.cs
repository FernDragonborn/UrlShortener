using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UrlShortener_Backend.DTOs;
using UrlShortener_Backend.Services;


namespace UrlShortener_Backend.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthController : Controller
{
    private readonly IConfiguration _config;

    public AuthController(IConfiguration config)
    {
        _config = config;
    }

    /// <summary>
    /// Register new user
    /// </summary>
    /// <param></param>
    /// <param name="userDto">user data transfer object. Neded fields for this method: login, password</param>
    /// <returns>HTTP responce.</returns>
    /// <response code="201">Succesfully regitered</response>
    /// <response code="400">User with this login already exists or Database exception</response>
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [HttpPost("register")]
    public IActionResult Register(UserDto userDto)
    {
        var result = UserService.RegisterUser(_config, userDto);
        if (result.IsSuccess)
        {
            return Created(result.Message, result.Data);
        }
        return BadRequest(result.Message);
    }

    /// <summary>
    /// Log in and get JWT token
    /// </summary>
    /// <param></param>
    /// <param name="userDto">user data transfer object. Neded fields for this method: login, password</param>
    /// <returns>HTTP responce and JWT token in it.</returns>
    /// <response code="200">Succesfully logged in</response>
    /// <response code="400">Wrong login or password</response>
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [HttpPost("login")]
    public IActionResult LogIn(UserDto userDto)
    {
        var result = UserService.LogIn(_config, userDto);
        if (result.IsSuccess)
        {
            return Ok(result.Data);
        }
        return BadRequest(result.Message);
    }

    /// <summary>
    /// Method for testing if authoraization works
    /// </summary>
    /// <param></param>
    /// <param name="userDto">user data transfer object. Neded fields for this method: login, password</param>
    /// <returns>HTTP responce.</returns>
    /// <response code="200">Succesfully authorized</response>
    [ProducesResponseType(StatusCodes.Status200OK)]
    [Authorize]
    [HttpPost("renewToken")]
    public IActionResult RenewToken(UserDto userDto)
    {
        var result = UserService.RenewToken(_config, userDto);
        if (result.IsSuccess)
        {
            return Ok(result.Data);
        }
        return BadRequest(result.Message);
    }
}