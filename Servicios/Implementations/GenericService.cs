using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using KO.Data.Interfaces;
using KO.Entities;
using KO.Services.Implementations;
using KO.Services.Interfaces;
using Microsoft.EntityFrameworkCore.Storage;


namespace KO.Services.Implementaciones
{
    public class GenericService : BaseService<IGenericData>, IGenericService
    {
        public GenericService(IGenericData datos) : base(datos) { }

        public void Add<T>(T p_Entity) where T : class
        {
            _datos.Add<T>(p_Entity);
        }

        public void AddAsync<T>(T p_Entity) where T : class
        {
            _datos.AddAsync<T>(p_Entity);
        }

        public void AddRange<T>(IEnumerable<T> p_Entity) where T : class
        {
            _datos.AddRange<T>(p_Entity);
        }

        public IDbContextTransaction BeginTransaction()
        {
            return _datos.BeginTransaction();
        }

        public void Delete<T>(T p_Entity) where T : class
        {
            _datos.Delete<T>(p_Entity);
        }

        public T Get<T>(Func<T, bool> predicate) where T : class
        {
            return _datos.Get<T>(predicate);
        }

        public T Get<T, W>(Func<T, bool> predicate, Expression<Func<T, W>> include)
            where T : class
            where W : class
        {
            return _datos.Get<T, W>(predicate, include);
        }

        public IEnumerable<T> GetAll<T>() where T : class
        {
            return _datos.GetAll<T>();
        }

        public IEnumerable<T> GetAll<T>(Func<T, bool> predicate) where T : class
        {
            return _datos.GetAll<T>(predicate);
        }

        public T GetById<T>(object id) where T : class
        {
            return _datos.GetById<T>(id);
        }

        public void Update<T>(T p_Entity) where T : class
        {
            _datos.Update(p_Entity);
        }

        public void Deactivate<T>(int id) where T : BaseEntity
        {
            _datos.Deactivate<T>(id);
        }

        public void Activate<T>(int id) where T : BaseEntity
        {
            _datos.Activate<T>(id);
        }

        public void UpdateIgnoringProperty<T, W>(T entity, Expression<Func<T, W>> property)
            where T : class
            where W : struct
        {
            _datos.UpdateIgnoringProperty<T, W>(entity, property);
        }

        public void UpdateRange<T>(IEnumerable<T> p_Entity) where T : class
        {
            _datos.UpdateRange<T>(p_Entity);
        }
    }


}
