using Autofac;
using TaskServer.Controllers;
using TaskServer.Models;
using TaskServer.Repository;
using TaskServer.Repository.Interface;

namespace TaskServer.Dependency;

public class DependencyModules : Module
{
    public IConfiguration Configuration { get; set; }
    
    public DependencyModules() { }

    public DependencyModules(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    protected override void Load(ContainerBuilder builder)
    {
        base.Load(builder);
        string taskConnectionString = Configuration["TaskDbConnectionString"] ?? string.Empty;

        builder.RegisterType<TaskRepository>()
            .As<ITaskRepository>()
            .WithParameter("connectionString", taskConnectionString)
            .SingleInstance();
        builder.RegisterType<AuthRepository>()
            .As<IAuthRepository>()
            .WithParameter("connectionString", taskConnectionString)
            .SingleInstance();

        builder.RegisterType<AuthController>()
            .SingleInstance();
        builder.RegisterType<TaskController>()
            .SingleInstance();
    }
}