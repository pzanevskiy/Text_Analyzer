using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Text_Analyzer.DAL.Context;

namespace Text_Analyzer.DAL.Repository
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        private TextAnalyzerContext _context;
        private DbSet<TEntity> _entitySet;

        public GenericRepository(TextAnalyzerContext context)
        {
            this._context = context;
            _entitySet = this._context.Set<TEntity>();
        }

        public IEnumerable<TEntity> Get()
        {
            return _entitySet.ToList();
        }

        public TEntity Get(Expression<Func<TEntity, bool>> predicate)
        {
            return _entitySet.FirstOrDefault(predicate);
        }

        public void Add(TEntity entity)
        {
            if (entity != null)
            {
                _entitySet.Add(entity);
            }
        }

        public void Delete(TEntity entity)
        {
            if (entity != null)
            {
                _entitySet.Remove(entity);
            }
        }

        public void Update(TEntity entity)
        {
            if (entity != null)
            {
                _context.Entry(entity).State = EntityState.Modified;
            }
        }
    }
}
