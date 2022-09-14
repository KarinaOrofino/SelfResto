using KO.Entities;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;


namespace KO.Services.Interfaces
{
    public interface IGenericService : IBaseService
    {
        T Get<T>(Func<T, bool> predicate) where T : class;
        T GetById<T>(object id) where T : class;
        T Get<T, W>(Func<T, bool> predicate, Expression<Func<T, W>> include) where T : class where W : class;
        IEnumerable<T> GetAll<T>() where T : class;
        IEnumerable<T> GetAll<T>(Func<T, bool> predicate) where T : class;
        void Add<T>(T p_Entity) where T : class;
        void AddRange<T>(IEnumerable<T> p_Entity) where T : class;
        void AddAsync<T>(T p_Entity) where T : class;
        void Update<T>(T p_Entity) where T : class;
        void Deactivate<T>(int id) where T : BaseEntity;
        void Activate<T>(int id) where T : BaseEntity;
        void UpdateRange<T>(IEnumerable<T> p_Entity) where T : class;
        void Delete<T>(T p_Entity) where T : class;
        void UpdateIgnoringProperty<T, W>(T entity, Expression<Func<T, W>> property) where T : class where W : struct;        
        IDbContextTransaction BeginTransaction();
    }
}
