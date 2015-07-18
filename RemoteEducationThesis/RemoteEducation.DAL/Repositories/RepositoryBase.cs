using Education.Model.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace Education.DAL.Repositories
{
	public abstract class RepositoryBase<T> 
        where T : EntityBase
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
        /// <param name="context">The<see cref="Education.DAL.EEducationDbContext"/> instance.</param>
        public RepositoryBase(EEducationDbContext context)
        {
            Context = context;
        }

        #endregion

        #region Methods

        #region GetAll

        /// <summary>
        /// Gets all items from database.
        /// </summary>
        /// <typeparam name="T">T is <see cref="Education.Model.EntityBase"/>.</typeparam>
        /// <returns>The <see cref="System.Linq.IQueryable{T}"/> collection.</returns>
        public virtual IQueryable<T> GetAll()
        {
            return Context.Set<T>();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="include"></param>
        /// <returns></returns>
        public virtual IQueryable<T> GetAll(Expression<Func<T, object>> include)
        {
            return GetAll().Include(include);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async virtual Task<List<T>> GetAllAsync()
        {
            return await GetAll().ToListAsync();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="include"></param>
        /// <returns></returns>
        public async virtual Task<List<T>> GetAllAsync(Expression<Func<T, object>> include)
        {
            return await GetAll().Include(include).ToListAsync();
        }

        #endregion

        #region Get

        /// <summary>
        /// Gets the specified item from database.
        /// </summary>
        /// <param name="id">The <see cref="System.Int32"/> value.</param>
        /// <typeparam name="T">T is <see cref="Education.Model.EntityBase"/>.</typeparam>
        /// <returns>T</returns>
        public virtual T Get(int id)
        {
            return Context.Set<T>().Find(id);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public virtual T Get(Predicate<T> predicate)
        {
            return Context.Set<T>().FirstOrDefault(x => predicate(x));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async virtual Task<T> GetAsync(int id)
        {
            return await Context.Set<T>().FindAsync(id);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public async virtual Task<T> GetAsync(Predicate<T> predicate)
        {
            return await Context.Set<T>().FirstOrDefaultAsync(x => predicate(x));
        }

        #endregion

        #region InsertOrUpdate

        /// <summary>
        /// Inserts or updates item in database.
        /// </summary>
        /// <param name="entity">The instance of type T.</param>
        /// <typeparam name="T">T is <see cref="Education.Model.EntityBase"/>.</typeparam>
        /// <returns>True if the insert or update operation succeded, false otherwise.</returns>
        public virtual bool InsertOrUpdate(T entity)
        {
            try
            {
                if (entity.ID == default(int))
                {
                    SetModifiedDate(entity);
                    Context.Set<T>().Add(entity);
                }
                else
                {
                    Context.Entry<T>(entity).State = EntityState.Modified;
                    SetModifiedDate(entity, true);
                }
            }
            catch
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        private void SetModifiedDate(T entity, bool overwrite = false)
        {
            if (!entity.DateModified.HasValue || overwrite)
                entity.DateModified = DateTime.Now;
        }

        #endregion

        #region Delete

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

        #endregion

        #region Save

        /// <summary>
        /// Saves changes.
        /// </summary>
        public virtual void Save()
        {
            Context.SaveChanges();
        }

        /// <summary>
        /// Saves changes.
        /// </summary>
        public virtual int SaveWithCount()
        {
            return Context.SaveChanges();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async virtual Task SaveAsync()
        {
            await Context.SaveChangesAsync();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async virtual Task<int> SaveWithCountAsync()
        {
            return await Context.SaveChangesAsync();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async virtual Task SaveAsync(CancellationToken cancellationToken)
        {
            await Context.SaveChangesAsync(cancellationToken);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async virtual Task<int> SaveWithCountAsync(CancellationToken cancellationToken)
        {
            return await Context.SaveChangesAsync(cancellationToken);
        }

        #endregion

        #endregion
    }
}
