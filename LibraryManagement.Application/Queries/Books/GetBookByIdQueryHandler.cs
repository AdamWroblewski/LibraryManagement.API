using AutoMapper;
using LibraryManagement.Application.CustomExceptions;
using LibraryManagement.Application.DTOs;
using LibraryManagement.Domain.Interfaces;
using MediatR;

namespace LibraryManagement.Application.Queries.Books
{
    internal class GetBookByIdQueryHandler : IRequestHandler<GetBookByIdQuery, BookDto>
    {
        private readonly IBookRepository _repository;
        private readonly IMapper _mapper;

        public GetBookByIdQueryHandler(IBookRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<BookDto> Handle(GetBookByIdQuery request, CancellationToken cancellationToken)
        {
            var book = await _repository.GetByIdAsync(request.Id);
            if (book == null)
                throw new EntityNotFoundException("Book");

            return _mapper.Map<BookDto>(book);
        }
    }
}
