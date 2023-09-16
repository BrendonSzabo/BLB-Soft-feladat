using Endpoint.Services;
using Logic.Interface;
using Logic.Logic;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Models;
using Repository;
using Repository.DbRepository;
using Repository.ModelRepository;
using System.IO;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Endpoint
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
            services.AddTransient<DatabaseContext>();

            services.AddTransient<IRepository<Models.Task>, TaskRepository>();
            services.AddTransient<IRepository<User>, UserRepository>();

            services.AddTransient<IUser, UserLogic>();
            services.AddTransient<ITask, TaskLogic>();

            services.AddSignalR();

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Endpoint", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Endpoint v1");
                });
            }
            app.UseExceptionHandler(t => t.Run(async context =>
            {
                var exc = context.Features
                .Get<IExceptionHandlerFeature>()
                .Error;
                var msg = new { Msg = exc.Message };
                await context.Response.WriteAsJsonAsync(msg);
            }
            ));



            app.UseCors(x => x
                .AllowCredentials()
                .AllowAnyMethod()
                .AllowAnyHeader()
                .WithOrigins("http://localhost:5213"));

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHub<SignalRHub>("/hub");
            });
        }
    }
}
