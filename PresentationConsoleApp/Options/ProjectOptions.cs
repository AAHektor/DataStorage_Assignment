using Business.Models;
using Business.Services;
using Presentation.ConsoleApp.Interfaces;

namespace Presentation.ConsoleApp.Options;

public class ProjectOptions
{
    private readonly ProjectService _projectService;
    private readonly CustomerService _customerService;
    private readonly IUserInterface _userInterface;

    public ProjectOptions(ProjectService projectService, CustomerService customerService, IUserInterface userInterface)
    {
        _projectService = projectService;
        _customerService = customerService;
        _userInterface = userInterface;
    }

    public async Task CreateNewProject()
    {
        try
        {

            var customers = await _customerService.GetCustomersAsync();
            var customerList = customers.ToList();

            var customerId = _userInterface.SelectCustomer(customerList);
            if (customerId == -1) return;

            var title = _userInterface.GetProjectTitle();
            if (title?.ToUpper() == "Q") return;

            var description = _userInterface.GetProjectDescription();
            if (description?.ToUpper() == "Q") return;

            var startDate = _userInterface.GetStartDate();
            var endDate = _userInterface.GetEndDate(startDate);

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
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
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
        _userInterface.DisplayMessage("\nProjects to update:");

        foreach (var project in projects)
        {
            _userInterface.DisplayMessage($"ID: {project.Id} | Project Number: {project.ProjectNumber} | Title: {project.Title}");
        }

        var id = _userInterface.GetValidProjectId("Enter project ID to update (or press 'Q' to cancel): ", projects);

        if (id == -1)
        {
            return;
        }

        try
        {
            var project = await _projectService.GetProjectByIdAsync(id);
            if (project == null)
            {
                _userInterface.DisplayMessage("Project not found.");
                return;
            }

            await GetUpdatedProjectDetails(project);

            var result = await _projectService.UpdateProjectAsync(project);
            _userInterface.DisplayMessage(result ? "Project updated successfully!" : "Project update failed.");
        }
        catch (Exception ex)
        {
            _userInterface.DisplayError();
        }

        Console.ReadLine();

    }

    public async Task ?GetUpdatedProjectDetails(Project project)
    {
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
    }


    public async Task DeleteProject()
    {
        var projects = await _projectService.GetProjectsAsync();
        Console.Clear();
        _userInterface.DisplayMessage("\nProjects to delete:");

        foreach (var project in projects)
        {
            _userInterface.DisplayMessage($"ID: {project.Id} | Project Number: {project.ProjectNumber} | Title: {project.Title}");
        }

        var id = _userInterface.GetValidProjectId("Enter Project ID to delete (or press 'Q' to cancel): ", projects);

        if (id == -1)
        {
            return;
        }

        try
        {
            var result = await _projectService.DeleteProjectAsync(id);
            _userInterface.DisplayMessage(result ? "Project deleted successfully!" : "Project deletion failed.");
        }
        catch (Exception ex)
        {
            _userInterface.DisplayError();
        }

        Console.ReadLine();


    }
}
