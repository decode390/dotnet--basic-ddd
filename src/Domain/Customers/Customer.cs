using Domain.Primitives;
using Domain.ValueObjects;

namespace Domain.Customers;

public sealed class Customer : AggregateRoot
{
    #pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private Customer() { }
    #pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

    public Customer(
    CustomerId id,
    string name,
    string lastName,
    string email,
    PhoneNumber phoneNumber,
    Address address,
    bool active
  )
    {
        Id = id;
        Name = name;
        LastName = lastName;
        Email = email;
        PhoneNumber = phoneNumber;
        Address = address;
        Active = active;
    }


    public CustomerId Id { get; private set; }
    public string Name { get; private set; }
    public string LastName { get; private set; }
    public string FullName => $"{Name} {LastName}";
    public string Email { get; private set; }
    public PhoneNumber PhoneNumber { get; private set; }
    public Address Address { get; private set; }
    public bool Active { get; private set; }
}