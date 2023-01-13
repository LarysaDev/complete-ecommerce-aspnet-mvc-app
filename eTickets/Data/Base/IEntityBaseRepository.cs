using eTickets.Models;
using System.Linq.Expressions;

namespace eTickets.Data.Base
{
	public interface IEntityBaseRepository<T> where T : class, IEntityBase, new()
	{
        Task<IEnumerable<T>> GetAllAsync();

        //params is a keyword which is used to specify a parameter, який приймає змінну кількість параметрів
        //
        Task<IEnumerable<T>> GetAllAsync(params Expression<Func<T, object>>[] includeParams);
        Task<T> GetByIdAsync(int id);
        Task AddAsync(T entity);
        Task UpdateAsync(int id, T entity);
        Task DeleteAsync(int id);
    }
}
