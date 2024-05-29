using AutoMapper;
using AutoMapper.QueryableExtensions;
using BookStore.API.Data;
using BookStore.API.Models.Book;
using Microsoft.EntityFrameworkCore;

namespace BookStore.API.Repositories
{
    public class BooksRepository : GenericRepository<Book>, IBooksRepository
    {
        private readonly BookStoreDbContext dbContext;
        private readonly IMapper mapper;

        public BooksRepository(BookStoreDbContext dbContext, IMapper mapper) : base(dbContext)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }

        public async Task<List<BookReadOnlyDto>> GetAllBooksAsync()
        {
            return await dbContext.Books
                            .Include(b => b.Author)
                            .ProjectTo<BookReadOnlyDto>(mapper.ConfigurationProvider)
                            .ToListAsync();
        }

        public async Task<BookDetailsDto> GetBookAsync(int id)
        {
            var book = await dbContext.Books
                .Include(q => q.Author)
                .ProjectTo<BookDetailsDto>(mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(q => q.Id == id);

            return book;
        }
    }
}
