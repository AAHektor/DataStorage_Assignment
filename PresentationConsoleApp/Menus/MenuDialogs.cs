using Business.Models;
using Business.Services;
using Presentation.ConsoleApp.Interfaces;
using System.Linq.Expressions;

namespace Presentation.ConsoleApp.Menus
{
    public class MenuDialogs
    {
        private readonly MenuDialogsCustomer _customerMenu;
        private readonly MenuDialogsProject _projectMenu;


        public MenuDialogs(MenuDialogsCustomer customerMenu, MenuDialogsProject projectMenu)
        {
            _customerMenu = customerMenu;
            _projectMenu = projectMenu;
        }

        public async Task MenuOptions()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Main Menu");
                Console.WriteLine("1. Manage Projects");
                Console.WriteLine("2. Manage Customers");
                Console.WriteLine("3. Exit");

                var option = Console.ReadLine();

                switch (option)
                {
                    case "1":
                        await _projectMenu.ShowMenu();
                        break;

                    case "2":
                        await _customerMenu.ShowMenu();
                        break;

                    case "3":
                        return;

                    default:
                        Console.WriteLine("Invalid choice, please try again");
                        break;
                }
            }
        }

    }
}
