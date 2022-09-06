using System;
using System.Linq;
using System.Linq.Expressions;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using KO.Data.EFScafolding;
using KO.Data.Interfaces;

namespace KO.Data.Implementacion
{
    public  class GenericData : BaseData, IGenericData
    {
        public GenericData(KOContext context) : base(context) { }
        public virtual T Get<T>(Func<T, bool> predicate) where T : class
        {
            return _context.Set<T>().FirstOrDefault(predicate);
        }
        public virtual T GetById<T>(object id) where T : class
        {
            return _context.Set<T>().Find(id);
        }

        public virtual T Get<T, W>(Func<T, bool> predicate, Expression<Func<T, W>> include) where T : class where W : class
        {
            return _context.Set<T>().Include(include).FirstOrDefault(predicate);
        }

        public virtual IEnumerable<T> GetAll<T>() where T : class
        {
            var result = _context.Set<T>().AsEnumerable<T>().ToList();
            return result;
        }

        public IEnumerable<T> GetAll<T>(Func<T, bool> predicate) where T : class
        {
            return _context.Set<T>().Where(predicate);
        }
        public virtual void Add<T>(T p_Entity) where T : class
        {
            _context.Set<T>().Add(p_Entity);
            _context.SaveChanges();
        }

        public virtual void Update<T>(T p_Entity) where T : class
        {
            _context.Set<T>().Update(p_Entity);
            _context.SaveChanges();
        }
        public virtual void Delete<T>(T p_Entity) where T : class
        {
            _context.Set<T>().Remove(p_Entity);
            _context.SaveChanges();
        }

        public async virtual void AddAsync<T>(T p_Entity) where T : class
        {
            await _context.Set<T>().AddAsync(p_Entity);
            await _context.SaveChangesAsync();
        }

        public virtual void AddRange<T>(IEnumerable<T> p_Entity) where T : class
        {
            _context.Set<T>().AddRange(p_Entity);
            _context.SaveChanges();
        }

        public virtual void UpdateIgnoringProperty<T, W>(T entity, Expression<Func<T, W>> property) where T : class where W : struct
        {
            _context.Entry(entity).State = EntityState.Modified;
            _context.Entry(entity).Property(property).IsModified = false;
            _context.SaveChanges();
        }

        public IDbContextTransaction BeginTransaction()
        {
            return _context.Database.BeginTransaction();
        }        

        public void UpdateRange<T>(IEnumerable<T> p_Entity) where T : class
        {
            _context.Set<T>().UpdateRange(p_Entity);
            _context.SaveChanges();
        }        
    }
}
