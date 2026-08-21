using LibraryManagement.Application.CustomExceptions;
using LibraryManagement.Domain.Entities;
using LibraryManagement.Domain.Interfaces;
using MediatR;

namespace LibraryManagement.Application.Commands.BookReviews
{
    public class CreateBookReviewCommandHandler : IRequestHandler<CreateBookReviewCommand, int>
    {
        private readonly IBookReviewRepository _bookReviewRepository;

        public CreateBookReviewCommandHandler(IBookReviewRepository bookRepository)
        {
            _bookReviewRepository = bookRepository;
        }

        public async Task<int> Handle(CreateBookReviewCommand request, CancellationToken cancellationToken)
        {
            var hasReview = await _bookReviewRepository.HasReviewAsync(request.BookId, request.UserId, cancellationToken);
            if (hasReview)
            {
                throw new DuplicateResourceException("You have already added a review of this book. You can edit or remove it.");
            }

            var bookReview = new BookReview(request.BookId, request.UserId, request.Rate, request.Comment);
            await _bookReviewRepository.AddAsync(bookReview, cancellationToken);

            return bookReview.Id;
        }
    }
}