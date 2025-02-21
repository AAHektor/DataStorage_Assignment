using Business.Models;
using Business.Factories;
using Data.Entities;
using Data.Repositories;

namespace Business.Services;

public class CustomerService(CustomerRepository customerRepository)
{
    private readonly CustomerRepository _customerRepository = customerRepository;

    public async Task<bool> CreateCustomerAsync(CustomerRegistrationForm form)
    {
        var customerEntity = CustomerFactory.Create(form);

        if (customerEntity != null)
        {
            await _customerRepository.AddAsync(customerEntity);
            return true;
        }
        return false;
    }

    public async Task<IEnumerable<Customer?>> GetCustomersAsync()
    {
        var customerEntities = await _customerRepository.GetAsync();
        return customerEntities.Select(CustomerFactory.Create);
    }

    public async Task<Customer?> GetCustomerByIdAsync(int id)
    {
        var customerEntity = await _customerRepository.GetAsync(x => x.Id == id);
        return CustomerFactory.Create(customerEntity!);
    }
    public async Task<Customer?> GetCustomerByCustomerNameAsync(string customerName)
    {
        var customerEntity = await _customerRepository.GetAsync(x => x.CustomerName == customerName);
        return CustomerFactory.Create(customerEntity!);
    }

    //chatgpt...
    public async Task<bool> UpdateCustomerAsync(Customer customer)
    {
        var existingCustomer = await _customerRepository.GetAsync(x => x.Id == customer.Id);
        if (existingCustomer == null)
            return false;

        existingCustomer.CustomerName = customer.CustomerName;

        await _customerRepository.UpdateAsync(existingCustomer);

        return true;
    }

    public async Task<bool> DeleteCustomerAsync(int id)
    {
        var customerEntity = await _customerRepository.GetAsync(x=>x.Id == id);
        if (customerEntity == null)
            return false;
        await _customerRepository.RemoveAsync(customerEntity);
        return true;
    }
}
