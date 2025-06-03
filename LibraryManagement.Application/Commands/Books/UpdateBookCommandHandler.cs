using AutoMapper;
using LibraryManagement.Domain.Interfaces;
using MediatR;

namespace LibraryManagement.Application.Commands.Books
{
    public class UpdateBookCommandHandler : IRequestHandler<UpdateBookCommand, Unit>
    {
        private readonly IBookRepository _bookRepository;
        private readonly IMapper _mapper;

        public UpdateBookCommandHandler(IBookRepository bookRepository, IMapper mapper)
        {
            _bookRepository = bookRepository;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(UpdateBookCommand request, CancellationToken cancellationToken)
        {
            var book = await _bookRepository.GetByIdAsync(request.Id);
            if (book == null)
                throw new KeyNotFoundException("Book not found.");

            book.UpdateDetails(request.Title, request.Author, request.ISBN, request.PublicationYear, request.Publisher);
            await _bookRepository.UpdateAsync(book);

            return Unit.Value;
        }
    }
}
