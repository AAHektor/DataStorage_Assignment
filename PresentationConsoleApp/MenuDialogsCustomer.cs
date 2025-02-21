using Business.Models;
using Business.Services;
using Presentation.ConsoleApp;

namespace Presentation.ConsoleApp;

public class MenuDialogsCustomer : IMenuDialogs
{
    private readonly CustomerService _customerService;

    public MenuDialogsCustomer(CustomerService customerService)
    {
        _customerService = customerService;
    }

    public async Task ShowMenu()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("Manage Customers:");
            Console.WriteLine("1. Create Customer");
            Console.WriteLine("2. List Customers");
            Console.WriteLine("3. Return to Main Menu");
            Console.Write("Select an option: ");

            var option = Console.ReadLine();

            switch (option)
            {
                case "1":
                    await CreateNewCustomer();
                    break;
                case "2":
                    await GetAllCustomers();
                    break;
                case "3":
                    return;
                default:
                    Console.WriteLine("Invalid choice, try again!");
                    break;
            }
        }
    }

    public async Task CreateNewCustomer()
    {
        Console.Write("Enter customer name: ");
        var name = Console.ReadLine();

        var customerForm = new CustomerRegistrationForm { CustomerName = name };
        var result = await _customerService.CreateCustomerAsync(customerForm);
        Console.WriteLine(result ? "Customer created successfully!" : "Customer creation failed.");
        Console.ReadLine();
    }

    public async Task GetAllCustomers()
    {
        var customers = await _customerService.GetCustomersAsync();
        foreach (var customer in customers)
        {
            Console.WriteLine($"ID: {customer.Id}, Name: {customer.CustomerName}");
        }
        Console.ReadLine();
    }
}