using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Music_Releases_Core.Repository;
using Music_Releases_Core.Repository.Interfaces;

namespace Music_Releases_Core.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;

            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("passes.json");
            Configuration = builder.Build();
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var AmazonAccessId = Configuration["AmazonAccessId"];
            var AmazonEndPoint = Configuration["AmazonEndPoint"];
            var AmazonAssociateTag = Configuration["AmazonAssociateTag"];
            var AmazonSecretKey = Configuration["AmazonSecretKey"];
            var ItunesAffiliateId = Configuration["ItunesAffiliateId"];
            var ItunesRequestUrl = Configuration["ItunesRequestUrl"];

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.AddTransient<IAmazonItemRepository>(s => new AmazonItemRepository(AmazonAccessId, AmazonEndPoint, AmazonAssociateTag, AmazonSecretKey));
            services.AddTransient<IItunesItemRepository>(s => new ItunesItemRepository(ItunesAffiliateId, ItunesRequestUrl));
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
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
