using Data.Repositories;
using Business.Models;
using Business.Factories;
using Microsoft.EntityFrameworkCore;


namespace Business.Services;

public class ProjectService
{
    private readonly ProjectRepository _projectRepository;

    public ProjectService(ProjectRepository projectRepository)
    {
        _projectRepository = projectRepository;
    }

    public async Task<bool> CreateProjectAsync(ProjectRegistrationForm form)
    {
        var projectEntity = await ProjectFactory.CreateAsync(form, _projectRepository);
        if (projectEntity != null)
        {
            Console.WriteLine($"Created Project with Number: {projectEntity.ProjectNumber}");
            await _projectRepository.AddAsync(projectEntity);
            return true;
        }
        return false;
    }



    public async Task<IEnumerable<Project?>> GetProjectsAsync()
    {
        var projectEntities = await _projectRepository.GetAsync(
        include: query => query.Include(p => p.Customer)
        );

        var projects = projectEntities.Select(ProjectFactory.Create).ToList();
        return projects;
    }

    public async Task<Project?> GetProjectByIdAsync(int id)
    {
        var projectEntity = await _projectRepository.GetAsync(x => x.Id == id);
        return ProjectFactory.Create(projectEntity!);
    }

    public async Task<bool> UpdateProjectAsync(Project project)
    {
        var existingProject = await _projectRepository.GetAsync(x => x.Id == project.Id);
        if (existingProject == null)
            return false;

        existingProject.Title = project.Title;
        existingProject.Description = project.Description;

        await _projectRepository.UpdateAsync(existingProject);
        return true;
    }

    public async Task<bool> DeleteProjectAsync(int id)
    {
        var projectEntity = await _projectRepository.GetAsync(x => x.Id == id);
        if (projectEntity == null)
            return false;

        await _projectRepository.RemoveAsync(projectEntity);
        return true;
    }
}
