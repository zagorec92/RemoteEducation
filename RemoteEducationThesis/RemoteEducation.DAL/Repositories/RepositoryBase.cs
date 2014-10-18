using Education.Model;
using System;
using System.Data.Entity;
using System.Linq;

namespace Education.DAL.Repositories
{
    public abstract class RepositoryBase<T> where T : EntityBase
    {
        #region Properties

        /// <summary>
        /// Gets or sets the database context.
        /// </summary>
        protected EEducationDbContext Context { get; set; }

        #endregion

        #region Constructor

        /// <summary>
        /// Creates a new instance of the <see cref="Education.DAL.EEducationDbContext"/> class.
        /// </summary>
        /// <param name="context"></param>
        public RepositoryBase(EEducationDbContext context)
        {
            Context = context;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Gets all items from database.
        /// </summary>
        /// <typeparam name="T">Type derived from <see cref="Education.Model.EntityBase"/> class.</typeparam>
        /// <returns>The <see cref="System.Linq.IQueryable"/> collection containing instances of type T.</returns>
        public virtual IQueryable<T> GetAll()
        {
            return Context.Set<T>();
        }

        /// <summary>
        /// Gets the specified item from database.
        /// </summary>
        /// <param name="id">The <see cref="System.Int32"/> value.</param>
        /// <typeparam name="T">Type derived from <see cref="Education.Model.EntityBase"/> class.</typeparam>
        /// <returns>The instance of type T, if found.</returns>
        public virtual T Get(int id)
        {
            return Context.Set<T>().Find(id);
        }

        /// <summary>
        /// Inserts or updates item in database.
        /// </summary>
        /// <param name="entity">The instance of type T.</param>
        /// <typeparam name="T">Type derived from <see cref="Education.Model.EntityBase"/> class.</typeparam>
        /// <returns>True if the insert or update operation succeded, false otherwise.</returns>
        public virtual bool InsertOrUpdate(T entity)
        {
            try
            {
                if (entity.ID == default(int))
                {
                    Context.Set<T>().Add(entity);
                    entity.DateCreated = entity.DateModified = DateTime.Now;
                }
                else
                {
                    Context.Entry<T>(entity).State = EntityState.Modified;
                    entity.DateModified = DateTime.Now;
                }
            }
            catch
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Deletes specified item from database.
        /// </summary>
        /// <param name="id">The <see cref="System.Int32"/> value.</param>
        /// <returns>True if the delete operation succeded, false otherwise.</returns>
        public virtual bool Delete(int id)
        {
            try
            {
                T entity = Get(id);
                Context.Set<T>().Remove(entity);
            }
            catch
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Saves changes.
        /// </summary>
        public virtual void Save()
        {
            Context.SaveChanges();
        }

        #endregion
    }
}
