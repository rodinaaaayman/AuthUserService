using FluentValidation;

public class CreateClientCommandValidator
    : AbstractValidator<CreateClientCommand>
{
    public CreateClientCommandValidator()
    {
        RuleFor(x => x.Username)
            .NotEmpty()
            .MaximumLength(50);

        RuleFor(x => x.FirstName)
            .NotEmpty()
            .MaximumLength(50);

        RuleFor(x => x.LastName)
            .NotEmpty()
            .MaximumLength(50);

        RuleFor(x => x.Email)
            .NotEmpty()
            .EmailAddress()
            .MaximumLength(200);

        RuleFor(x => x.Password)
            .NotEmpty();

        RuleFor(x => x.NationalId)
            .NotEmpty()
            .Length(14);

        RuleFor(x => x.PhoneNumber)
            .MaximumLength(15);

        RuleFor(x => x.Deposit)
            .GreaterThanOrEqualTo(0);
    }
}