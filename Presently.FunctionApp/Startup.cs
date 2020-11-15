using Microsoft.AspNetCore.Http;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Azure.WebJobs.Host.Bindings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Presently.Common.Models;
using Presently.FunctionApp.Providers;
using Presently.FunctionApp.Providers.Abstractions;
using Presently.FunctionApp.Services;
using Presently.FunctionApp.Services.Abstractions;
using System.IO;

[assembly: FunctionsStartup(typeof(Presently.FunctionApp.Startup))]

namespace Presently.FunctionApp
{
    public class Startup : FunctionsStartup
    {
        private IConfigurationRoot _functionConfig = null;

        private IConfigurationRoot FunctionConfig(string appDir) =>
            _functionConfig ??= new ConfigurationBuilder()
            .AddJsonFile(Path.Combine(appDir, "appsettings.json"), optional: true, reloadOnChange: true)
            .AddEnvironmentVariables()
            .Build();

        public override void Configure(IFunctionsHostBuilder builder)
        {
            _ = builder.Services.BuildServiceProvider()
                .GetService<IOptions<ExecutionContextOptions>>().Value;

            builder.Services.AddLogging();

            builder.Services.AddOptions<AppSettings>()
                .Configure<IOptions<ExecutionContextOptions>>((appSettings, exeContext) =>
                FunctionConfig(exeContext.Value.AppDirectory).GetSection("AppSettings").Bind(appSettings));

            builder.Services.AddSingleton<IAccessTokenProvider, AccessTokenProvider>();
            builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            builder.Services.AddSingleton<IAuthService, AuthService>();
            builder.Services.AddSingleton<IEmployeeService, EmployeeService>();
            builder.Services.AddSingleton<IEmployeeSiteService, EmployeeSiteService>();
            builder.Services.AddSingleton<IAttendanceLogService, AttendanceLogService>();
        }
    }
}
