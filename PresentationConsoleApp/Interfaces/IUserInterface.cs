using Business.Models;
namespace Presentation.ConsoleApp.Interfaces;

public interface IUserInterface
{
    void DisplayProjects(IEnumerable<Project> projects);
    void DisplayMessage(string message);
    int GetValidProjectId(string prompt, IEnumerable<Project> projects);
    void DisplayError();
    string GetProjectTitle();
    string GetProjectDescription();
    DateTime GetStartDate();
    DateTime GetEndDate(DateTime startDate);
    int SelectCustomer(List<Customer> customerList);
    string GetUserInput(string prompt);
    void DisplayCustomerList(IEnumerable<Customer> customers);
    string GetValidCustomerName();
    int GetValidCustomerId(IEnumerable<Customer> customers);




}
