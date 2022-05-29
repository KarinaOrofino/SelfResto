using Framework.Common;
using Framework.Utils;
using Framework.Web.Seguridad;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Localization;
using KO.Datos.EFScafolding;
using KO.Datos.Interfaces;
using KO.Framework.Web;
using KO.Recursos;
using KO.Servicios;
using KO.Servicios.Interfaces;
using Servicios.Interfaces;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace Web
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
            services
                .AddControllersWithViews()
                .AddRazorRuntimeCompilation();

            services.AddTransient<IAutenticacionInterna, AutenticacionInterna>();                        
            services.AddSingleton<IAppInfo, AppInfo>();

            var cultureInfo = new CultureInfo("es-AR");
            cultureInfo.NumberFormat.NumberDecimalSeparator = ".";
            CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
            CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;

            NumberFormatInfo nfi = new CultureInfo("en-US").NumberFormat;
            nfi.NumberDecimalDigits = 2;

            ///Cookies            
            services.ConfigureApplicationCookie(options =>
            {
                // Cookie settings
                options.Cookie.HttpOnly = true;
                options.Cookie.Expiration = TimeSpan.FromDays(150);
                options.SlidingExpiration = true;
                options.LoginPath = "/Account/Login";
                options.LogoutPath = "/Account/Logout";
                options.AccessDeniedPath = "/Account/AccessDenied";
            });

            string idApp = Configuration[Constantes.IDAPP].ToString();

            services.AddAuthentication(idApp)
                .AddCookie(idApp, options =>
                {
                    options.LoginPath = "/Account/Login";
                    options.LogoutPath = "/Account/Logout";
                });

            ConfigurarMensajesDeModelo(services);

            services.Configure<SecurityStampValidatorOptions>(options => options.ValidationInterval = TimeSpan.FromHours(2));

            //Se registra el dbcontext
            string connectionString = Configuration.GetSection("ConnectionStrings").GetSection(Constantes.DB_CONFIG_KEY).Value;
            services.AddDbContext<KOContext>(options => options.UseSqlServer(connectionString).UseLazyLoadingProxies());
            services.AddDatabaseDeveloperPageExceptionFilter();

            //Se registran los Servicios
            services.Scan(scan => scan.FromAssemblyOf<IServicioBase>().AddClasses(false).AsMatchingInterface().WithTransientLifetime());

            //Se registran los Datos
            services.Scan(scan => scan.FromAssemblyOf<IDatosBase>().AddClasses(false).AsMatchingInterface().WithTransientLifetime());
#if DEBUG
            services.AddTransient<IServicioAutenticacion, ServicioAutenticacionMock>();
            services.AddTransient<IServicioLogEventos, ServicioLogEventosMock>();
            services.AddTransient<IServicioRoles, ServicioRolesMock>();
            services.AddTransient<IServicioSocket, ServicioSocketMock>();            
#endif
            services.AddSignalR();
            services.AddWebOptimizer(pipeline =>
            {
                pipeline.MinifyJsFiles("js/**/*.js");
                pipeline.MinifyCssFiles();
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            //Configuraciones para ambientes de Dev y Prod
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                SeedDatabase(app);
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }
            
            app.UseHttpsRedirection();

            if (!env.IsDevelopment())
                app.UseWebOptimizer();

            app.UseStaticFiles();
            //Para que tome los .vue en el mime type
            app.UseStaticFiles(new StaticFileOptions
            {
                ContentTypeProvider = new FileExtensionContentTypeProvider(
                    new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase) {
                        { ".vue", "application/javascript" }
                    }),
            });
            app.UseCookiePolicy();
            app.UseAuthentication();
            app.UseRouting();
            app.UseAuthorization();

            DefaultFilesOptions DefaultFile = new DefaultFilesOptions();
            DefaultFile.DefaultFileNames.Clear();
            DefaultFile.DefaultFileNames.Add("/Producto/Listado");
            app.UseDefaultFiles(DefaultFile);          

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                
            });
        }

        //Realiza el seed de la base de datos para el ambiente de desarrollo
        private static void SeedDatabase(IApplicationBuilder app)
        {   
            using (var serviceScope = app.ApplicationServices
                .GetRequiredService<IServiceScopeFactory>()
                .CreateScope())
            {
                using (var context = serviceScope.ServiceProvider.GetService<KOContext>())
                {
                    KOInitializer.Initialize(context);
                }
            }
        }

        //Setea mensajes de validacion de modelo, tomandolos de Global.resx        
        public void ConfigurarMensajesDeModelo(IServiceCollection services)
        {
            services.AddLocalization();
            services.AddMvc(options =>
            {
                options.Filters.Add(new CheckSession());

                var factory = services.BuildServiceProvider().GetService<IStringLocalizerFactory>();
                var localizer = factory.Create(typeof(Global));
                /*
                options.ModelBindingMessageProvider.SetValueIsInvalidAccessor(
                    (x) => localizer[nameof(Global.ValueIsInvalid), x]);
                options.ModelBindingMessageProvider.SetValueMustBeANumberAccessor(
                    (x) => localizer[nameof(Global.ValueMustBeANumber), x]);
                options.ModelBindingMessageProvider.SetMissingBindRequiredValueAccessor(
                    (x) => localizer[nameof(Global.MissingBindRequired), x]);
                options.ModelBindingMessageProvider.SetAttemptedValueIsInvalidAccessor(
                    (x, y) => localizer[nameof(Global.AttemptedValueIsInvalid), x, y]);
                options.ModelBindingMessageProvider.SetMissingKeyOrValueAccessor(
                    () => localizer[nameof(Global.MissingKeyOrValue)]);
                options.ModelBindingMessageProvider.SetUnknownValueIsInvalidAccessor(
                    (x) => localizer[nameof(Global.UnknownValueIsInvalid), x]);
                options.ModelBindingMessageProvider.SetValueMustNotBeNullAccessor(
                    (x) => localizer[nameof(Global.ValueMustNotBeNull), x]);*/
            })
            .AddDataAnnotationsLocalization()
            .AddViewLocalization();
        }
    }
}