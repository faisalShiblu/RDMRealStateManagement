using FluentValidation;

namespace RealStateMVCWebApp.Models
{
    public class User
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;

        public string Password { get; set; } = string.Empty;
        public string ConfirmPassword { get; set; } = string.Empty;
    }

    public class UserValidator : AbstractValidator<User>
    {
        public UserValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required.");
            RuleFor(x => x.Email).NotEmpty().WithMessage("Email is required.")
                                 .EmailAddress().WithMessage("Email is not valid");
            RuleFor(x => x.PhoneNumber).NotEmpty().WithMessage("Phone Number is required.")
                                     .Matches(@"^\+?(88)?0?1[3456789][0-9]{8}\b").WithMessage("Invalid phone number format.");
            RuleFor(x => x.Password).NotEmpty().WithMessage("Password is required.");
            RuleFor(x => x.ConfirmPassword).NotEmpty().WithMessage("Confirm Password is required.")
                            .Equal(x => x.Password).WithMessage("Password Don't Match");

        }
    }
}
