using AutoMapper;
using AutoMapper.QueryableExtensions;
using BookStore.API.Data;
using BookStore.API.Models;
using Microsoft.EntityFrameworkCore;

namespace BookStore.API.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly BookStoreDbContext dbContext;
        private readonly IMapper mapper;

        public GenericRepository(BookStoreDbContext dbContext, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }

        public async Task<T> AddAsync(T entity)
        {
            await dbContext.AddAsync<T>(entity);
            await dbContext.SaveChangesAsync();
            return entity;
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await GetAsync(id);
            dbContext.Set<T>().Remove(entity);
            await dbContext.SaveChangesAsync();
        }

        public async Task<List<T>> GetAllAsync()
        {
            return await dbContext.Set<T>().ToListAsync();
        }

        public async Task<T> GetAsync(int id)
        {
            if (id == 0) 
                return null;
            else
                return await dbContext.Set<T>().FindAsync(id);
        }

        public async Task UpdateAsync(T entity)
        {
            dbContext.Update<T>(entity);
            await dbContext.SaveChangesAsync();
        }

        public async Task<bool> Exits(int id)
        {
            var entity = await GetAsync(id);
            return entity != null;
        }

        public async Task<VirtualizeResponse<TResult>> GetAllAsync<TResult>(QueryParameters queryParam) where TResult : class
        {
            var totalSize = await dbContext.Set<T>().CountAsync();
            var items = await dbContext.Set<T>()
                .Skip(queryParam.StartIndex)
                .Take(queryParam.PageSize)
                    .ProjectTo<TResult>(mapper.ConfigurationProvider)
                .ToListAsync();

            return new VirtualizeResponse<TResult> { Items = items, TotalSize = totalSize };
        }
    }
}
