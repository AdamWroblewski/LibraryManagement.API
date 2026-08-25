using FluentValidation;

namespace LibraryManagement.Application.Queries.Books
{
    public class GetPagedBooksQueryValidator : AbstractValidator<GetPagedBooksQuery>
    {
        public GetPagedBooksQueryValidator()
        {
            RuleFor(x => x.PageNumber).GreaterThan(0);
            RuleFor(x => x.PageSize).InclusiveBetween(1, 100);
        }
    }
}
