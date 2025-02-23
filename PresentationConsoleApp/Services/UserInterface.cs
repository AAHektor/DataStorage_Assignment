using Business.Models;
using Presentation.ConsoleApp.Interfaces;

namespace Presentation.ConsoleApp.Services;

public class UserInterface : IUserInterface
{
    public void DisplayProjects(IEnumerable<Project> projects)
    {
        foreach (var project in projects)
        {
            Console.WriteLine($"ID: {project.Id} | Title: {project.Title} | Start: {project.StartDate} | End: {project.EndDate}");
        }
    }

    public void DisplayMessage(string message)
    {
        Console.WriteLine(message);
    }

    public int GetValidProjectId(string prompt, IEnumerable<Project> projects)
    {
        while (true)
        {
            var input = GetUserInput(prompt);
            if (input?.ToUpper() == "Q") return -1;

            if (int.TryParse(input, out var id) && projects.Any(p => p.Id == id))
            {
                return id;
            }
            else
            {
                DisplayError();
            }
        }
    }

    public void DisplayError()
    {
        DisplayMessage("\nInvalid input! Please enter a valid ID.");
    }

    public string GetProjectTitle() => GetUserInput("Enter project title (or press 'Q' to cancel): ");
    public string GetProjectDescription() => GetUserInput("Enter project description (optional, press 'Q' to cancel): ");
    public DateTime GetStartDate() => GetDate("Enter project start date (yyyy-MM-dd, or 'Q' to cancel): ");
    public DateTime GetEndDate(DateTime startDate) => GetDate("Enter project end date (yyyy-MM-dd, or 'Q' to cancel): ", startDate);

    public string GetUserInput(string prompt)
    {
        Console.Write(prompt);
        return Console.ReadLine();
    }

    private DateTime GetDate(string prompt, DateTime? minDate = null)
    {
        DateTime date;
        while (true)
        {
            Console.Write(prompt);
            if (DateTime.TryParse(Console.ReadLine(), out date) && (!minDate.HasValue || date >= minDate))
            {
                return date;
            }
            else
            {
                Console.WriteLine("Invalid date.");
            }
        }
    }

    public int SelectCustomer(List<Customer> customerList)
    {
        Console.Clear();
        Console.WriteLine("Select a customer by ID (or press 'Q' to cancel):");

        foreach (var customer in customerList)
        {
            Console.WriteLine($"ID: {customer.Id}, Name: {customer.CustomerName}");
        }

        while (true)
        {
            Console.Write("Enter customer ID: ");
            var input = Console.ReadLine();

            if (input?.ToUpper() == "Q")
                return -1;

            if (int.TryParse(input, out int customerId))
            {
                var customer = customerList.FirstOrDefault(c => c.Id == customerId);
                if (customer != null)
                {
                    return customerId;
                }
                else
                {
                    DisplayError();
                }
            }
            else
            {
                DisplayError();
                Console.WriteLine("(or press 'Q' to cancel):");
            }
        }
    }

    public void DisplayCustomerList(IEnumerable<Customer> customers)
    {
        foreach (var customer in customers)
        {
            Console.WriteLine($"ID: {customer.Id}, Name: {customer.CustomerName}");
        }
    }

    public string GetValidCustomerName()
    {
        while (true)
        {
            var name = GetUserInput("Enter customer name (or press Q to cancel): ");
            if (name.ToUpper() == "Q")
            {
                return null;
            }

            if (!string.IsNullOrWhiteSpace(name) && name.Length >= 2)
            {
                return name;
            }

            DisplayMessage("Invalid input. Name must be at least 2 characters long.");
        }
    }

    public int GetValidCustomerId(IEnumerable<Customer> customers)
    {
        while (true)
        {
            Console.Write("Enter customer ID to delete (or press Q to cancel): ");
            var input = Console.ReadLine();

            if (input?.ToUpper() == "Q")
            {
                return -1;
            }

            if (int.TryParse(input, out int customerId) && customers.Any(c => c.Id == customerId))
            {
                return customerId;
            }

            DisplayError();
        }
    }



}
