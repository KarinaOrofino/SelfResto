using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using KO.Datos.EFScafolding;
using KO.Datos.Interfaces;
using KO.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;


namespace KO.Datos.Implementacion
{
    public class DatosGenerico : DatosBase, IDatosGenerico
    {
        public DatosGenerico(KOContext context) : base(context) { }
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
            if (p_Entity is EntidadBase)
            {
                var entity = p_Entity as EntidadBase;
                entity.FechaAlta = DateTime.Now;                           
            }

            _context.Set<T>().Add(p_Entity);
            _context.SaveChanges();
        }

        public virtual void Update<T>(T p_Entity, bool saveChanges = true) where T : class
        {
            if (p_Entity is EntidadBase)
            {
                var entity = p_Entity as EntidadBase;
                entity.FechaModificacion = DateTime.Now;
            }

            _context.Set<T>().Update(p_Entity);
            _context.SaveChanges();
        }

        public virtual void UpdateCleaningTracker<T>(T p_Entity, bool saveChanges = true) where T : class
        {
            if (p_Entity is EntidadBase)
            {
                var entity = p_Entity as EntidadBase;
                entity.FechaModificacion = DateTime.Now;
            }

            // Tracker agregado para que no salte un error de tracking del mismo id al intentar actualizar, duplico update para no afectar a otros usos del servicio
            _context.ChangeTracker.Clear();

            _context.Set<T>().Update(p_Entity);
            _context.SaveChanges();
        }
        public virtual void Delete<T>(T p_Entity) where T : class
        {
            _context.Set<T>().Remove(p_Entity);
            _context.SaveChanges();
        }

        public void Deactivate<T>(T p_Entity) where T : EntidadBase
        {            
            p_Entity.Activo = false;
            p_Entity.FechaModificacion = DateTime.Now;
            Update<T>(p_Entity);
        }

        public void Activate<T>(T p_Entity) where T : EntidadBase
        {
            p_Entity.Activo = true;
            p_Entity.FechaModificacion = DateTime.Now;
            Update<T>(p_Entity);
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
        public virtual void UpdateIgnoringPropertyList<T, W>(T entity, List<Expression<Func<T, W>>> propertys) where T : class where W : struct
        {
            _context.Entry(entity).State = EntityState.Modified;
            propertys.ForEach(prop =>
                {
                    _context.Entry(entity).Property(prop).IsModified = false;
                }
                );

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

        public void DetachLocal<T>(T t, int id) where T : class
        {
            var local = GetById<T>(id);
            if (local != null)
            {
                _context.Entry(local).State = EntityState.Detached;
            }
            _context.Entry(t).State = EntityState.Modified;
        }

    }
}
