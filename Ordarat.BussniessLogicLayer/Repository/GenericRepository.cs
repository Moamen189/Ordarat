﻿using Microsoft.EntityFrameworkCore;
using Ordarat.BussniessLogicLayer.Interfaces;
using Ordarat.BussniessLogicLayer.Specification;
using Ordarat.DataAccessLayer;
using Ordarat.DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordarat.BussniessLogicLayer.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        private readonly StroreContext _context;

        public GenericRepository(StroreContext context)
        {
            _context = context;
        }
        public async Task<IReadOnlyList<T>> GetAllAsync()
                => await _context.Set<T>().ToListAsync();

        public async Task<IReadOnlyList<T>> GetAllWithSpecAsync(ISpecification<T> spec)
        
            => await ApplySpecifications(spec).ToListAsync();
        

        public async Task<T> GetAsync(int id)
            => await _context.Set<T>().FindAsync(id);

        public async Task<T> GetWithSpecAsync(ISpecification<T> spec)
                   => await ApplySpecifications(spec).FirstOrDefaultAsync();


        private IQueryable<T> ApplySpecifications(ISpecification<T> spec)
        {
            return SpecificationEvalutor<T>.GetQuery(_context.Set<T>(), spec);
        }

    }
}
