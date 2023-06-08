using AntropoPollWebApi.Core.Contexts;
using AntropoPollWebApi.Core.Interfaces;
using AntropoPollWebApi.Core.Models;
using AntropoPollWebApi.Core.Settings;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace CremlinWebApi.TicketCore.Repository
{
    public class PollRepository<T> : IPollRepository<T> where T : BaseModel
    {
        protected IMapper _mapper;
        protected AntropoPollSettings _ticketOptions;
        public AntropoPollContext Context { get; set; }

        public PollRepository(IMapper mapper, IOptions<AntropoPollSettings> ticketOptions)
        {
            _mapper = mapper;
            _ticketOptions = ticketOptions.Value;
            Context = new AntropoPollContext(_ticketOptions.AntropoPollProviders);
        }


        public T Replace(T oldEntity, T newEntity)
        {
            Context.Remove(oldEntity);
            Context.Add(newEntity);
            Context.SaveChanges();
            return newEntity;
        }

        public void Delete(T entity)
        {
            Context.Remove(entity);
            Context.SaveChanges();
        }

        public T Get(Guid guid)
        {
            return Context.Set<T>().FirstOrDefault(x => x.Guid == guid);
        }

        public async Task<T> GetAsync(Guid guid)
        {
            return await Context.Set<T>().FirstOrDefaultAsync(x => x.Guid == guid);
        }

        public IQueryable<T> GetAll()
        {
            return Context.Set<T>();
        }

        public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression)
        {
            return Context.Set<T>().Where(expression);
        }

        public IEnumerable<T> Include(params Expression<Func<T, object>>[] includes)
        {
            DbSet<T> dbSet;
            IEnumerable<T> query = null;

            dbSet = Context.Set<T>();

            foreach (var include in includes)
            {
                query = dbSet.Include(include);
            }

            return query ?? dbSet;
        }

        public void RemoveRange(IQueryable<T> deleteRecords)
        {
            Context.RemoveRange(deleteRecords);
            Context.SaveChanges();
        }

        public void UpdateRange(IQueryable<T> updateRecords)
        {
            updateRecords.ToList().ForEach(c => { c.LastUpdate = DateTime.UtcNow; });
            Context.UpdateRange(updateRecords);
            Context.SaveChanges();
        }

        public T Insert(T entity)
        {
            entity.LastUpdate = DateTime.UtcNow;

            entity.Guid = Guid.NewGuid();

            Context.Add(entity);
            Context.SaveChanges();
            return entity;
        }

        public T Update(T entity)
        {
            entity.LastUpdate = DateTime.UtcNow;
            Context.Update(entity);
            Context.SaveChanges();
            return entity;
        }
    }
}
