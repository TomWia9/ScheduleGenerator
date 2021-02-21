using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using ScheduleGenerator.Client.Services;
using ScheduleGenerator.Client.Shared;
using ScheduleGenerator.Shared.Auth;
using ScheduleGenerator.Shared.Dto;
using ScheduleGenerator.Shared.Validators;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace ScheduleGenerator.Client
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");

            builder.Services.AddAutoMapper(typeof(Program));

            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

            builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();
            builder.Services.AddScoped<IUsersService, UsersService>();
            builder.Services.AddScoped<ILocalStorageService, LocalStorageService>();
            builder.Services.AddScoped<ISchedulesService, SchedulesService>();
            builder.Services.AddScoped<IScheduleItemsService, ScheduleItemsService>();
            builder.Services.AddScoped<IHttpService, HttpService>();

            builder.Services.AddTransient<IValidator<AuthenticateRequest>, AuthenticateRequestValidator>();
            builder.Services.AddTransient<IValidator<UserForCreationDto>, UserForCreationValidator>();
            builder.Services.AddTransient<IValidator<ScheduleForCreationDto>, ScheduleForCreationValidator>();
            builder.Services.AddTransient<IValidator<ScheduleForUpdateDto>, ScheduleForUpdateValidator>();
            builder.Services.AddTransient<IValidator<ScheduleItemForCreationDto>, ScheduleItemForCreationValidator>();
            builder.Services.AddTransient<IValidator<ScheduleItemForUpdateDto>, ScheduleItemForUpdateValidator>();

            ValidatorOptions.Global.LanguageManager.Enabled = false;


            builder.Services.AddSingleton<SchedulesState>();
            builder.Services.AddSingleton<ScheduleItemsState>();

            var host = builder.Build();

            var authenticationService = host.Services.GetRequiredService<IAuthenticationService>();
            await authenticationService.Initialize();

            await host.RunAsync();

        }
    }
}
