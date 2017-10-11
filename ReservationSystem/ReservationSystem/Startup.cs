using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ReservationSystem.Data;
using ReservationSystem.Data.Common.Repositories;
using ReservationSystem.Data.Models;
using ReservationSystem.Services.Data;
using ReservationSystem.Services.Data.Contracts;
using ReservationSystem.Services.Web;
using ReservationSystem.Services.Web.Contracts;

namespace ReservationSystem
{
  public class Startup
  {
    public Startup(IConfiguration configuration)
    {
      this.Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {
      services.AddDbContext<ApplicationDbContext>(options =>
          options.UseSqlServer(this.Configuration.GetConnectionString("DefaultConnection")));

      services.AddIdentity<ApplicationUser, IdentityRole>(options =>
      {
        options.Password.RequireDigit = false;
        options.Password.RequiredLength = 3;
        options.Password.RequiredUniqueChars = 0;
        options.Password.RequireLowercase = true;
        options.Password.RequireNonAlphanumeric = false;
        options.Password.RequireUppercase = false;
      })
        .AddEntityFrameworkStores<ApplicationDbContext>()
        .AddDefaultTokenProviders();

      // Add application services.
      services.AddScoped<IDataInitializer, DataInitializer>();
      services.AddScoped<DbContext, ApplicationDbContext>();
      services.AddScoped(typeof(IEfDbRepository<>), typeof(EfDbRepository<>));
      services.AddScoped<IEfUnitOfWork, EfUnitOfWork>();
      services.AddScoped<IReservationService, ReservationService>();
      services.AddTransient<IEmailSender, EmailSender>();

      services.AddMvc();
      services.AddAutoMapper(typeof(Startup));
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IHostingEnvironment env, IDataInitializer dataInitializer)
    {
      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
        app.UseBrowserLink();
        app.UseDatabaseErrorPage();
      }
      else
      {
        app.UseExceptionHandler("/Home/Error");
      }

      app.UseStaticFiles();
      app.UseAuthentication();

      dataInitializer.Initialize(app);

      app.UseMvc(routes =>
      {
        routes.MapRoute(
            name: "areas",
            template: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

        routes.MapRoute(
                  name: "default",
                  template: "{controller=Calendar}/{action=Index}/{id?}");
      });
    }
  }
}
