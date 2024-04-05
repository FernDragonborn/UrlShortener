using UrlShortener_Backend.DbContext;
using UrlShortener_Backend.DTOs;
using UrlShortener_Backend.Models;

namespace UrlShortener_Backend.Services;
public static class UserService
{
    public static Result<UserDto> RegisterUser(IConfiguration config, UserDto userDto)
    {
        var context = ContextFactory.CreateNew();

        if (context.Users.FirstOrDefault(x => x.Login == userDto.Login) != default)
        {
            return new Result<UserDto>(false, "User with this login already exists", null);
        }

        User user = new()
        {
            Salt = BCrypt.Net.BCrypt.GenerateSalt()
        };
        user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(userDto.Password + user.Salt, workFactor: 13);

        user.Login = userDto.Login;
        user.Role = userDto.Role;

        try
        {
            context.Users.Add(user);
            context.SaveChangesAsync();
        }
        catch (Exception ex) { return new Result<UserDto>(false, ex.ToString(), null); }

        string token = JwtHandler.CreateToken(user);
        userDto = new UserDto(user)
        {
            JwtToken = token
        };
        return new Result<UserDto>(true, "api/auth", userDto);
    }

    public static Result<UserDto> LogIn(IConfiguration config, UserDto userDto)
    {
        var context = ContextFactory.CreateNew();

        var user = context.Users.FirstOrDefault(x => x.Login == userDto.Login);
        if (user is null
            || !BCrypt.Net.BCrypt.Verify(userDto.Password + user.Salt, user.PasswordHash))
        {
            return new Result<UserDto>(
                false,
                "Wrong login or password",
                null);
        }

        string token = JwtHandler.CreateToken(user, config);
        userDto = new UserDto(user)
        {
            JwtToken = token
        };
        return new Result<UserDto>(true, "", userDto);
    }

    public static Result<UserDto> RenewToken(IConfiguration config, UserDto userDto)
    {
        var context = ContextFactory.CreateNew();

        User user = context.Users.FirstOrDefault(x => x.Login == userDto.Login);
        if (user is null)
        {
            return new Result<UserDto>(
                false,
                "User not found",
                null);
        }

        string token = JwtHandler.CreateToken(user, config);
        userDto = new UserDto(user)
        {
            JwtToken = token
        };

        return new Result<UserDto>(true, "", userDto);
    }
}

