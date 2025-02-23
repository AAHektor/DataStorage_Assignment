namespace Presentation.ConsoleApp.Interfaces
{
    public interface ICustomerMenu : IMenuDialogs
    {
        Task CreateCustomer();
        Task ListCustomers();
    }
}
