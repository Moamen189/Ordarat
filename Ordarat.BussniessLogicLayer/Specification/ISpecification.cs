using Ordarat.DataAccessLayer.Entities;
using System;
using System.Collections.Generic;

using System.Linq.Expressions;

namespace Ordarat.BussniessLogicLayer.Specification

{
    public interface ISpecification<T> where T : BaseEntity
    {

        public Expression<Func<T, bool>> Criteria { get; set; }

        public List<Expression<Func<T, object>>> Includes { get; set; }

    }
}
