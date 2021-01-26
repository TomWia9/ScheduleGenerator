using System;
using System.IO;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Linq;
using System.Reflection;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Serialization;

namespace ScheduleGenerator.Server
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllersWithViews()
                .AddNewtonsoftJson(setupAction =>
                {
                    setupAction.SerializerSettings.ContractResolver =
                        new CamelCasePropertyNamesContractResolver();
                })
                .AddXmlDataContractSerializerFormatters();
            
            services.AddRazorPages();

            services.AddMvc(options =>
            {
                options.ReturnHttpNotAcceptable = true;

                //options.Filters.Add(new ValidationFilter());
                options.Filters.Add(
                    new ProducesResponseTypeAttribute(StatusCodes.Status400BadRequest));
                options.Filters.Add(
                    new ProducesResponseTypeAttribute(StatusCodes.Status500InternalServerError));

                options.OutputFormatters.Add(new Microsoft.AspNetCore.Mvc.Formatters.XmlSerializerOutputFormatter());
            });

            services.AddSwaggerGenNewtonsoftSupport();

            services.AddSwaggerGen(setupAction =>
            {
                setupAction.SwaggerDoc(
                    "ScheduleGeneratorOpenAPISepcification",
                    new Microsoft.OpenApi.Models.OpenApiInfo()
                    {
                        Title = "ScheduleGenerator API",
                        Version = "1",
                        Description = "Through this API you can access schedules and users",
                        Contact = new Microsoft.OpenApi.Models.OpenApiContact()
                        {
                            Email = "tomaszwiatrowski9@gmail.com",
                            Name = "Tomasz Wiatrowski",
                            Url = new Uri("https://www.linkedin.com/in/tomasz-wiatrowski-279b00176/")
                        },
                        License = new Microsoft.OpenApi.Models.OpenApiLicense()
                        {
                            Name = "MIT License",
                            Url = new Uri("https://opensource.org/licenses/MIT")
                        }
                    });

                //OpenApiSecurityScheme securityDefinition = new OpenApiSecurityScheme()
                //{
                //    Name = "Bearer",
                //    BearerFormat = "JWT",
                //    Scheme = "bearer",
                //    Description = "Specify the authorization token.",
                //    In = ParameterLocation.Header,
                //    Type = SecuritySchemeType.Http,
                //};
                //setupAction.AddSecurityDefinition("jwt_auth", securityDefinition);

                //OpenApiSecurityScheme securityScheme = new OpenApiSecurityScheme()
                //{
                //    Reference = new OpenApiReference()
                //    {
                //        Id = "jwt_auth",
                //        Type = ReferenceType.SecurityScheme
                //    }
                //};
                //OpenApiSecurityRequirement securityRequirements = new OpenApiSecurityRequirement()
                //{
                //    {securityScheme, new string[] { }},
                //};
                //setupAction.AddSecurityRequirement(securityRequirements);

                //Collect all referenced projects output XML document file paths  
                var currentAssembly = Assembly.GetExecutingAssembly();
                var xmlDocs = currentAssembly.GetReferencedAssemblies()
                    .Union(new[] { currentAssembly.GetName() })
                    .Select(a => Path.Combine(Path.GetDirectoryName(currentAssembly.Location), $"{a.Name}.xml"))
                    .Where(File.Exists).ToList();

                foreach (var d in xmlDocs)
                {
                    setupAction.IncludeXmlComments(d);
                }


            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseWebAssemblyDebugging();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseSwagger();

            app.UseSwaggerUI(setupAction =>
            {
                setupAction.SwaggerEndpoint("/swagger/ScheduleGeneratorOpenAPISepcification/swagger.json", "ScheduleGenerator API");
                setupAction.RoutePrefix = "swagger";

                setupAction.DefaultModelExpandDepth(2);
                setupAction.DefaultModelRendering(Swashbuckle.AspNetCore.SwaggerUI.ModelRendering.Model);
                setupAction.DocExpansion(Swashbuckle.AspNetCore.SwaggerUI.DocExpansion.None);
            });

            app.UseHttpsRedirection();
            app.UseBlazorFrameworkFiles();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
                endpoints.MapControllers();
                endpoints.MapFallbackToFile("index.html");
            });
        }
    }
}
