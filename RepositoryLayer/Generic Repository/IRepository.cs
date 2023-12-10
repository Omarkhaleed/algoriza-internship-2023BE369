//using DomainLayer.DTOs.Paging;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.RepositoryPattern
{
        public interface IRepository<T> 
        {
        Task<IQueryable<T>> FindAllAsync();
        Task<T> FindByIdAsync(int? Id);
        Task<IQueryable<T>> FindByConditionAsync(Expression<Func<T, bool>> expression);
        Task<IQueryable<T>> FindByConditionTrackingAsync(Expression<Func<T, bool>> expression);
        Task<IQueryable<T>> FindByConditionAsync(Expression<Func<T, bool>> expression, Func<IQueryable<T>, IQueryable<T>> includes);
        //Task<IQueryable<T>> FindByConditionAsync(Expression<Func<T, bool>> expression, PagingRequest paging, Func<IQueryable<T>, IQueryable<T>> includes);
        Task<int> CountAsync();
        Task<int> CountAsync(Expression<Func<T, bool>> expression);
        Task InsertAync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(T entity);
      
        //void Insert(T entity);
        void SaveChanges();
    }
    
}
