using FluentValidation;

namespace LibraryManagement.Application.Queries.Books
{
    public class GetBookDetailsQueryValidator : AbstractValidator<GetBookDetailsQuery>
    {
        public GetBookDetailsQueryValidator()
        {
            RuleFor(x => x.PageNumber).GreaterThan(0);
            RuleFor(x => x.PageSize).InclusiveBetween(1, 100);
        }
    }
}