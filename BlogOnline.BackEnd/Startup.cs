﻿using System;
using System.Collections.Generic;
using BlogOnline.BackEnd.Helper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace BlogOnline.BackEnd
{
    public class Startup
    {
        readonly string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        public void ConfigureServices(IServiceCollection services)
        {
            var clientUrls = new Dictionary<string, string>
            {
                ["Blazor"] = Configuration["ClientUrl:Blazor"],
                ["Swagger"] = Configuration["ClientUrl:Swagger"]
            };

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));

            // services.AddTransient<IStorageService, FileStorageService>();
            services.AddScoped<IReadHelper, ReadHelper>();

            //services.AddDefaultIdentity<User>()
            //    .AddRoles<IdentityRole>()
            //    .AddEntityFrameworkStores<ApplicationDbContext>();
            //services.AddIdentityServer(options =>
            //{
            //    options.Events.RaiseErrorEvents = true;
            //    options.Events.RaiseInformationEvents = true;
            //    options.Events.RaiseFailureEvents = true;
            //    options.Events.RaiseSuccessEvents = true;
            //})
            //    .AddInMemoryIdentityResources(IdentityServerConfig.Ids)
            //    .AddInMemoryApiResources(IdentityServerConfig.Apis)
            //    .AddInMemoryClients(IdentityServerConfig.Clients(clientUrls))
            //    .AddAspNetIdentity<User>()
            //    .AddDeveloperSigningCredential(); // not recommended for production - you need to store your key material somewhere secure

            //services.AddAuthentication()
            //    .AddLocalApi("Bearer", option =>
            //    {
            //        option.ExpectedScope = "api.myshop";
            //    });

            //services.AddAuthorization(options =>
            //{
            //    options.AddPolicy("Bearer", policy =>
            //    {
            //        policy.AddAuthenticationSchemes("Bearer");
            //        policy.RequireAuthenticatedUser();
            //    });
            //});

            //services.AddCors(options =>
            //{
            //    options.AddPolicy(MyAllowSpecificOrigins,
            //    builder =>
            //    {
            //        builder.WithOrigins(clientUrls["Blazor"], clientUrls["Angular"])
            //            .AllowAnyHeader()
            //            .AllowAnyMethod();
            //    });
            //});

            services.AddControllersWithViews();
            services.AddRazorPages();

            //services.AddSwaggerGen(c =>
            //{
            //    c.SwaggerDoc("v1", new OpenApiInfo { Title = "MyShop API", Version = "v1" });
            //    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            //    {
            //        Type = SecuritySchemeType.OAuth2,
            //        Flows = new OpenApiOAuthFlows
            //        {
            //            AuthorizationCode = new OpenApiOAuthFlow
            //            {
            //                TokenUrl = new Uri("/connect/token", UriKind.Relative),
            //                AuthorizationUrl = new Uri("/connect/authorize", UriKind.Relative),
            //                Scopes = new Dictionary<string, string> { { "api.myshop", "My Shop API" } }
            //            },
            //        },
            //    });
            //    c.AddSecurityRequirement(new OpenApiSecurityRequirement
            //    {
            //        {
            //            new OpenApiSecurityScheme
            //            {
            //                Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "Bearer" }
            //            },
            //            new List<string>{ "api.myshop" }
            //        }
            //    });
            //});

            //services.AddDatabaseDeveloperPageExceptionFilter();
            //services.AddOpenTelemetryTracing(tracing =>
            //{
            //    tracing.AddAspNetCoreInstrumentation()
            //        .AddHttpClientInstrumentation()
            //        .AddSource("OTel.Demo")
            //        .SetSampler(new AlwaysOnSampler())
            //        .AddZipkinExporter(option =>
            //        {
            //            option.ServiceName = typeof(Startup).Assembly.GetName().Name;
            //            option.Endpoint = new Uri("http://localhost:9411/api/v2/spans");
            //        });
            //});
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            //app.UseSwagger();

            //app.UseIdentityServer();
            app.UseAuthorization();
            //app.UseSwaggerUI(c =>
            //{
            //    c.OAuthClientId("swagger");
            //    c.OAuthClientSecret("secret");
            //    c.OAuthUsePkce();
            //    c.SwaggerEndpoint("/swagger/v1/swagger.json", "MyShop API V1");
            //});

            app.UseCors(MyAllowSpecificOrigins);

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });
        }
    }
}
