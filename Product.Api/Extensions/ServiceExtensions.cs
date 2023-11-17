using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.HttpLogging;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Product.Api.Mapper;
using Product.DataAccess;
using Product.DataAccess.Repositories;
using Product.Services.Services;
using System.Text;

namespace Product.Api.Extensions;

public static class ServiceExtensions
{
    public static string corsName = "AllowOrigin";
    public static void ConfigureHttpLogging(this IServiceCollection services)
    {
        services.AddHttpLogging(logging =>
        {
            logging.LoggingFields = HttpLoggingFields.All;
            logging.RequestHeaders.Add("sec-ch-ua");
            logging.ResponseHeaders.Add("MyResponseHeader");
            logging.MediaTypeOptions.AddText("application/javascript");
            logging.RequestBodyLogLimit = 4096;
            logging.ResponseBodyLogLimit = 4096;

        });
    }


    public static void ConfigureSwagger(this IServiceCollection services)
    {
        services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo { Title = "Product API", Version = "v1" });
            options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                In = ParameterLocation.Header,
                Description = "Please enter a valid token",
                Name = "Authorization",
                Type = SecuritySchemeType.ApiKey,
                BearerFormat = "JWT",
                Scheme = "Bearer"
            });
            options.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type=ReferenceType.SecurityScheme,
                            Id="Bearer"
                        }
                    },
                    new string[]{}
                }
            });
        });

    }

    public static void ConfigureJwtAuthentication(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = configuration["Jwt:Issuer"],
                    ValidAudience = configuration["Jwt:Issuer"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]))
                };
            });
    }

    public static void ConfigureDb(this IServiceCollection services)
    {
        services.AddDbContext<ProductContext>();
    }

    public static void ConfigureAutoMapper(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(AutoMapperProfile));
    }

    public static void ConfigureLogger(this IServiceCollection services)
    {
        services.AddLogging(loggingBuilder =>
        {
            loggingBuilder.ClearProviders();
            loggingBuilder.AddConsole();
        });
    }


    public static void ConfigureCors(this IServiceCollection services)
    {
        services.AddCors(c =>
        {

            c.AddPolicy(corsName,
                           options => options
                           .SetIsOriginAllowed(IsOriginAllowed)
                           //.WithOrigins( "http://enesql.shi.co.jp/", "http://localhost:4200/" )
                           .AllowAnyHeader()
                           .AllowAnyMethod()
                           .AllowCredentials());
        });
    }

    public static void ConfigureRepository(this IServiceCollection services)
    {
        services.AddScoped<IProductService, ProductService>();
        services.AddScoped<IProductRepository, ProductRepository>();
    }

    static bool IsOriginAllowed(string origin)
    {
        var uri = new Uri(origin);
        var env = System.Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "n/a";



        var isAllowed = uri.Host.Equals("eneweb02.shi-g.com", StringComparison.OrdinalIgnoreCase) ||
                        uri.Host.Equals("localhost", StringComparison.OrdinalIgnoreCase);

        return isAllowed;
    }



}