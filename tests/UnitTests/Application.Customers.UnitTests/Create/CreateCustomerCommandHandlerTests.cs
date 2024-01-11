using Application.Customers.Create;
using Domain.Customers;
using Domain.Primitives;

namespace Application.Customers.UnitTests.Create;

public class CreateCustomerCommandHandlerTests
{
    //QUE_VAMOS_A_PROBAR
    //EL_ESCENARIO
    //LO_QUE_DEBE_ARROJAR

    private readonly Mock<ICustomerRepository> _mockCustomerRepository;

    private readonly Mock<IUnitOfWork> _mockUnitOfWork;

    private readonly CreateCustomerCommandHandler _handler;

    public CreateCustomerCommandHandlerTests()
    {
        _mockCustomerRepository = new Mock<ICustomerRepository>();
        _mockUnitOfWork = new Mock<IUnitOfWork>();
        _handler = new CreateCustomerCommandHandler(_mockUnitOfWork.Object, _mockCustomerRepository.Object);
    }

    [Fact]
    public async Task HandleCreateCustomer_WhenPhoneNumberHasInvalidFormat_ShouldReturnValidationError()
    {
        //Arrange
        CreateCustomerCommand command = new CreateCustomerCommand(
            "Miguel",
            "Fandiño",
            "miguelfandino@email.com",
            "12312312312",
            "123",
            "line1",
            "line2",
            "city",
            "state",
            "zipcode");

        //Act
        var result = await _handler.Handle(command, default);

        //Assert
        result.IsError.Should().BeTrue();
        result.FirstError.Type.Should().Be(ErrorType.Validation);
        result.FirstError.Code.Should().Be(CustomerErrors.PhoneNumberBadFormat.Code);
        result.FirstError.Description.Should().Be(CustomerErrors.PhoneNumberBadFormat.Description);
    }
}