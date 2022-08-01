using Microsoft.EntityFrameworkCore;
using Ordarat.DataAccessLayer.Entities;
using System.Linq;

namespace Ordarat.BussniessLogicLayer.Specification

{
    public class SpecificationEvalutor<TEntity> where TEntity : BaseEntity
    {
        public static IQueryable<TEntity> GetQuery(IQueryable<TEntity> InputQuery, ISpecification<TEntity> spec)
        {
            var query = InputQuery;
            if (spec.Criteria != null)
                query = query.Where(spec.Criteria);

            query = spec.Includes.Aggregate(query, (curretQuery, include) => curretQuery.Include(include));
            return query;
        }
    }
}
