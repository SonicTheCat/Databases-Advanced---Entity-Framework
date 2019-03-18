namespace NonStopMarket.App.Core.Repositories
{
    using System.Collections.Generic;
    using System.Linq;

    using Microsoft.EntityFrameworkCore;
    using AutoMapper;

    using Contracts;
    
    public class Repository<TEntity> : IRepository<TEntity>
         where TEntity : class
    {
        protected readonly DbContext context;
        protected readonly IMapper mapper; 
        private readonly DbSet<TEntity> entities;

        public Repository(DbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper; 
            this.entities = this.context.Set<TEntity>(); 
        }

        public void Add(TEntity entity)
        {
            this.entities.Add(entity); 
        }

        public void AddRange(IEnumerable<TEntity> entities)
        {
            this.entities.AddRange(entities); 
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