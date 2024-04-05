﻿using Microsoft.IdentityModel.Tokens;
using System.Text;
using UrlShortener_Backend.DbContext;
using UrlShortener_Backend.DTOs;
using UrlShortener_Backend.Models;

namespace UrlShortener_Backend.Services
{
    public class UrlService
    {
        public static Result<UrlDto[]> GetUrls()
        {
            var context = ContextFactory.CreateNew();

            var arrStored = context.Urls.ToArray();
            UrlDto[] arrResponse = new UrlDto[arrStored.Length];
            for (int i = 0; i < arrStored.Length; i++)
            {
                arrResponse[i] = new(arrStored[i]);
            }

            return new Result<UrlDto[]>(true, "", arrResponse);
        }

        public static Result<UrlDto> FindForRedirect(UrlDto dto)
        {
            var context = ContextFactory.CreateNew();

            var url = context.Urls.Find(dto.ShortUrl);
            if (url is null) return new Result<UrlDto>(false, "Url do not exist", null);

            return new Result<UrlDto>(true, "Successfully found", new UrlDto(url));
        }

        public static Result<UrlDto> Register(UrlDto urlDto)
        {
            var context = ContextFactory.CreateNew();

            UrlData? url = context.Urls.FirstOrDefault(x => x.LongUrl == urlDto.LongUrl);
            if (url is not null) return new Result<UrlDto>(false, "Url already exists", null);

            var user = context.Users.Find(Guid.Parse(urlDto.UserId));
            if (user is null) return new Result<UrlDto>(false, "User do not exist", null);


            url = new()
            {
                LongUrl = urlDto.LongUrl,
                ShortUrl = GenerateRandomString(6),
                User = user,
                CreatedAt = DateTime.Now
            };

            context.Urls.Add(url);
            context.SaveChanges();

            return new Result<UrlDto>(true, "Url successfully created", new UrlDto(url));
        }

        public static Result DeleteAny(UrlDto urlDto)
        {
            var context = ContextFactory.CreateNew();

            var url = context.Urls.Find(urlDto.ShortUrl);
            if (url is null) return new Result(false, "Url doesn't exist");

            context.Urls.Remove(url);
            context.SaveChanges();

            return new Result(true, "Url successfully deleted");
        }

        public static Result Delete(UrlDto urlDto)
        {
            if (urlDto.UserId.IsNullOrEmpty()) return new Result(false, "UserId is null");

            var context = ContextFactory.CreateNew();

            var url = context.Urls.Find(urlDto.ShortUrl);
            if (url is null) return new Result(false, "Url doesn't exist");
            if (url.User.UserId.ToString() != urlDto.UserId) return new Result(false, "Unauthorized");

            context.Urls.Remove(url);
            context.SaveChanges();

            return new Result(true, "Url successfully deleted");
        }

        private static readonly Random _random = new();
        public static string GenerateRandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var stringBuilder = new StringBuilder(length);

            for (int i = 0; i < length; i++)
            {
                stringBuilder.Append(chars[_random.Next(chars.Length)]);
            }

            return stringBuilder.ToString();
        }

    }
}
