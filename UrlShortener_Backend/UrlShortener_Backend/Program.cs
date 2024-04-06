using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using System.Reflection;
using System.Text;
using UrlShortener_Backend.DbContext;
using UrlShortener_Backend.Identity;
using UrlShortener_Backend.Services;

namespace UrlShortener_Backend;

public class Program
{
    public static void Main(string[] args)
    {
        Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
        Console.OutputEncoding = Encoding.GetEncoding(1251);
        Console.InputEncoding = Encoding.GetEncoding(1251);

        var builder = WebApplication.CreateBuilder(args);

        //Uncomment for api models problems
        //https://mirsaeedi.medium.com/asp-net-core-customize-validation-error-message-9022c12d3d7d
#if DEBUG
        builder.Services.Configure<ApiBehaviorOptions>(apiBehaviorOptions =>
            {
                apiBehaviorOptions.SuppressModelStateInvalidFilter = true;
            });
#endif

        //Add services to the container.
        builder.Services.AddSingleton<IConfiguration>(builder.Configuration);
        ContextFactory.Initialize(
        builder.Services.BuildServiceProvider().GetRequiredService<IConfiguration>()
        );
        JwtHandler.Initialize(
            builder.Services.BuildServiceProvider().GetRequiredService<IConfiguration>()
        );

        builder.Services.AddSingleton<JwtHandler>();

        string connectionString = builder.Configuration.GetConnectionString("Default")
                                  ?? throw new InvalidOperationException("Connection string not found.");
        builder.Services.AddControllers();
        builder.Services.AddDbContext<UrlShortenerDbContext>(options =>
            options.UseSqlServer(connectionString));

        builder.Services.AddCors(options =>
        {
            options.AddPolicy("AllowAllOrigins",
                builder =>
                {
                    builder.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader();
                });
        });
        builder.Services.AddMvc(options => options.EnableEndpointRouting = false);

        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen(options =>
        {
            options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme()
            {
                In = ParameterLocation.Header,
                Name = "Authorization",
                Type = SecuritySchemeType.ApiKey
            });
            options.OperationFilter<SecurityRequirementsOperationFilter>();
            options.SwaggerDoc("v1", new OpenApiInfo
            {
                Version = "v1",
                Title = "ToDo API",
                Description = "An ASP.NET Core Web API for managing ToDo items",
                TermsOfService = new Uri("https://example.com/terms"),
                Contact = new OpenApiContact
                {
                    Name = "Example Contact",
                    Url = new Uri("https://example.com/contact")
                },
                License = new OpenApiLicense
                {
                    Name = "Example License",
                    Url = new Uri("https://example.com/license")
                }
            });

            //using System.Reflection;
            var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
        });

        builder.Services.AddAuthentication(x =>
        {
            x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            x.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(x =>
        {
            x.TokenValidationParameters = new TokenValidationParameters
            {
                ValidIssuer = builder.Configuration["JwtSettings:Issuer"],
                ValidAudience = builder.Configuration["JwtSettings:Audience"],
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
                    builder.Configuration["JwtSettings:Key"])),
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,

            };
        });

        builder.Services.AddAuthorization(options =>
        {
            options.AddPolicy(IdentityData.AdminUserPolicyName, p =>
                p.RequireClaim(IdentityData.AdminUserClaimName, "true"));
        });

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
                options.RoutePrefix = string.Empty;
            });
        }
        else
        {
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseCors("AllowAllOrigins");
        app.UseMvc();

        app.UseAuthentication();
        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}
