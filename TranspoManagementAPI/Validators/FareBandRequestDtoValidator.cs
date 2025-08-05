using FluentValidation;
using TranspoManagementAPI.DTO;

namespace TranspoManagementAPI.Validators
{
    public class FareBandRequestDtoValidator : AbstractValidator<FareBandRequestDto>
    {
        public FareBandRequestDtoValidator()
        {
            RuleFor(x => x.DistanceLimit)
                .GreaterThanOrEqualTo(0).WithMessage("Distance limit must be non-negative");

            RuleFor(x => x.RatePerMile)
                .NotEmpty().WithMessage("Rate per mile is required")
                .GreaterThan(0).WithMessage("Rate per mile must be positive");
        }
    }
}
