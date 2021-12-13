using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using DMSapi.Models;
using DMSapi.Services;

namespace DMSapi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices( IServiceCollection services)
        {
            // requires using Microsoft.Extensions.Options
            services.Configure<DatabaseSetting>(
            Configuration.GetSection(nameof(DatabaseSetting)));

            services.AddSingleton<DatabaseSetting>(sp =>
            sp.GetRequiredService<IOptions<DatabaseSetting>>().Value);

            services.AddSingleton<RoomService>();
            services.AddSingleton<UserService>();
            services.AddSingleton<MeterService>();


            services.AddControllers();
            services.AddSwaggerGen();
            services.AddCors();
            // services.AddMvc();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // app.UseMvc();
            app.UseCors(options => options.AllowAnyMethod()
            .AllowAnyHeader()
            .AllowAnyOrigin()
            .SetIsOriginAllowed(origin => true)
            // .AllowCredentials()
            );
            app.UseSwagger();
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "DMSapi v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
