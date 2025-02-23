using Business.Models;
using Business.Services;

namespace Presentation.ConsoleApp.Options;

public class ProjectOptions
{
    private readonly ProjectService _projectService;
    private readonly CustomerService _customerService;

    public ProjectOptions(ProjectService projectService, CustomerService customerService)
    {
        _projectService = projectService;
        _customerService = customerService;
    }

    public async Task CreateNewProject()
    {
        try
        {
            var customers = await _customerService.GetCustomersAsync();
            var customerList = customers.ToList();

            Console.Clear();
            Console.WriteLine("\nSelect a customer (or press 'Q' to cancel):");
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
        Console.Clear();
        foreach (var project in projects)
        {
            Console.WriteLine($"\nID: {project.Id}");
            Console.WriteLine($"Project Number: {project.ProjectNumber}");
            Console.WriteLine($"Title: {project.Title}");
            Console.WriteLine($"Description: {project.Description}");
            Console.WriteLine($"Start Date: {project.StartDate}");
            Console.WriteLine($"End Date: {project.EndDate}");
            Console.WriteLine($"Customer: {project.CustomerName}");
        }
        Console.ReadLine();
    }

    public async Task UpdateProject()
    {
        var projects = await _projectService.GetProjectsAsync();
        Console.Clear();
        Console.WriteLine("\nProjects to update:");

        foreach (var project in projects)
        {
            Console.WriteLine($"ID: {project.Id} | Project Number: {project.ProjectNumber} | Title: {project.Title}");
        }

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

                Console.Write($"New Description ({project.Description}): ");
                var description = Console.ReadLine();
                project.Description = string.IsNullOrWhiteSpace(description) ? project.Description : description;

                Console.Write($"New Start Date (Current: {project.StartDate:yyyy-MM-dd}): ");
                var startDateInput = Console.ReadLine();
                if (DateTime.TryParse(startDateInput, out DateTime newStartDate))
                {
                    project.StartDate = newStartDate;
                }

                Console.Write($"New End Date (Current: {project.EndDate:yyyy-MM-dd}): ");
                var endDateInput = Console.ReadLine();
                if (DateTime.TryParse(endDateInput, out DateTime newEndDate))
                {
                    project.EndDate = newEndDate;
                }


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
        var projects = await _projectService.GetProjectsAsync();
        Console.Clear();
        Console.WriteLine("\nProjects to delete:");

        foreach (var project in projects)
        {
            Console.WriteLine($"ID: {project.Id} | Project Number: {project.ProjectNumber} | Title: {project.Title}");
        }

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
