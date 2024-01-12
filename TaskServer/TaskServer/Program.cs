using Microsoft.AspNetCore;
using TaskServer;

public class Program
{
    public static void Main(string[] args)
    {
        BuildWebHost(args).Run();
    }
    public static IWebHost BuildWebHost(string[] args)
    {
        var builder = WebHost.CreateDefaultBuilder(args).UseStartup<Startup>().Build();
        return builder;
    }
}