using Business.Services;
using Data.Contexts;
using Data.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Presentation.ConsoleApp.Interfaces;
using Presentation.ConsoleApp.Services;
using Presentation.ConsoleApp.Options;
using Presentation.ConsoleApp.Menus;

namespace Presentation.ConsoleApp;

class Program
{
    public static async Task Main(string[] args)
    {
        var services = new ServiceCollection()
            .AddDbContext<DataContext>(x => x.UseSqlServer("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Projects\\DataStorage_Assignment\\Data\\Databases\\local_database.mdf;Integrated Security=True;Connect Timeout=30"))
            .AddScoped<CustomerRepository>()
            .AddScoped<ProjectRepository>()
            .AddScoped<CustomerService>()
            .AddScoped<ProjectService>()
            .AddScoped<MenuDialogsCustomer>()
            .AddScoped<MenuDialogsProject>()
            .AddScoped<MenuDialogs>()
            .AddScoped<IUserInterface,UserInterface>()
            .AddScoped<CustomerOptions>()
            .AddScoped<ProjectOptions>()
            .BuildServiceProvider();

        var menuDialogs = services.GetRequiredService<MenuDialogs>();
        await menuDialogs.MenuOptions();
    }
}
