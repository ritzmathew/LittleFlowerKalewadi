using LittleFlowerKalewadi.Server.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace LittleFlowerKalewadi.Server
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
            X509Certificate2 cert = null;
            // using (X509Store certStore = new X509Store(StoreName.My, StoreLocation.CurrentUser))
            // {
            //     certStore.Open(OpenFlags.ReadOnly);
            //     X509Certificate2Collection certCollection = certStore.Certificates.Find(
            //         X509FindType.FindByThumbprint,
            //         // Replace below with your cert's thumbprint
            //         "026DB67476FFE82DF5345CBC56D70B0160964C06",
            //         false);
            //     // Get the first cert with the thumbprint
            //     if (certCollection.Count > 0)
            //     {
            //         cert = certCollection[0];
            //         //Log.Logger.Information($"Successfully loaded cert from registry: {cert.Thumbprint}");
            //     }
            // }

            // Fallback to local file for development
            if (cert == null)
            {
                string path = Path.Combine(Directory.GetCurrentDirectory(), "lfps.pfx");
                string azPath = Path.Combine("D:", "home", "site", "wwwroot", "wwwroot", "lfps.pfx");
                if(File.Exists(azPath)) {
                    path = azPath;
                }
                //string path = Path.Combine(Directory.GetCurrentDirectory(), "lfps.pfx");
                cert = new X509Certificate2(path, "Up22mlFF");
                //Log.Logger.Information($"Falling back to cert from file. Successfully loaded: {cert.Thumbprint}");
            }

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));

            //services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
                //.AddEntityFrameworkStores<ApplicationDbContext>();

            // services.AddIdentityServer()
            //     .AddSigningCredential(cert)
            //     .AddApiAuthorization<ApplicationUser, ApplicationDbContext>();

            // services.AddAuthentication()
            //     .AddIdentityServerJwt();

            services.AddAuthentication(options =>
            {
                options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            }
            ).AddCookie();

            services.AddControllersWithViews();
            services.AddRazorPages();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
                app.UseWebAssemblyDebugging();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseBlazorFrameworkFiles();
            app.UseStaticFiles();


            app.UseRouting();

            //app.UseIdentityServer();
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
