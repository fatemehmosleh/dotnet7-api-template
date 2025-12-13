using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using NSwag;
using NSwag.Generation.Processors.Security;
using BackEnd.Common.AspNetCore.Security;
using BackEnd.Services.Core.Api.Facade;
using BackEnd.Services.Core.Api.Context;
using BackEnd.Services.Core.Api.Exceptions;
using BackEnd.Services.Core.Api.Facade.Abstraction;
using BackEnd.Services.Core.Api.Scheduler;
using Microsoft.Extensions.Hosting.Internal;
using System.IO;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.DataProtection.AuthenticatedEncryption.ConfigurationModel;
using Microsoft.AspNetCore.DataProtection.AuthenticatedEncryption;
using EncryptionAlgorithm = Microsoft.AspNetCore.DataProtection.AuthenticatedEncryption.EncryptionAlgorithm;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Text;

namespace BackEnd.Services.Core.Api
{
    public class Startup
    {
        private readonly IWebHostEnvironment _env;
        public IConfiguration Configuration { get; }

        public static IServiceProvider ServiceProvider { get; set; }

        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            Configuration = configuration;
            _env = env;

            


        }


        public void ConfigureServices(IServiceCollection services)
        {
            //add settings class to DI
            var settings = Configuration.GetSection("Settings").Get<Settings>();
            services.Configure<Settings>(Configuration.GetSection("Settings"));
            

            
            services.AddScoped<DemoJob>();
            
            services.AddControllers(o =>
            {
                if (!_env.IsDevelopment())
                {
                    //Add AuthorizationPolicy to all controllers in production
                    var policy = new AuthorizationPolicyBuilder()
                        .RequireAuthenticatedUser()
                        .Build();

                    o.Filters.Add(new AuthorizeFilter(policy));
                }
            });

            services.AddOpenApiDocument(document =>
            {
                document.DocumentName = "v1";
                document.Title = "BackEnd.Services.Core.Api";
                document.Description = "backend Services Core Api";
                document.GenerateEnumMappingDescription = true;

                document.AddSecurity("bearer", Enumerable.Empty<string>(), new OpenApiSecurityScheme
                {
                    Type = OpenApiSecuritySchemeType.Http,
                    Scheme = "bearer",
                    BearerFormat = "JWT",
                    In = OpenApiSecurityApiKeyLocation.Header,
                    Name = "Authorization",
                    Description = "JWT Authorization header using the Bearer scheme. Example: \"Bearer {token}\""
                });

                document.OperationProcessors.Add(
                    new AspNetCoreOperationSecurityScopeProcessor("bearer"));
            });



            Common.AspNetCore.Security.TokenHelper.AddJwtAuthentication(services);

            // add cors
            services.AddCors(options =>
            {
                // this defines a CORS policy called "default"
                options.AddPolicy("default", policy =>
                {
                    policy.WithOrigins(
                            "http://localhost:3000",
                            "http://localhost:6419"
                            )
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        .AllowCredentials();
                });
            });
            services.AddDbContextPool<CoreContext>(options =>
                options.UseMySql(Configuration.GetConnectionString("CoreContext"),
                    new MySqlServerVersion(new Version(8, 0, 21))));


            //Use HttpContext in Facade components
            services.AddHttpContextAccessor();


            //add Facades to DI
           
            services.AddScoped<IUserFacade, UserFacade>();
           

            //ToDo:This line must be not comment
           // HandleScheduler.StartAsync(services.BuildServiceProvider()).GetAwaiter().GetResult();

            //services.AddDataProtection().PersistKeysToFileSystem(new DirectoryInfo(@"C:\temp-keys\"))
            services.AddDataProtection().PersistKeysToFileSystem(new DirectoryInfo(@"/root/.aspnet/DataProtection-Keys"))
                .UseCryptographicAlgorithms(new AuthenticatedEncryptorConfiguration()
                {
                    EncryptionAlgorithm = EncryptionAlgorithm.AES_256_CBC,
                    ValidationAlgorithm = ValidationAlgorithm.HMACSHA256
                });



        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                //app.UseSwagger();
                //app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "BackEnd.Services.Core.Api v1"));
            }
            

            // Add OpenAPI/Swagger middleWares
            app.UseOpenApi(); // Serves the registered OpenAPI/Swagger documents by default on `/swagger/{documentName}/swagger.json`
            app.UseSwaggerUi3(); // Serves the Swagger UI 3 web ui to view the OpenAPI/Swagger documents by default on `/swagger`

            if (!env.IsDevelopment())
                app.ConfigureCustomExceptionMiddleware();


            //app.UseHttpsRedirection();
            app.UseCors("default");
            
            app.UseRouting();

            //enable Authentication and Authorization
            app.UseAuthentication();
            app.UseAuthorization();


            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });

            // apply peding database migirations
            using var scope = app.ApplicationServices.CreateScope();
            ApplyMigrations(scope.ServiceProvider.GetRequiredService<CoreContext>());
        }


        // function to apply peding database migirations
        private static void ApplyMigrations(DbContext context)
        {
            if (context.Database.GetPendingMigrations().Any())
            {
                context.Database.Migrate();
            }
        }
    }

}