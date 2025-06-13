using core.Models;
using core.Spacification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace core.Repository
{
    public interface IGenaricrepostry<T> where T : BaseClass
    {
        Task<T> GetByIdAsync(int id);
        Task<IReadOnlyList<T>> GetAllAsync();
        Task<IReadOnlyList<T>> getallwithspacAsync(Ispacfiaction<T> spac);
        Task<T> getbyidwithspacAsync(Ispacfiaction<T> spac);
        Task<T> set(T entity);
        Task<T> UpdateAsync(T entity);
        Task DeleteAsync(T entity);
        Task<ICollection<T>> GetBooksByIdsAsync(ICollection<int> bookIds);
   

    }
}
