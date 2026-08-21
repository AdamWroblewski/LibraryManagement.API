using LibraryManagement.Application.CustomExceptions;
using LibraryManagement.Domain.Interfaces;
using MediatR;

namespace LibraryManagement.Application.Commands.BookReviews
{
    public class UpdateBookReviewCommandHandler : IRequestHandler<UpdateBookReviewCommand, Unit>
    {
        private readonly IBookReviewRepository _bookReviewRepository;

        public UpdateBookReviewCommandHandler(IBookReviewRepository bookReviewRepository)
        {
            _bookReviewRepository = bookReviewRepository;
        }

        public async Task<Unit> Handle(UpdateBookReviewCommand request, CancellationToken cancellationToken)
        {
            var review = await _bookReviewRepository.GetByBookIdAndUserId(request.BookId, request.UserId, cancellationToken);
            if (review == null) 
            {
                throw new EntityNotFoundException("Book review");
            }

            review.UpdateDetails(request.Comment, request.Rate);
            await _bookReviewRepository.UpdateAsync(review, cancellationToken);

            return Unit.Value;
        }
    }
}
