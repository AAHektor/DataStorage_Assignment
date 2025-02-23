using Business.Models;
using Business.Services;
using Data.Migrations;
using Presentation.ConsoleApp.Interfaces;

namespace Presentation.ConsoleApp.Options;

public class CustomerOptions
{

    private readonly CustomerService _customerService;
    private readonly IUserInterface _userInterface;

    public CustomerOptions(CustomerService customerService, IUserInterface userInterface)
    {
        _customerService = customerService;
        _userInterface = userInterface;
    }

    public async Task CreateNewCustomer()
    {
        Console.Clear();
        _userInterface.DisplayMessage("Registered Customers: ");
        var customers = await _customerService.GetCustomersAsync();
        _userInterface.DisplayCustomerList(customers);

        var name = _userInterface.GetValidCustomerName();
        if (name == null)
        {
            _userInterface.DisplayMessage("Operation cancelled.");
            return;
        }

        var customerForm = new CustomerRegistrationForm { CustomerName = name };
        var result = await _customerService.CreateCustomerAsync(customerForm);
        _userInterface.DisplayMessage(result ? "Customer created successfully!" : "Customer creation failed.");
        Console.ReadKey();
    }


    public async Task GetAllCustomers()
    {
        Console.Clear();
        var customers = await _customerService.GetCustomersAsync();
        _userInterface.DisplayCustomerList(customers);
        Console.ReadLine();
    }

    public async Task DeleteCustomer()
    {
        Console.Clear();
        var customers = await _customerService.GetCustomersAsync();
        _userInterface.DisplayCustomerList(customers);

        var customerId = _userInterface.GetValidCustomerId(customers);
        if (customerId == -1)
        {
            Console.WriteLine("Operation cancelled.");
            return;
        }

        var result = await _customerService.DeleteCustomerAsync(customerId);
        Console.WriteLine(result ? "Customer deleted successfully!" : "Customer not found or deletion failed.");
        Console.ReadLine();
    }

}
