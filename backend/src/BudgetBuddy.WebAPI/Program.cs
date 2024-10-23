using BudgetBuddy.WebAPI.Services;

namespace BudgetBuddy.WebAPI;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        // The container DI is configured by the IoC project to isolate the WebAPI project from the rest of the application
        builder.Services.AddLayeredServices();
        var app = builder.Build();

        // Configure the HTTP request pipeline.
#if DEBUG
        app.UseSwagger();
        app.UseSwaggerUI();
#endif

#if DEBUG
        if (app.Environment.IsProduction())
        {
            app.UseHttpsRedirection();
        }
#endif

        app.Run();

    }
}
