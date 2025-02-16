using Business.Models;
using Data.Entities;

namespace Business.Factories;

public static class CustomerFactory
{
    public static CustomerEntity? Create(CustomerRegistrationForm form) => form == null ? null : new()
    {
        CustomerName = form.CustomerName,
    };

    public static Customer? Create(CustomerEntity entity) => entity == null ? null : new()
    {
        Id = entity.Id,
        CustomerName = entity.CustomerName,
    };

    //chatgpt
    public static CustomerEntity? Create(Customer customer, CustomerEntity existingEntity)
    {
        if (customer == null || existingEntity == null)
                return null;

        existingEntity.CustomerName = customer.CustomerName;
        return existingEntity;
    }

}
