using Business.Models;
using Business.Services;

namespace Presentation.ConsoleApp;

public class MenuDialogsProject : IMenuDialogs
{
    private readonly CustomerService _customerService;
    private readonly ProjectService _projectService;

    public MenuDialogsProject(CustomerService customerService, ProjectService projectService)
    {
        _customerService = customerService;
        _projectService = projectService;
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
                    await CreateNewProject();
                    break;
                case "2":
                    await GetAllProjects();
                    break;
                case "3":
                    await UpdateProject();
                    break;
                case "4":
                    await DeleteProject();
                    break;
                case "5":
                    return;
                default:
                    Console.WriteLine("Invalid choice, please try again!");
                    break;
            }
        }
    }


    public async Task CreateNewProject()
    {
        try
        {
            var customers = await _customerService.GetCustomersAsync();
            var customerList = customers.ToList();

            Console.WriteLine("Select a customer (or press 'Q' to cancel):");
            for (int i = 0; i < customerList.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {customerList[i].CustomerName} (ID: {customerList[i].Id})");
            }

            Console.Write("Enter the customer number: ");
            var input = Console.ReadLine();
            if (input?.ToUpper() == "Q") return;

            if (!int.TryParse(input, out int customerIndex) || customerIndex < 1 || customerIndex > customerList.Count)
            {
                Console.WriteLine("Invalid selection.");
                return;
            }
            int customerId = customerList[customerIndex - 1].Id;

            Console.Write("Enter project title (or press 'Q' to cancel): ");
            var title = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(title) || title.ToUpper() == "Q") return;

            Console.Write("Enter project description (optional, press 'Q' to cancel): ");
            var description = Console.ReadLine();
            if (description?.ToUpper() == "Q") return;

            Console.Write("Enter project start date (yyyy-MM-dd, or 'Q' to cancel): ");
            if (!DateTime.TryParse(Console.ReadLine(), out DateTime startDate)) return;

            Console.Write("Enter project end date (yyyy-MM-dd, or 'Q' to cancel): ");
            if (!DateTime.TryParse(Console.ReadLine(), out DateTime endDate) || endDate < startDate)
            {
                Console.WriteLine("Invalid end date.");
                return;
            }

            var projectForm = new ProjectRegistrationForm
            {
                Title = title,
                Description = description,
                StartDate = startDate,
                EndDate = endDate,
                CustomerId = customerId
            };

            var result = await _projectService.CreateProjectAsync(projectForm);
            Console.WriteLine(result ? "Project created successfully!" : "Project creation failed.");
        }
        catch
        {
            Console.WriteLine("Error");
        }

        Console.ReadLine();
    }


    public async Task GetAllProjects()
    {
        var projects = await _projectService.GetProjectsAsync();
        foreach (var project in projects)
        {
            Console.WriteLine($"ID: {project.Id} | Project Number: {project.ProjectNumber} | Title: {project.Title} | Customer: {project.CustomerName}");
        }
        Console.ReadLine();
    }

    public async Task UpdateProject()
    {
        Console.Write("Enter project ID to update (or press 'Q' to cancel): ");
        var input = Console.ReadLine();

        if (input?.ToUpper() == "Q")
        {
            return;
        }

        try
        {
            if (int.TryParse(input, out var id))
            {
                var project = await _projectService.GetProjectByIdAsync(id);
                if (project == null)
                {
                    Console.WriteLine("Project not found.");
                    return;
                }
                Console.Write($"New Title ({project.Title}): ");
                var title = Console.ReadLine();
                project.Title = string.IsNullOrWhiteSpace(title) ? project.Title : title;

                var result = await _projectService.UpdateProjectAsync(project);
                Console.WriteLine(result ? "Project updated successfully!" : "Project update failed.");

            }
            else
            {
                Console.WriteLine("Invalid project ID format.");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error");
        }

        Console.ReadLine();

    }

    public async Task DeleteProject()
    {
        Console.WriteLine("Enter Project ID to delete (or press 'Q' to cancel): ");
        var input = Console.ReadLine();

        if (input?.ToUpper() == "Q")
        {
            return;
        }

        try
        {
            if (int.TryParse(input, out var id))
            {
                var result = await _projectService.DeleteProjectAsync(id);
                Console.WriteLine(result ? "Project deleted successfully!" : "Project deletion failed.");

            }
            else
            {
                Console.WriteLine("Invalid input! Please enter Project ID.");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("invalid input, try again.");
        }

        Console.ReadLine();
    }
}
