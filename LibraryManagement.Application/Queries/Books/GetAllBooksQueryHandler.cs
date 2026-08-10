using AutoMapper;
using LibraryManagement.Application.DTOs;
using LibraryManagement.Domain.Interfaces;
using MediatR;

namespace LibraryManagement.Application.Queries.Books
{
    public class GetAllBooksQueryHandler : IRequestHandler<GetAllBooksQuery, List<BookListDto>>
    {
        private readonly IBookRepository _bookRepository;
        private readonly IMapper _mapper;

        public GetAllBooksQueryHandler(IBookRepository bookRepository, IMapper mapper)
        {
            _bookRepository = bookRepository;
            _mapper = mapper;
        }

        public async Task<List<BookListDto>> Handle(GetAllBooksQuery request, CancellationToken cancellationToken)
        {
            var books = await _bookRepository.GetAllAsync(cancellationToken);
            return _mapper.Map<List<BookListDto>>(books);
        }
    }
}