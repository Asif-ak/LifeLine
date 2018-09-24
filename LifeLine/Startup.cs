using LifeLine_WebApi.DBConfiguration;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
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

            services.AddApiVersioning(opt =>
            {
                opt.ReportApiVersions = true;
                opt.AssumeDefaultVersionWhenUnspecified = true;
                opt.DefaultApiVersion = new ApiVersion(1, 0);
                //opt.ApiVersionReader = new HeaderApiVersionReader("api-version"); // we are not using HTTP hearders for reading version
            });
            

            services.AddMvc().AddJsonOptions(opt =>
            {
                opt.SerializerSettings.Formatting = Newtonsoft.Json.Formatting.Indented;
            });

            var connection = "Data Source=LifeLine1.db";
            services.AddDbContext<LifeLineContext>
            (Options => Options.UseSqlite(connection));

            services.AddSwaggerGen(swagger =>
            {
                swagger.SwaggerDoc("v1", new Swashbuckle.AspNetCore.Swagger.Info
                {
                    Version = "V1",
                    Title = "LifeLine Blood Donation Web Api",
                    Description = "A Dot Net Core based Web Api project for Blood Donation",
                    TermsOfService = "GNU-GPL3",
                    Contact = new Swashbuckle.AspNetCore.Swagger.Contact { Name = "Muhammad Asif", Email = "asif.ak@hotmail.com" }
                });
                var app = System.AppContext.BaseDirectory;
                swagger.IncludeXmlComments(System.IO.Path.GetFileName(app + GetXmlPath()));
                swagger.DescribeAllEnumsAsStrings();
                
            });
        }
        private string GetXmlPath()
        {
            
            var assemblyname = System.Reflection.Assembly.GetEntryAssembly().GetName().Name;
            var filename = System.IO.Path.GetFileName(assemblyname + ".xml");
            return filename;
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
            app.UseStaticFiles();
            app.UseSwagger();
            app.UseSwaggerUI(swagger =>
            {
                swagger.SwaggerEndpoint("/swagger/v1/swagger.json", "Lifeline API V1");
                
            });
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
