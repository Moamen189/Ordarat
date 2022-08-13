using Ordarat.BussniessLogicLayer.Specification;
using Ordarat.DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordarat.BussniessLogicLayer.Interfaces
{
    public interface IGenericRepository<T> where T : BaseEntity
    {
        Task<T> GetAsync(int id);

        Task<IReadOnlyList<T>> GetAllAsync();

        Task<T> GetWithSpecAsync(ISpecification<T> spec);

        Task<IReadOnlyList<T>> GetAllWithSpecAsync(ISpecification<T> spec);

        Task<int> GetCountAsync(ISpecification<T> spec);

        Task Add(T Entity);
        void Update(T Entity);

        void Delete(T Entity);


    }
}
