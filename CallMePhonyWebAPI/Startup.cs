using System.Reflection;
using CallMePhonyWebAPI.Data;
using CallMePhonyWebAPI.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

namespace CallMePhonyWebAPI;

public class Startup
{
    private readonly IConfiguration _configuration;

    public Startup(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddControllers().AddNewtonsoftJson(options => options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);
        services.AddMvc();
        services.AddSession();
        services.AddHttpLogging((options) =>
        {
            options.LoggingFields = Microsoft.AspNetCore.HttpLogging.HttpLoggingFields.Request;
        });
        services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo
            {
                Version = "v1",
                Title = "CallMePhonyWebAPI",
                Description = "An ASP.NET Core Web API for managing CallMePhony App",
            });

            var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
            //options.OrderActionsBy((apiDesc) => $"{apiDesc.ActionDescriptor.RouteValues["controller"]}_{apiDesc.HttpMethod}");
        });

        services.AddDbContext<CallMePhonyDbContext>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IServiceService, ServiceService>();
        services.AddScoped<ISiteService, SiteService>();
    }

    public void Configure(IApplicationBuilder app, CallMePhonyDbContext context)
    {
        context.Database.EnsureCreated();

        app.UseSwagger();
        app.UseSwaggerUI(options =>
        {
            options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
            options.RoutePrefix = string.Empty;
        });

        app.UseHttpLogging();

        app.UseHttpsRedirection();

        app.UseSession();

        app.UseRouting();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
    }


}