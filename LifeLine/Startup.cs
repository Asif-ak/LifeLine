using LifeLine_WebApi.DBConfiguration;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace LifeLine
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // we cannot setup antiforgery key on open api.
            //services.AddMvc(options =>
            //{
            //    options.Filters.Add(new AutoValidateAntiforgeryTokenAttribute());
            //}).SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.AddMvc(options=>
            {
                options.Filters.Add(new RequireHttpsAttribute()); // to make sure we are https only.
            }).SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.AddMvc().AddJsonOptions(opt =>
            {
                opt.SerializerSettings.Formatting = Newtonsoft.Json.Formatting.Indented;
            });

            var connection = "Data Source=LifeLine1.db";
            services.AddDbContext<LifeLineContext>
            (Options => Options.UseSqlite(connection));
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
            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = Microsoft.AspNetCore.HttpOverrides.ForwardedHeaders.XForwardedFor | Microsoft.AspNetCore.HttpOverrides.ForwardedHeaders.XForwardedProto
            });

            
            app.UseHsts(opt => opt.MaxAge(days: 365).IncludeSubdomains());
            app.UseXXssProtection(opt => opt.EnabledWithBlockMode());
            app.UseXContentTypeOptions();
            app.UseXfo(opt => opt.Deny());
            app.UseReferrerPolicy(opt => opt.SameOrigin());
            
            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
