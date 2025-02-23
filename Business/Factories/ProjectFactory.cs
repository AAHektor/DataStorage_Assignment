using Business.Models;
using Data.Entities;
using Data.Repositories;

namespace Business.Factories;

public static class ProjectFactory
{

    public static async Task<ProjectEntity?> CreateAsync(ProjectRegistrationForm form, ProjectRepository projectRepository)
    {
        if (form == null)
            return null;

        string projectNumber = await GenerateProjectNumberAsync(projectRepository);

        return new ProjectEntity
        {
            Title = form.Title,
            Description = form.Description,
            StartDate = form.StartDate,
            EndDate = form.EndDate,
            CustomerId = form.CustomerId ?? 0,
            ProjectNumber = projectNumber
        };
    }

    // Med hjälp av ChatGPT.
    // Denna metod skapar ett nytt projektnummer genom att kolla på det senaste projektnumret i databasen,
    // ta bort "P-" och öka numret med 1. Om det inte finns något projekt, börjar numret på "P-101".
    private static async Task<string> GenerateProjectNumberAsync(ProjectRepository projectRepository)
    {
        var latestProject = (await projectRepository.GetAsync())
        .OrderByDescending(p => p.ProjectNumber)
        .FirstOrDefault();

        int newProjectNumber = 101;

        if (latestProject != null)
        {
            var lastProjectNumber = latestProject.ProjectNumber;
            var numberPart = lastProjectNumber.Substring(2);
            if (int.TryParse(numberPart, out var lastNumber))
            {
                newProjectNumber = lastNumber + 1;
            }
        }

        return $"P-{newProjectNumber}";
    }


    public static Project? Create(ProjectEntity entity)
    {
        if (entity == null)
            return null;

        var customerName = entity.Customer?.CustomerName ?? "Unknown";

        var projectNumber = entity.ProjectNumber;

        return new Project
        {
            Id = entity.Id,
            Title = entity.Title,
            Description = entity.Description,
            StartDate = entity.StartDate,
            EndDate = entity.EndDate,
            CustomerId = entity.CustomerId,
            CustomerName = customerName,
            ProjectNumber = projectNumber
        };
    }


}


