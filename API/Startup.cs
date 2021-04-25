using API.Services;
using Datos;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using ModelsNotUse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API
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
            /*Para utilizar el API
            CORS para que un dominio distinto se pueda accesar */

            services.AddCors(options =>
            {
                options.AddPolicy(name: "DefaultCorsPolicy", builder =>
                {
                    builder.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader();
                });
            });


            /* SQL ----------------------------------------------------------------- */
            /* https://docs.microsoft.com/en-us/aspnet/core/fundamentals/dependency-injection?view=aspnetcore-5.0 */
            //Agrega Servicios de carpeta "Services", lambda Context
            services.AddScoped(p => new AutomovilService(p.GetService<Context>()));

            //Agrega Servicio el Context de Entity Framework usando default connection del appsettings.json
            services.AddDbContext<Context>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            /* mongodb --------------------------------------------------------------- */

            // requires using Microsoft.Extensions.Options
            services.Configure<MongodbSettings>(
                Configuration.GetSection(nameof(MongodbSettings)));

            services.AddSingleton<IMongodbSettings>(sp =>
                sp.GetRequiredService<IOptions<MongodbSettings>>().Value);

            //requires
            services.AddScoped<IBitacoraService, BitacoraService>();

            //services.AddSingleton<BitacoraService>();
            // requires using Microsoft.Extensions.Options

            /* netcore -------------------------------------------------------------- */

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "API", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "API v1"));
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            /*CORS para que un dominio distinto se pueda accesar*/
            app.UseCors("DefaultCorsPolicy");

            app.UseAuthorization();

            /*Para usar el API*/
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
