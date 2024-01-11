using Domain.Customers;
using Domain.Primitives;
using Domain.ValueObjects;
using Domain.Errors;
using ErrorOr;
using MediatR;

namespace Application.Customers.Create;

public sealed class CreateCustomerCommandHandler : IRequestHandler<CreateCustomerCommand, ErrorOr<Unit>>
{

    private readonly ICustomerRepository _customerRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateCustomerCommandHandler(
      IUnitOfWork unitOfWork,
      ICustomerRepository customerRepository
    )
    {
        _unitOfWork = unitOfWork
          ?? throw new ArgumentNullException(nameof(unitOfWork));
        _customerRepository = customerRepository
          ?? throw new ArgumentNullException(nameof(customerRepository));
    }


    public async Task<ErrorOr<Unit>> Handle(
      CreateCustomerCommand command,
      CancellationToken cancellationToken
    )
    {

        if (PhoneNumber.Create(command.PhoneNumber) is not PhoneNumber phoneNumber)
            // throw new ArgumentException(nameof(phoneNumber));
            return CustomerErrors.PhoneNumberBadFormat;

        if (
          Address.Create(
            command.Country,
            command.Line1,
            command.Line2,
            command.City,
            command.State,
            command.ZipCode
          ) is not Address address
        )
        {
            // throw new ArgumentException(nameof(address));
            return CustomerErrors.AddressBadFormat;
        }

        Customer customer = new(
          new CustomerId(Guid.NewGuid()),
          command.Name,
          command.LastName,
          command.Email,
          phoneNumber,
          address,
          true
        );

        await _customerRepository.Add(customer);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Unit.Value;

    }

}
