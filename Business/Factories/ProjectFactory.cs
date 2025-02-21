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

        // Generera projektnummer asynkront
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

    //
    private static async Task<string> GenerateProjectNumberAsync(ProjectRepository projectRepository)
    {
        var latestProject = (await projectRepository.GetAsync()).OrderByDescending(p => p.Id).FirstOrDefault();
        int newId = latestProject != null ? latestProject.Id + 1 : 101;  // Om inget tidigare projekt finns, börja med 101
        return $"P-{newId}";
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


