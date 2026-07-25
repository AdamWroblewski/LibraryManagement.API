using AutoMapper;
using LibraryManagement.Domain.Interfaces;
using MediatR;

namespace LibraryManagement.Application.Commands.Books
{
    public class DeleteBookByIdCommandHandler : IRequestHandler<DeleteBookByIdCommand, bool>
    {
        private readonly IBookRepository _bookRepository;
        private readonly IMapper _mapper;

        public DeleteBookByIdCommandHandler(IBookRepository bookRepository, IMapper mapper)
        {
            _bookRepository = bookRepository;
            _mapper = mapper;
        }

        public async Task<bool> Handle(DeleteBookByIdCommand request, CancellationToken cancellationToken)
        {
            var book = await _bookRepository.GetByIdAsync(request.Id);

            if (book is null)
                return false;

            await _bookRepository.DeleteAsync(book);

            return true;
        }
    }
}
