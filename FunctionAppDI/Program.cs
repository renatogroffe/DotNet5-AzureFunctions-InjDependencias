using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using FunctionAppDI.Interfaces;
using FunctionAppDI.Implementations;

namespace FunctionAppDI
{
    public class Program
    {
        public static void Main()
        {
            var host = new HostBuilder()
                .ConfigureFunctionsWorkerDefaults()
                .ConfigureServices(services =>
                {
                    services.AddSingleton<ITesteA, TesteA>();
                    services.AddTransient<ITesteB, TesteB>();
                    services.AddScoped<TesteC>();
                    services.AddTransient<TesteInjecao>();
                })
                .Build();

            host.Run();
        }
    }
}