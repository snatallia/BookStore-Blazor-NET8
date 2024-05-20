using BookStore.API.Models.Book;

namespace BookStore.API.Models.Author
{
    public class AuthorDetailsDto: AuthorReadOnlyDto
    {
        public List<BookReadOnlyDto> Books{ get; set; }
    }
}
