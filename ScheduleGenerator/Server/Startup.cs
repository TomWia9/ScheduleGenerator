using AutoMapper;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json.Serialization;
using ScheduleGenerator.Server.Filters;
using ScheduleGenerator.Server.Helpers;
using ScheduleGenerator.Server.Models;
using ScheduleGenerator.Server.Repositories;
using ScheduleGenerator.Shared.Auth;
using ScheduleGenerator.Shared.Dto;
using ScheduleGenerator.Shared.Validators;
using Serilog;
using Swashbuckle.AspNetCore.Swagger;
using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

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

                options.Filters.Add(new ValidationFilter());
                options.Filters.Add(
                    new ProducesResponseTypeAttribute(StatusCodes.Status400BadRequest));
                options.Filters.Add(
                    new ProducesResponseTypeAttribute(StatusCodes.Status500InternalServerError));

                options.OutputFormatters.Add(new Microsoft.AspNetCore.Mvc.Formatters.XmlSerializerOutputFormatter());

            }).AddFluentValidation(options =>
            {
                options.ValidatorOptions.LanguageManager.Enabled = false;
            });


            services.AddTransient<IValidator<AuthenticateRequest>, AuthenticateRequestValidator>();
            services.AddTransient<IValidator<UserForCreationDto>, UserForCreationValidator>();
            services.AddTransient<IValidator<ScheduleForCreationDto>, ScheduleForCreationValidator>();
            services.AddTransient<IValidator<ScheduleForUpdateDto>, ScheduleForUpdateValidator>();
            services.AddTransient<IValidator<ScheduleItemForCreationDto>, ScheduleItemForCreationValidator>();
            services.AddTransient<IValidator<ScheduleItemForUpdateDto>, ScheduleItemForUpdateValidator>();

            services.AddDbContext<ScheduleGeneratorContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("ScheduleGeneratorConnection")));

            services.Configure<AppSettings>(Configuration.GetSection("AppSettings"));
            services.AddScoped<IDbRepository, DbRepository>();
            services.AddScoped<IUsersRepository, UsersRepository>();
            services.AddScoped<ISchedulesRepository, SchedulesRepository>();
            services.AddScoped<IScheduleItemsRepository, ScheduleItemsRepository>();

            services.AddAutoMapper(typeof(Startup));

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

                OpenApiSecurityScheme securityDefinition = new OpenApiSecurityScheme()
                {
                    Name = "Bearer",
                    BearerFormat = "JWT",
                    Scheme = "bearer",
                    Description = "Specify the authorization token.",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                };
                setupAction.AddSecurityDefinition("jwt_auth", securityDefinition);

                OpenApiSecurityScheme securityScheme = new OpenApiSecurityScheme()
                {
                    Reference = new OpenApiReference()
                    {
                        Id = "jwt_auth",
                        Type = ReferenceType.SecurityScheme
                    }
                };
                OpenApiSecurityRequirement securityRequirements = new OpenApiSecurityRequirement()
                {
                    {securityScheme, new string[] { }},
                };
                setupAction.AddSecurityRequirement(securityRequirements);

                //Collect all referenced projects output XML document file paths  
                var currentAssembly = Assembly.GetExecutingAssembly();
                var xmlDocs = currentAssembly.GetReferencedAssemblies()
                    .Union(new[] { currentAssembly.GetName() })
                    .Select(a => Path.Combine(Path.GetDirectoryName(currentAssembly.Location) ?? string.Empty, $"{a.Name}.xml"))
                    .Where(File.Exists).ToList();

                foreach (var d in xmlDocs)
                {
                    setupAction.IncludeXmlComments(d);
                }

                setupAction.AddFluentValidationRules();

            });

            var key = Encoding.UTF8.GetBytes(Configuration.GetValue<string>("AppSettings:Secret"));

            services.AddAuthentication(x =>
                {
                    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(x =>
                {
                    x.RequireHttpsMetadata = false;
                    x.SaveToken = true;
                    x.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(key),
                        ValidateIssuer = false,
                        ValidateAudience = false
                    };
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
                // setupAction.DocExpansion(Swashbuckle.AspNetCore.SwaggerUI.DocExpansion.None);
            });

            app.UseHttpsRedirection();
            app.UseBlazorFrameworkFiles();
            app.UseStaticFiles();

            app.UseSerilogRequestLogging();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
                endpoints.MapControllers();
                endpoints.MapFallbackToFile("index.html");
            });
        }
    }
}
