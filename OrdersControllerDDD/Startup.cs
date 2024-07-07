using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.SwaggerGen;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using System.IO;
using System.Reflection;
using System;
using System.Collections.Generic;
using ITS.ServiceManager.API.Swagger;
using Domain;
using Infrastructure.Data.Interfaces;
using Infrastructure.Data.Repositories;
using Infrastructure.Data.Services;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace OrdersControllerDDD
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddDbContext<OrderContext>(options =>
                options.UseSqlServer("Data Source= (local)\\MSSQLSERVER01; Initial Catalog=PubContractDataMigrationsTest; TrustServerCertificate=True; User Id=Domo; Password=Dominik5123!;"));

            services.AddScoped<IOrdersService, OrdersService>();
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();
            services.AddSwaggerGen(options =>
            {
                // Add a custom operation filter which sets default values
                options.OperationFilter<SwaggerDefaultValues>();

                // Include XML comments
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);

                if (File.Exists(xmlPath))
                {
                    options.IncludeXmlComments(xmlPath);
                }
                else
                {
                    throw new FileNotFoundException($"XML Documentation file '{xmlPath}' not found. Make sure it is generated during build.");
                }

                // Uncomment this to enable Bearer Token authorization
                #region Bearer Token Auth
                //options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                //{
                //    Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                //    Name = "Authorization",
                //    In = ParameterLocation.Header,
                //    Type = SecuritySchemeType.ApiKey
                //});
                //options.AddSecurityRequirement(new OpenApiSecurityRequirement
                //{
                //    {
                //        new OpenApiSecurityScheme
                //        {
                //            Reference = new OpenApiReference
                //            {
                //                Type = ReferenceType.SecurityScheme,
                //                Id = "Bearer"
                //            },
                //            Scheme = "oauth2",
                //            Name = "Bearer",
                //            In = ParameterLocation.Header,
                //        },
                //        new List<string>()
                //    }
                //});
                #endregion

                // Custom schema IDs to avoid conflicts
                options.CustomSchemaIds(x => x.FullName);
            });

            // Add API versioning and API explorer for versioning
            services.AddApiVersioning(options =>
            {
                options.ReportApiVersions = true;
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.DefaultApiVersion = new Microsoft.AspNetCore.Mvc.ApiVersion(1, 0);
            });

            services.AddVersionedApiExplorer(options =>
            {
                options.GroupNameFormat = "'v'VVV";
                options.SubstituteApiVersionInUrl = true;
            });
            
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IHostApplicationLifetime lifetime, IApiVersionDescriptionProvider apiDescriptionProvider)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // Use Swagger middleware
            app.UseSwagger(options =>
            {
                options.RouteTemplate = "swagger/{documentName}/swagger.json";
            });

            // Use SwaggerUI middleware
            app.UseSwaggerUI(options =>
            {
                options.RoutePrefix = "swagger";
                foreach (var description in apiDescriptionProvider.ApiVersionDescriptions)
                {
                    options.SwaggerEndpoint($"{description.GroupName}/swagger.json", description.GroupName.ToUpperInvariant());
                }
                options.DocumentTitle = "OrdersDDD";
            });

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                // Uncomment the following line if you have SignalR hubs
                // endpoints.MapHub<AppHub>("/appHub");
            });
        }
    }
}
