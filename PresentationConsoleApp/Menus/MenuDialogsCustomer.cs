using Presentation.ConsoleApp.Interfaces;
using Presentation.ConsoleApp.Options;

namespace Presentation.ConsoleApp.Menus;

public class MenuDialogsCustomer : IMenuDialogs
{
    private readonly CustomerOptions _customerOptions;

    public MenuDialogsCustomer(CustomerOptions customerOptions)
    {
        _customerOptions = customerOptions;
    }

    public async Task ShowMenu()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("Manage Customers:");
            Console.WriteLine("1. Create Customer");
            Console.WriteLine("2. List Customers");
            Console.WriteLine("3. Delete Customer");
            Console.WriteLine("4. Return to Main Menu");
            Console.Write("Select an option: ");

            var option = Console.ReadLine();

            switch (option)
            {
                case "1":
                    await _customerOptions.CreateNewCustomer();
                    break;
                case "2":
                    await _customerOptions.GetAllCustomers();
                    break;
                case "3":
                    await _customerOptions.DeleteCustomer();
                    break;
                case "4":
                    return;
                default:
                    Console.WriteLine("Invalid choice, try again!");
                    break;
            }
        }
    }
}