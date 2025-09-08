using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using ToDoList.Infrastructure.Database;
using ToDoList.Infrastructure.TimeProvider;

namespace ToDoList.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddMongoDb(this IServiceCollection services)
    {
        return services.AddSingleton<IMongoClient>(provider =>
        {
            IConfiguration configuration = provider.GetRequiredService<IConfiguration>();

            string connectionString = configuration.GetConnectionString("ToDoListDatabase")
                                      ?? throw new ArgumentException("Connection string was not provided.");

            MongoClientSettings mongoClientSettings = MongoClientSettings
                .FromConnectionString(connectionString);
            
            return new MongoClient(mongoClientSettings);
        });
    }
    
    public static IServiceCollection AddDao(this IServiceCollection services)
    {
        return services.AddScoped<IToDoTasksDao, MongoToDoTasksDao>();
    }

    public static IServiceCollection AddTimeProvider(this IServiceCollection services)
    {
        return services.AddTransient<ITimeProvider, UtcTimeProvider>();
    }
}