using AutoMapper;
using AutoMapper.QueryableExtensions;
using BookStore.API.Data;
using BookStore.API.Models.Author;
using Microsoft.EntityFrameworkCore;

namespace BookStore.API.Repositories
{
    public class AuthorsRepository : GenericRepository<Author>, IAuthorsRepository
    {
        private readonly BookStoreDbContext dbContext;
        private readonly Mapper mapper;

        public AuthorsRepository(BookStoreDbContext dbContext, Mapper mapper) : base(dbContext)
        {
            this.mapper = mapper;
            this.dbContext = dbContext;
        }

        public async Task<AuthorDetailsDto> GetAuthorDetailsAsync(int id)
        {
             var author = await dbContext.Authors
                    .Include(q => q.Books)
                    .ProjectTo<AuthorDetailsDto>(mapper.ConfigurationProvider)
                    .FirstOrDefaultAsync(q => q.Id == id);

            return author;
        }
    }
}
