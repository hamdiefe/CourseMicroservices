using Course.Web.Models.Discount;
using FluentValidation;

namespace Course.Web.Validator
{
    public class DiscountApplyInputValidator : AbstractValidator<DiscountApplyInput>
    {
        public DiscountApplyInputValidator()
        {
            RuleFor(x => x.Code)
                .NotEmpty()
                .WithMessage("indirim kupon alanı boş olamaz");
        }
    }
}
