namespace LibraryManagement.Domain.Entities;

public class Book
{
    public int Id { get; private set; }
    public string Title { get; private set; } = string.Empty;
    public string Author { get; private set; } = string.Empty;
    public string ISBN { get; private set; } = string.Empty;
    public int PublicationYear { get; private set; }
    public string Publisher { get; private set; } = string.Empty;


    private readonly List<BookLoan> _loans = new();
    public IReadOnlyCollection<BookLoan> Loans => _loans.AsReadOnly();

    private readonly List<BookReview> _reviews = new();
    public IReadOnlyCollection<BookReview> Reviews => _reviews.AsReadOnly();


    private Book() { }

    public Book(string title, string author, string isbn, int publicationYear, string publisher)
    {
        Title = title;
        Author = author;
        ISBN = isbn;
        PublicationYear = publicationYear;
        Publisher = publisher;
    }

    public void UpdateDetails(string title, string author, string isbn, int publicationYear, string publisher)
    {
        Title = title;
        Author = author;
        ISBN = isbn;
        PublicationYear = publicationYear;
        Publisher = publisher;
    }
}