using Ordarat.BussniessLogicLayer.Interfaces;
using Ordarat.DataAccessLayer;
using Ordarat.DataAccessLayer.Entities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordarat.BussniessLogicLayer.Repository
{

    
    public class UnitOfWork : IUnitOfWork
    {
        private readonly StroreContext _context;
        private Hashtable _repostories;
        public UnitOfWork(StroreContext context)
        {
            _context = context;
        }
        public async Task<int> Complete()
        {
           return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public IGenericRepository<TEntity> Repository<TEntity>() where TEntity : BaseEntity
        {
            if(_repostories == null)
                _repostories = new Hashtable(); 

            var type = typeof(TEntity).Name;

            if (!_repostories.ContainsKey(type))
            {
                var repository = new GenericRepository<TEntity>(_context);
                _repostories.Add(type, repository);
            }
            return(IGenericRepository<TEntity>) _repostories[type];
        }
    }
}
