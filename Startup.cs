﻿using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PaginaPessoal_BD.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaginaPessoal_BD
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
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));

            services.AddDatabaseDeveloperPageExceptionFilter();

            services.AddIdentity<IdentityUser, IdentityRole>(options =>
            {
                //Sign in
                options.SignIn.RequireConfirmedAccount = false; //para não ser preciso por exemplo validação por e-amil

                //Password
                options.Password.RequireDigit = true; //digito na password obrigatorio
                options.Password.RequireUppercase = true; //obriga a ter caracter maiusculo
                options.Password.RequiredLength = 6; //minimo de 6 caracteres
            })

                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultUI(); //adiciona o User Interface (o que possibilita o Registo e Login)
            services.AddControllersWithViews();

            services.AddDbContext<PaginaPessoalBDContext>(options =>
                    options.UseSqlServer(Configuration.GetConnectionString("PaginaPessoalBDContext")));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, PaginaPessoalBDContext context, UserManager<IdentityUser> gestorUtilizadores, RoleManager<IdentityRole> gestorRoles)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });

            SeedData.InsereRolesAsync(gestorRoles).Wait();  //wait no fim da função é para a não tornar asincrona
            SeedData.InsereAdministradorPadraoAsync(gestorUtilizadores).Wait();

            if (env.IsDevelopment())
            {
                SeedData.PreencheDadosFicticiosExperiencia(context);
                SeedData.InsereUsuariosFicticiosAsync(gestorUtilizadores).Wait();
            }

        }
    }
}
