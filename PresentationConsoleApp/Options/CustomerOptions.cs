using Business.Models;
using Business.Services;

namespace Presentation.ConsoleApp.Options;

public class CustomerOptions
{

    private readonly CustomerService _customerService;

    public CustomerOptions(CustomerService customerService)
    {
        _customerService = customerService;
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
