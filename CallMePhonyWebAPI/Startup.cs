using Microsoft.OpenApi.Models;
using System.Reflection;

namespace CallMePhonyWebAPI;

public class Startup {
    private readonly IConfiguration _configuration;

    public Startup(IConfiguration configuration) {
        _configuration = configuration;
    }

    public void ConfigureServices(IServiceCollection services) {
        services.AddMvc();
        services.AddSession();
        services.AddHttpLogging((options) => {
            options.LoggingFields = Microsoft.AspNetCore.HttpLogging.HttpLoggingFields.Request;
        });
        services.AddSwaggerGen(options => {
            options.SwaggerDoc("v1", new OpenApiInfo {
                Version = "v1",
                Title = "CallMePhonyWebAPI",
                Description = "An ASP.NET Core Web API for managing CallMePhony App",
            });

            var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
        });
    }

    public void Configure(IApplicationBuilder app) {
        app.UseSwagger();
        app.UseSwaggerUI(options => {
            options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
            options.RoutePrefix = string.Empty;
        });

        app.UseHttpLogging();

        app.UseHttpsRedirection();

        app.UseSession();

        app.UseRouting();

        app.UseEndpoints(endpoints => {
            endpoints.MapControllers();
        });
    }


}