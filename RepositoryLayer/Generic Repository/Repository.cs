// DomainLayer.DTOs.Paging;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;


namespace RepositoryLayer.RepositoryPattern
{
    public class Repository<T> : IRepository<T> where T : class
    {

        private readonly ApplicationDbContext _context;
        private DbSet<T> _entity;
        public Repository(ApplicationDbContext applicationDbContext)
        {

            _context = applicationDbContext;
            _entity = _context.Set<T>();
        }

        public async Task<IQueryable<T>> FindAllAsync()
        {
            return _entity.AsNoTracking();
        }
        public async Task<T> FindByIdAsync(int? Id)
        {
            var post = await _entity.FindAsync(Id);
            return post;
        }
        // when no need for loading additional entities 
        public async Task<IQueryable<T>> FindByConditionAsync(Expression<Func<T, bool>> expression)
        {
            return _entity.Where(expression).AsNoTracking();
        }
        public async Task<IQueryable<T>> FindByConditionTrackingAsync(Expression<Func<T, bool>> expression)
        {
            return _entity.Where(expression);
        }
        // when need for loading additional entities , so will use the eager loading 
        // With Eager Loading , to use it in my general case to use find by expression
        // Need to add another paramater and it will be expression so 
        public async Task<IQueryable<T>> FindByConditionAsync(Expression<Func<T, bool>> expression,
                                                           Func<IQueryable<T>, IQueryable<T>> includes = null)
        {
            var query = _entity.Where(expression);

            if (includes != null)
            {
                query = includes(query);
            }

            return query.AsNoTracking();

        }
        //public async Task<IQueryable<T>> FindByConditionAsync(Expression<Func<T, bool>> expression,
        //                                                      PagingRequest paging,
        //                                                      Func<IQueryable<T>, IQueryable<T>> includes = null)
        //{
        //    var query = _entity.Where(expression).Skip((paging.PageNumber - 1) * paging.RowCount).Take(paging.RowCount);

        //    if (includes != null)
        //    {
        //        query = includes(query);
        //    }

        //    return query.AsNoTracking();

        //}
        public async Task InsertAync(T entity)
        {

            await _entity.AddAsync(entity);

        }
        public async Task UpdateAsync(T entity)
        {
            _entity.Update(entity);

        }
        public async Task DeleteAsync(T entity)
        {
            _entity.Remove(entity);

        }
        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public async Task<int> CountAsync(Expression<Func<T, bool>> expression)
        {
            int count = _entity.Where(expression).Count();
            return count;

        }
        public async Task<int> CountAsync()
        {
            int count = _entity.Count();
            return count;

        }
    }
}
