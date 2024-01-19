using Autofac;
using TaskServer.Dependency;

namespace TaskServer;

public class Startup
{
    public readonly IConfiguration Configuration;

    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public void ConfigureServices(IServiceCollection services)
    {
        var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

        services.AddSwaggerGen();
        
        services.AddCors(options =>
        {
            options.AddPolicy(name: MyAllowSpecificOrigins,
                policy =>
                {
                    policy.WithOrigins("*")
                        .AllowAnyHeader();
                });
        });
        
        services.AddMvc().AddControllersAsServices();
        
        services.AddMvc(options =>
        {
            options.EnableEndpointRouting = false;
        });
    }

    public void ConfigureContainer(ContainerBuilder builder)
    {
        builder.RegisterModule(new DependencyModules
        {
            Configuration = Configuration
        });
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }
        
        app.UseCors(x => x
            .AllowAnyMethod()
            .AllowAnyHeader()
            //.SetIsOriginAllowed(origin => true)
            .AllowCredentials()
        );

        app.UseDeveloperExceptionPage();
        app.UseRouting();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
    }
}