using System;
using System.Diagnostics;
using System.Net;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Resolver.Case.Api.Graph;
using Resolver.Case.Api.Services;

namespace Resolver.Case.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.AddLogging(builder => { builder.AddAzureWebAppDiagnostics(); });

            services.AddCors(option => {
                option.AddPolicy("AllowSpecificOrigin", policy => policy.WithOrigins(Configuration["Cors-Origins"].Split(',', StringSplitOptions.RemoveEmptyEntries)));
                option.AddPolicy("AllowMethods", policy => policy.WithMethods("GET", "POST", "PUT", "DELETE"));
            });

            services.AddMvc().AddJsonOptions(options => {
               options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            });


            services.AddAutoMapper(typeof(Startup));
            services.AddSwaggerDocument();

            services.AddTransient<IGraphService, GraphService>();
            services.AddTransient<IStorageRepository, StorageRepository>();

            var container = new ContainerBuilder();
            container.Populate(services);

            return new AutofacServiceProvider(container.Build());
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseExceptionHandler(builder =>
            {
                builder.Run(async context =>
                {
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    var error = context.Features.Get<IExceptionHandlerFeature>();

                    if (error != null)
                    {
                        Trace.TraceError(error.ToString());
                        await context.Response.WriteAsync(error.Error.Message);
                    }

                });
            });

            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
