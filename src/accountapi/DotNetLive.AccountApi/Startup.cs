﻿using DotNetLive.Framework.DependencyManagement;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;

namespace DotNetLive.AccountApi
{
    public partial class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            var env = services.BuildServiceProvider().GetService<IHostingEnvironment>();
            services.AddSingleton(factory => services);
            services.AddSingleton(factory => Configuration);

            //先通过asp.net core ioc注册
            services.AddDependencyRegister(Configuration);

            ConfigSwagger(services);
            ConfigureAuthorization(services);
            return services.BuildServiceProvider();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            app.UseDeveloperExceptionPage();

            app.UseStaticFiles();

            ConfigSwagger(app);

            ConfigureAuthorization(app);

            //// System.Security.Cryptography.X509Certificates.X509Certificate2 cert2 = new System.Security.Cryptography.X509Certificates.X509Certificate2(byte[] rawData);
            //System.Security.Cryptography.X509Certificates.X509Certificate2 cert2 = DotNetUtilities.CreateX509Cert2("mycert");
            //Microsoft.IdentityModel.Tokens.SecurityKey secKey = new X509SecurityKey(cert2);

            //var tokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
            //{
            //    // The signing key must match!
            //    ValidateIssuerSigningKey = true,
            //    IssuerSigningKey = signingKey,

            //    // Validate the JWT Issuer (iss) claim
            //    ValidateIssuer = true,
            //    ValidIssuer = "ExampleIssuer",

            //    // Validate the JWT Audience (aud) claim
            //    ValidateAudience = true,
            //    ValidAudience = "ExampleAudience",

            //    // Validate the token expiry
            //    ValidateLifetime = true,

            //    // If you want to allow a certain amount of clock drift, set that here:
            //    ClockSkew = TimeSpan.Zero,
            //};

            //var bearerOptions = new JwtBearerOptions()
            //{
            //    AutomaticAuthenticate = true,
            //    AutomaticChallenge = true,
            //    TokenValidationParameters = tokenValidationParameters,
            //    Events = new CustomBearerEvents()
            //};

            //// Optional 
            //// bearerOptions.SecurityTokenValidators.Clear();
            //// bearerOptions.SecurityTokenValidators.Add(new MyTokenHandler());
            //app.UseJwtBearerAuthentication(bearerOptions);

            app.UseMvcWithDefaultRoute();
        }
    }
}
