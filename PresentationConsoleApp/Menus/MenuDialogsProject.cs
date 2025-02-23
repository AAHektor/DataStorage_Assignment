using Presentation.ConsoleApp.Interfaces;
using Presentation.ConsoleApp.Options;

namespace Presentation.ConsoleApp.Menus;

public class MenuDialogsProject : IMenuDialogs
{
    private readonly ProjectOptions _projectOptions;

    public MenuDialogsProject(ProjectOptions projectOptions)
    {
        _projectOptions = projectOptions;
    }

    public async Task ShowMenu()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("Manage Projects");
            Console.WriteLine("1. Create Projects");
            Console.WriteLine("2. List Projects");
            Console.WriteLine("3. Update Projects");
            Console.WriteLine("4. Delete Projects");
            Console.WriteLine("5. Return to Main Menu");

            var option = Console.ReadLine();

            switch (option)
            {
                case "1":
                    await _projectOptions.CreateNewProject();
                    break;
                case "2":
                    await _projectOptions.GetAllProjects();
                    break;
                case "3":
                    await _projectOptions.UpdateProject();
                    break;
                case "4":
                    await _projectOptions.DeleteProject();
                    break;
                case "5":
                    return;
                default:
                    Console.WriteLine("Invalid choice, please try again!");
                    break;
            }
        }
    }


    
}
