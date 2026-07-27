using AutoMapper;
using LibraryManagement.Domain.Entities;
using LibraryManagement.Domain.Interfaces;
using MediatR;

namespace LibraryManagement.Application.Commands.BookReviews
{
    public class CreateBookReviewCommandHandler : IRequestHandler<CreateBookReviewCommand, int>
    {
        private readonly IBookReviewRepository _bookReviewRepository;
        private readonly IMapper _mapper;

        public CreateBookReviewCommandHandler(IBookReviewRepository bookRepository, IMapper mapper)
        {
            _bookReviewRepository = bookRepository;
            _mapper = mapper;
        }

        public async Task<int> Handle(CreateBookReviewCommand request, CancellationToken cancellationToken)
        {
            var bookReview = new BookReview(request.BookId, request.ApplicationUserId, request.Rate, request.Comment);
            await _bookReviewRepository.AddAsync(bookReview);
            return bookReview.Id;
        }
    }
}