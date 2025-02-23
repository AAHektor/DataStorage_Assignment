namespace Presentation.ConsoleApp.Interfaces
{
    public interface IProjectMenu : IMenuDialogs
    {
        Task CreateProject();
        Task ListProjects();
    }
}
