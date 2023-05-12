using System;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Linq.Expressions;

namespace Igmite.Lighthouse.DAL.EF
{
    /// <summary>
    /// Data Access logic of the Generic Repository
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class GenericRepository<T> : IGenericRepository<T>
     where T : class
    {
        private IgmiteDbContext _entities;

        /// <summary>
        /// Initializes the Generic Repository class.
        /// </summary>
        public GenericRepository()
        {
            _entities = ContextFactory.CreateContext();
        }

        /// <summary>
        /// Get instance of Database Context object
        /// </summary>
        public IgmiteDbContext Context
        {
            get { return _entities; }
            set { _entities = value; }
        }

        /// <summary>
        /// Add entity to the repository
        /// </summary>
        /// <param name="entity">the entity to add</param>
        /// <returns>The added entity</returns>
        public virtual void Add(T entity)
        {
            Context.Set<T>().Add(entity);
        }

        /// <summary>
        /// Edit entity within the the repository
        /// </summary>
        /// <param name="entity">the entity to edit</param>
        /// <returns>The edit entity</returns>
        public virtual void Edit(T entity)
        {
            Context.Set<T>().Attach(entity);
            Context.Entry(entity).State = EntityState.Modified;
        }

        /// <summary>
        /// Delete entity within the the repository
        /// </summary>
        /// <param name="entity">the entity to delete</param>
        /// <returns>The delete entity</returns>
        public virtual void Delete(T entity)
        {
            Context.Set<T>().Remove(entity);
            Context.Entry(entity).State = EntityState.Deleted;
        }

        /// <summary>
        /// Mark entity to be deleted within the repository
        /// </summary>
        /// <param name="entity">The entity to delete</param>
        public virtual T Save(T entity)
        {
            try
            {
                Context.Set<T>().Add(entity);
                Context.SaveChanges();

                return entity;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Get all the element of this repository
        /// </summary>
        /// <returns></returns>
        public virtual IQueryable<T> GetAll()
        {
            IQueryable<T> query = Context.Set<T>();
            return query;
        }

        /// <summary>
        /// Get all the element of this repository by condition
        /// </summary>
        /// <returns></returns>
        public IQueryable<T> FindBy(Expression<Func<T, bool>> predicate)
        {
            IQueryable<T> query = _entities.Set<T>().Where(predicate);
            return query;
        }

        /// <summary>
        /// Check entity already exists
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public bool Exists(Func<T, bool> predicate)
        {
            T singleItem = _entities.Set<T>().FirstOrDefault(predicate);

            return singleItem != null;
        }
    }
}