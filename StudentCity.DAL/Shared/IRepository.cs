using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace StudentCity.DAL.Shared
{
    /// <summary>
    /// this interface is the parent for the Repository pattern 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IRepository<T> where T : class
    {
        #region [CURD Operations Methods signature]

        /// <summary>
        /// Gets a collection of all objects in the database
        /// </summary>
        /// <remarks>Synchronous</remarks>
        /// <returns>An IQueryable of every object in the database</returns>
        IQueryable<T> GetAll();
        /// <summary>
        /// Return specific count of rows 
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        List<T> GetAll(int pageIndex, int pageSize);

        /// <summary>
        /// Get Entities that match the critaria 
        /// </summary>
        /// <param name="exp"></param>
        /// <returns></returns>
        Task<object> GetBy(Expression<Func<T, bool>> exp);

        T InsertOrUpdate(Expression<Func<T, bool>> exp, dynamic entity);

        /// <summary>
        /// Returns a single object with a primary key of the provided id
        /// </summary>
        /// <remarks>Synchronous</remarks>
        /// <param name="iD">The primary key of the object to fetch</param>
        /// <returns>A single object with the provided primary key or null</returns>
        T GetById(Int32 iD);
        /// <summary>
        /// Inserts a single object to the database and commits the change
        /// </summary>
        /// <remarks>Synchronous</remarks>
        /// <param name="insertEntity">The object to insert</param>
        /// <returns>The resulting object including its primary key after the insert</returns>
        T Add(T insertEntity);
        /// <summary>
        /// Inserts a collection of objects into the database and commits the changes
        /// </summary>
        /// <remarks>Synchronous</remarks>
        /// <param name="insertListEntity">An IEnumerable list of objects to insert</param>
        /// <returns>The IEnumerable resulting list of inserted objects including the primary keys</returns>
        IEnumerable<T> AddAll(IEnumerable<T> insertListEntity);

        /// <summary>
        /// Updates a single object based on the provided primary key and commits the change
        /// </summary>
        /// <param name="updateEntity">The updateEntity object to apply to the database</param>
        /// <returns>The resulting updateEntity object</returns>
        bool Update(T updateEntity);


       
        bool Delete(int id);
        bool DeleteObj(int id);

        // Check if entity exists or not by entity id
        bool Exists(Guid entityId);

        // Check if entity exists or not by expression
        bool Exists(Expression<Func<T, bool>> exp);


        #endregion


    }
}