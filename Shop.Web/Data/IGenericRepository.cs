namespace Shop.Web.Data
{
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public interface IGenericRepository<T> where T : class
    {
        IQueryable<T> GetAll();

        IEnumerable GetList();

        Task<T> GetByIdAsync(int id);

        Task CreateAsync(T entity);

        Task UpdateAsync(T entity);

        Task DeleteAsync(T entity);

        Task<bool> ExistAsync(int id);
    }


}
