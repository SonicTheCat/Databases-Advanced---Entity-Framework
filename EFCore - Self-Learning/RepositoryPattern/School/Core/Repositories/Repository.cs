using Microsoft.EntityFrameworkCore;

using SoftUni.Core.Repositories.Contracts;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace SoftUni.Core.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity>
        where TEntity : class
    {
        protected readonly DbContext context;
        private readonly DbSet<TEntity> entities;  
        
        public Repository(DbContext context)
        {
            this.context = context;
            this.entities = this.context.Set<TEntity>();
        }

        public void Add(TEntity value)
        {
            this.entities.Add(value); 
        }

        public void AddRange(IEnumerable<TEntity> values)
        {
            this.entities.AddRange(values); 
        }

        public IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate)
        {
            return this.entities.Where(predicate); 
        }

        public TEntity Get(int id)
        {
            return this.entities.Find(id); 
        }

        public IEnumerable<TEntity> GetAll()
        {
            return this.entities.ToList(); 
        }

        public void Remove(TEntity entity)
        {
            this.entities.Remove(entity);
        }

        public void RemoveRange(IEnumerable<TEntity> entities)
        {
            this.entities.RemoveRange(entities);
        }
    }
}