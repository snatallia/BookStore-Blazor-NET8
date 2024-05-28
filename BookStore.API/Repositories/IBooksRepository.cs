using BookStore.API.Data;

namespace BookStore.API.Repositories
{
    public interface IBooksRepository : IGenericRepository<Book>
    {
    }

    public class BooksRepository : GenericRepository<Book>, IBooksRepository
    {
        public BooksRepository(BookStoreDbContext dbContext) : base(dbContext)
        {
        }
    }
}
