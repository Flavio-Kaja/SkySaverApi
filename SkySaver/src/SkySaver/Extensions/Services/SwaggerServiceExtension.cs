namespace SkySaver.Extensions.Services;

using SkySaver.Services;
using Configurations;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System;
using System.IO;
using System.Collections.Generic;
using System.Reflection;

public static class SwaggerServiceExtension
{
    public static void AddSwaggerExtension(this IServiceCollection services, IConfiguration configuration)
    {
        var authOptions = configuration.GetAuthOptions();
        services.AddSwaggerGen(config =>
        {
            config.CustomSchemaIds(type => type.ToString());
            config.MapType<DateOnly>(() => new OpenApiSchema
            {
                Type = "string",
                Format = "date"
            });

            config.SwaggerDoc(
                "v1",
                new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Flight Rewards Management",
                    Description = "Our API uses a REST based design, leverages the JSON data format, and relies upon HTTPS for transport. We respond with meaningful HTTP response codes and if an error occurs, we include error details in the response body. API Documentation is at flightrewards.com/dev/docs",
                    Contact = new OpenApiContact
                    {
                        Name = "Flight Rewards",
                        Email = "devsupport@FlightRewards.com",
                            Url = new Uri("https://www.flightrewards.com"),
                    },
                });

            config.IncludeXmlComments(string.Format(@$"{AppDomain.CurrentDomain.BaseDirectory}{Path.DirectorySeparatorChar}SkySaver.WebApi.xml"));
        });
    }
}