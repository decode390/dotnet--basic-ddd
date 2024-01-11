using ErrorOr;

namespace Domain.Errors;

public static partial class CustomerErrors
{
    public static Error PhoneNumberBadFormat => Error.Validation(
            "Customer.PhoneNumber",
            "Phone number has not valid format."
        );

    public static Error AddressBadFormat => Error.Validation(
        "Customer.Address",
        "Address is not valid."
    );
}