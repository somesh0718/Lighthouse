using System;
using System.Linq;
using System.Linq.Expressions;

namespace Igmite.Lighthouse.DAL
{
    /// <summary>
    /// Data Access Interface of the Generic Repository
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IGenericRepository<T> where T : class
    {
        /// <summary>
        /// Add entity to the repository
        /// </summary>
        /// <param name="entity">the entity to add</param>
        /// <returns>The added entity</returns>
        void Add(T entity);

        /// <summary>
        /// Edit entity within the the repository
        /// </summary>
        /// <param name="entity">the entity to edit</param>
        /// <returns>The edit entity</returns>
        void Edit(T entity);

        /// <summary>
        /// Delete entity within the the repository
        /// </summary>
        /// <param name="entity">the entity to delete</param>
        /// <returns>The delete entity</returns>
        void Delete(T entity);

        /// <summary>
        /// Mark entity to be deleted within the repository
        /// </summary>
        /// <param name="entity">The entity to delete</param>
        T Save(T entity);

        /// <summary>
        /// Get all the element of this repository
        /// </summary>
        /// <returns></returns>
        IQueryable<T> GetAll();

        /// <summary>
        /// Get all the element of this repository by condition
        /// </summary>
        /// <returns></returns>
        IQueryable<T> FindBy(Expression<Func<T, bool>> predicate);

        /// <summary>
        /// Check entity already exists
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        bool Exists(Func<T, bool> predicate);
    }
}