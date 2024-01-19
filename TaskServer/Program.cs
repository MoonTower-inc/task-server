using Autofac.Extensions.DependencyInjection;

namespace TaskServer;

public class Program
{
    public static void Main(string[] args)
    {
        CreateHostBuilder(args).Build().Run();
    }

    private static IHostBuilder CreateHostBuilder(string[] args)
    {
        var builder = Host.CreateDefaultBuilder()
            .UseServiceProviderFactory(new AutofacServiceProviderFactory())
            .ConfigureWebHostDefaults(builder =>
            {
                // builder.UseKestrel(options =>
                // {
                //     options.Limits.MaxRequestBodySize = null;
                // });
                builder.UseStartup<Startup>();
            });
        return builder;
    }
}