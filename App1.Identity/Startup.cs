using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Swashbuckle.AspNetCore.Swagger;
using App1.Identity.DbContext;
using App1.Identity.Entities;
using App1.Identity.GlobalSettings;
using App1.Identity.Handlers;
using App1.Identity.Helpers;
using App1.Identity.Helpers.Interfaces;
using App1.Identity.Models.FacebookResponse;
using App1.Identity.Models.GoogleResponse;
using App1.Identity.Services;
using App1.Identity.Services.Interfaces;
using App1.Identity.Services.Sync;
using App1.ServiceBase.Handler;

namespace Upope.Identity
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            BuildAppSettingsProvider();
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            #region Add Entity Framework and Identity Framework

            services.AddDbContext<ApplicationUserDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DatabaseConnection")));

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationUserDbContext>();

            services.AddTransient<IRandomPasswordHelper, RandomPasswordHelper>();

            services.AddTransient<IExternalAuthClient, FacebookClient>();
            services.AddTransient<IExternalAuthClient, GoogleClient>();

            services.AddTransient<IExternalAuthService<FacebookResponse>, FacebookService>(sp => {
                var externalAuthService = sp.GetRequiredService<IEnumerable<IExternalAuthClient>>();
                return new FacebookService(externalAuthService.FirstOrDefault());
            });

            services.AddTransient<IExternalAuthService<GoogleResponse>, GoogleService>(sp => {
                var externalAuthService = sp.GetRequiredService< IEnumerable<IExternalAuthClient>>();
                return new GoogleService(externalAuthService.Skip(1).Take(1).FirstOrDefault());
            });

            #endregion

            #region Add Authentication
            var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Tokens:Key"]));
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddFacebook(options =>
            {
                options.AppId = Configuration["ExternalAuthentication:Facebook:AppId"];
                options.AppSecret = Configuration["ExternalAuthentication:Facebook:AppSecret"];
            })
            .AddGoogle(googleOptions =>
            {
                googleOptions.ClientId = Configuration["ExternalAuthentication:Google:ClientId"];
                googleOptions.ClientSecret = Configuration["ExternalAuthentication:Google:ClientSecret"];
            }) 
            .AddJwtBearer(config =>
            {
                config.RequireHttpsMetadata = false;
                config.SaveToken = true;
                config.TokenValidationParameters = new TokenValidationParameters()
                {
                    IssuerSigningKey = signingKey,
                    ValidateAudience = true,
                    ValidAudience = Configuration["Tokens:Audience"],
                    ValidateIssuer = true,
                    ValidIssuer = Configuration["Tokens:Issuer"],
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true
                };
            });

            #endregion

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2)
                .AddJsonOptions(options => {
                    options.SerializerSettings.DateFormatString = "yyyy-MM-dd";
                });

            // Register the Swagger generator, defining 1 or more Swagger documents
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "App1 Identity API", Version = "v1" });
            });

            services.AddAutoMapper();

            services.AddHttpClient();
            services.AddTransient<IHttpHandler, HttpHandler>();
            services.AddTransient<ISpotPlayerSyncService, SpotPlayerSyncService>();
            services.AddTransient<ITokenService, TokenService>();
            services.AddTransient<IPasswordHasher, PasswordHasher>();
            //services.AddOcelot(Configuration);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.ConfigureExceptionHandler();
            //app.UseHttpsRedirection();
            app.UseAuthentication();

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.), 
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "App1 Identity API V1");
                c.RoutePrefix = string.Empty;
            });

            app.UseDefaultFiles();

            app.UseStaticFiles();
            app.UseMvc();
            //await app.UseOcelot();
        }

        private void BuildAppSettingsProvider()
        {
            AppSettingsProvider.SpotBaseUrl = Configuration["App1.Spot:BaseUrl"].ToString();
            AppSettingsProvider.CreateOrUpdateUser = Configuration["App1.Spot:CreateOrUpdateUser"].ToString();
            AppSettingsProvider.CreateOrUpdatePlayer = Configuration["App1.Spot:CreateOrUpdatePlayer"].ToString();
            AppSettingsProvider.IdentityBaseUrl = Configuration["App1.Identity:BaseUrl"].ToString();
        }
    }
}
