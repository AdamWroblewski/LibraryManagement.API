using LibraryManagement.Application.CustomExceptions;
using LibraryManagement.Domain.Interfaces;
using MediatR;

namespace LibraryManagement.Application.Commands.BookReviews
{
    public class DeleteBookReviewCommandHandler : IRequestHandler<DeleteBookReviewCommand, Unit>
    {
        private readonly IBookReviewRepository _bookReviewRepository;

        public DeleteBookReviewCommandHandler(IBookReviewRepository bookRepository)
        {
            _bookReviewRepository = bookRepository;
        }

        public async Task<Unit> Handle(DeleteBookReviewCommand request, CancellationToken cancellationToken)
        {
            var review = await _bookReviewRepository.GetByBookIdAndUserId(request.BookId, request.UserId, cancellationToken);

            if (review == null)
                throw new EntityNotFoundException("Book review");

            await _bookReviewRepository.DeleteAsync(review, cancellationToken);

            return Unit.Value;
        }
    }
}