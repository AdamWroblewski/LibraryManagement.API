using FluentValidation;

namespace LibraryManagement.Application.Queries.BookLoans
{
    public class GetBookLoansQueryValidator : AbstractValidator<GetBookLoansQuery>
    {
        public GetBookLoansQueryValidator()
        {
            RuleFor(x => x.PageNumber).GreaterThan(0);
            RuleFor(x => x.PageSize).InclusiveBetween(1, 100);
        }
    }
}
