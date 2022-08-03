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

        public Expression<Func<T, object>> OrderBy { get; set; }
        public Expression<Func<T, object>> OrderByDesending { get; set; }

        public int Take { get; set; }

        public int Skip { get; set; }

        public bool IsPagingEnabled{ get; set; }



    }
}
