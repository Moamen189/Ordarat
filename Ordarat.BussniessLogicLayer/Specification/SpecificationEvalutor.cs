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
            if (spec.OrderBy != null)
                query = query.OrderBy(spec.OrderBy);
            if (spec.OrderByDesending != null)
                query = query.OrderByDescending(spec.OrderByDesending);

            //if (spec.IsPagingEnabled)
            //    query = query.Skip(spec.Skip).Take(spec.Take);

            query = spec.Includes.Aggregate(query, (curretQuery, include) => curretQuery.Include(include));
            return query;
        }
    }
}
