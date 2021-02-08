using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net.Http;
using LittleFlowerKalewadi.ViewModels;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.Authorization;

namespace LittleFlowerKalewadi.Client
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("app");

            builder.Services.AddOptions();
            builder.Services.AddAuthorizationCore();

            builder.Services.AddTransient(sp => 
                new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

            builder.Services.AddHttpClient<ILoginViewModel, LoginViewModel>
                ("LitteFlowerKalewadi.ServerAPI", client => client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress));
            
            builder.Services.AddHttpClient<IRegisterViewModel, RegisterViewModel>
                ("LitteFlowerKalewadi.ServerAPI", client => client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress));

            builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthenticationStateProvider>();

            // builder.Services.AddHttpClient("LittleFlowerKalewadi.ServerAPI", client => client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress))
            //     .AddHttpMessageHandler<BaseAddressAuthorizationMessageHandler>();

            // // Supply HttpClient instances that include access tokens when making requests to the server project
            // builder.Services.AddScoped(sp => sp.GetRequiredService<IHttpClientFactory>().CreateClient("LittleFlowerKalewadi.ServerAPI"));

            //builder.Services.AddApiAuthorization();

            await builder.Build().RunAsync();
        }
    }
}
