using AutoMapper;
using Fluxor;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using vagbhatapp.Data.Application.Behaviours;
using vagbhatapp.Data.Application.Command;
using vagbhatapp.Data.Application.Queries;
using vagbhatapp.Data.Core;
using vagbhatapp.Data.Services;

namespace vagbhatapp
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
            services.BuildMediator();
            services.AddCustomDbContext(Configuration)
                .AddCustomIntegrations()
                .AddAutoMapper(typeof(Startup));
            services.AddFluxor(options =>
            {
                options.ScanAssemblies(typeof(Startup).Assembly);
            });
            services.AddRazorPages();
            services.AddServerSideBlazor();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
            }

            app.UseStaticFiles();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapBlazorHub();
                endpoints.MapFallbackToPage("/_Host");
            });
        }
    }
    static class CustomExtensionsMethods
    {
        public static IServiceCollection AddCustomDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContextPool<EntitiesContext>(
                options => options.UseJet(configuration.GetConnectionString("DefaultConnection")));

            return services;
        }

        public static IMediator BuildMediator(this IServiceCollection services)
        {
            services.AddScoped<ServiceFactory>(p => p.GetService);


            services.AddMediatR(typeof(CreatePatientCommandAsync));
            services.AddMediatR(typeof(CreatePatientCommandAsyncHandler));
            services.AddMediatR(typeof(GetPatientQueryAsync));
            services.AddMediatR(typeof(GetPatientQueryAsyncHandler));

            var provider = services.BuildServiceProvider();

            return provider.GetRequiredService<IMediator>();
        }
        public static IServiceCollection AddCustomIntegrations(this IServiceCollection services)
        {
            services.AddScoped<EntitiesContext>();
            services.AddScoped(typeof(IEntityRepository<>), typeof(EntityRepository<>));
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));
            services.AddScoped(typeof(IPipelineBehavior<,>), typeof(TransactionBehaviour<,>));
            //services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            //services.AddScoped<IAuthorizationHandler, IsUserDeletedHandler>();

            //services.AddScoped<IAccessService, AccessService>();
            //services.AddScoped<IRefreshTokenService, RefreshTokenService>();
            //services.AddScoped<IUserService, UserService>();
            //services.AddScoped<IRoleService, RoleService>();
            services.AddScoped<ICommonService, CommonService>();
            services.AddScoped<IPatientService, PatientService>();
            services.AddScoped<IAddressService, AddressService>();
            services.AddScoped<IAppointmentService, AppointmentService>();
            services.AddScoped<ITreatmentService, TreatmentService>();

            return services;
        }
    }
}
