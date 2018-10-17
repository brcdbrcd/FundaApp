using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FundaApp.DAO;
using FundaApp.Logger;
using FundaApp.Models.Conf;
using FundaApp.ScheduledTasks;
using FundaApp.Service;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace FundaApp
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
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.Configure<Settings>(options =>
            {
                options.FundaUri1 = Configuration.GetSection("FundaUri").Value
                                    + Configuration.GetSection("TemporaryKey").Value
                                    + Configuration.GetSection("Param0").Value
                                    + Configuration.GetSection("Param2").Value
                                    + Configuration.GetSection("Param3").Value;
                options.FundaUri2 = Configuration.GetSection("FundaUri").Value
                                    + Configuration.GetSection("TemporaryKey").Value
                                    + Configuration.GetSection("Param0").Value
                                    + Configuration.GetSection("Param1").Value  // houses with a garden (tuin)
                                    + Configuration.GetSection("Param2").Value
                                    + Configuration.GetSection("Param3").Value;
                options.ConnectionString = Configuration.GetSection("MongoConnection:ConnectionString").Value;
                options.Database = Configuration.GetSection("MongoConnection:Database").Value;
                options.RefreshTimeInMinutes = Configuration.GetValue<int>("RefreshTimeInMinutes");
            });
            services.AddTransient<IMakelaarRepository, MakelaarRepository>();
            services.AddTransient<IFundaService, FundaService>();
            services.AddSingleton<IHostedService, ImportTask>();
            services.AddSingleton<ILoggerManager, LoggerManager>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, Microsoft.AspNetCore.Hosting.IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
