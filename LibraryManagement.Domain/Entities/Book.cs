namespace LibraryManagement.Domain.Entities
{
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string ISBN { get; set; }
        public int PublicationYear { get; set; }
        public string Publisher { get; set; }
        public ICollection<BookLoan> Loans { get; set; } = new List<BookLoan>();

        public void UpdateDetails(string title, string author, string isbn, int publicationYear, string publisher)
        {
            Title = title;
            Author = author;
            ISBN = isbn;
            PublicationYear = publicationYear;
            Publisher = publisher;
        }
    }
}