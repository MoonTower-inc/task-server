namespace TaskServer;

public class Startup
{
    public void ConfigureServices(IServiceCollection services)
    {
        var testCors = "_testCors";
        
        services.AddCors(options =>
        {
            options.AddPolicy(name: testCors,
                policy =>
                {
                    policy.WithOrigins("*")
                        .AllowAnyHeader();
                });
        });
        services.AddMvc();
        services.AddControllersWithViews(options => options.EnableEndpointRouting = false);
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        app.UseCors(x => x
            .AllowAnyMethod()
            .AllowAnyHeader()
            .SetIsOriginAllowed(origin => true)
            .AllowCredentials());

        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }

        app.UseRouting();
        app.UseMvc();
    }
}