using EStore.Core.AppContexts;
using EStore.Core.Models;
using EStore.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EStore.Repositories.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : BaseEntity
    {
        protected readonly AppDbContext _context;

        public BaseRepository(AppDbContext context)
        {
            this._context = context;
        }
        public async Task<bool> AddAsync(T entity)
        {
            var result = _context.Set<T>().Add(entity);
            if (result.State == EntityState.Added)
            {
                return true;
            }
            return false;
        }

        //public async Task<bool> DeleteByIdAsync(int id)
        //{
        //    var entity = _context.Set<T>().FirstOrDefault(e => e.Id == id);
        //    if (entity is null)
        //    {
        //        throw new NullReferenceException();
        //    }
        //    var result = _context.Set<T>().Remove(entity);
        //    if (result.State == EntityState.Deleted)
        //    {
        //        return true;
        //    }
        //    return false;
        //}

        public async Task<IEnumerable<T>> GetAllAsync(int PageNumber,int PageSize)
        {
            const int MaxmumPageSize = 20;
            PageSize=Math.Min(PageSize, MaxmumPageSize);

            var result = _context.Set<T>()
                        .Skip((PageNumber - 1) * PageSize)
                        .Take(PageSize)
                        .ToList();
            return result;
        }

        //public async Task<T> GetByIdAsync(int id)
        //{
        //    var result = _context.Set<T>().FirstOrDefault(e => e.Id == id);
        //    return result;
        //}

        //public async Task<bool> UpdateAsync(int id, T entity)
        //{
        //    var CurrentEntity = _context.Set<T>().FirstOrDefault(x => x.Id == id);
        //    if (CurrentEntity is null)
        //    {
        //        return false;
        //    }
        //    _context.Entry(CurrentEntity).CurrentValues.SetValues(entity);
        //    return true;
        //}
    }
}
