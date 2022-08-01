using Ordarat.DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Ordarat.BussniessLogicLayer.Specification
{
    public class BaseSpecification<T> : ISpecification<T> where T : BaseEntity
    {
        public Expression<Func<T, bool>> Criteria { get; set; }
        public List<Expression<Func<T, object>>> Includes { get; set; } = new List<Expression<Func<T, object>>>();
        public BaseSpecification()
        {

        }
        public BaseSpecification(Expression<Func<T, bool>> Criteria)
        {
            this.Criteria = Criteria;
        }

        public void AddInclude(Expression<Func<T, object>> include)
        {
            Includes.Add(include);
        }
    }
}
