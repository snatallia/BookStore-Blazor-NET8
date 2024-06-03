using AutoMapper;
using AutoMapper.QueryableExtensions;
using BookStore.API.Data;
using BookStore.API.Models;
using BookStore.API.Models.Author;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookStore.API.Repositories
{
    public class AuthorsRepository : GenericRepository<Author>, IAuthorsRepository
    {
        private readonly BookStoreDbContext dbContext;
        private readonly IMapper mapper;

        public AuthorsRepository(BookStoreDbContext dbContext, IMapper mapper) : base(dbContext,mapper)
        {
            this.mapper = mapper;
            this.dbContext = dbContext;
        }

        public async Task<AuthorDetailsDto> GetAuthorDetailsAsync(int id)
        {
             return await dbContext.Authors
                    .Include(q => q.Books)
                    .ProjectTo<AuthorDetailsDto>(mapper.ConfigurationProvider)
                    .FirstOrDefaultAsync(q => q.Id == id);
        }
    }

}
