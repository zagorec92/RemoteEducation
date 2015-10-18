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
	public class Repository<T>
		where T : Entity
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
		public Repository(EEducationDbContext context)
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
		public IQueryable<T> GetAll()
		{
			return Context.Set<T>();
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="include"></param>
		/// <returns></returns>
		public IQueryable<T> GetAll(Expression<Func<T, object>> include)
		{
			return GetAll().Include(include);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="include1"></param>
		/// <param name="include2"></param>
		/// <returns></returns>
		public IQueryable<T> GetAll(Expression<Func<T, object>> include1, Expression<Func<T, object>> include2)
		{
			return GetAll()
				.Include(include1)
				.Include(include2);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="include1"></param>
		/// <param name="include2"></param>
		/// <param name="include3"></param>
		/// <returns></returns>
		public IQueryable<T> GetAll(Expression<Func<T, object>> include1, Expression<Func<T, object>> include2, Expression<Func<T, object>> include3)
		{
			return GetAll()
				.Include(include1)
				.Include(include2)
				.Include(include3);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		public async Task<List<T>> GetAllAsync()
		{
			return await GetAll().ToListAsync();
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="include"></param>
		/// <returns></returns>
		public async Task<List<T>> GetAllAsync(Expression<Func<T, object>> include)
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
		public T Get(int id)
		{
			return Context.Set<T>().Find(id);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="predicate"></param>
		/// <returns></returns>
		public T Get(Predicate<T> predicate)
		{
			return Context.Set<T>().FirstOrDefault(x => predicate(x));
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		public async Task<T> GetAsync(int id)
		{
			return await Context.Set<T>().FindAsync(id);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="predicate"></param>
		/// <returns></returns>
		public async Task<T> GetAsync(Predicate<T> predicate)
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
		public bool InsertOrUpdate(T entity)
		{
			bool retVal = true;

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
				retVal = false;
			}

			return retVal;
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
		public bool Delete(int id)
		{
			bool retVal = true;

			try
			{
				T entity = Get(id);
				Context.Set<T>().Remove(entity);
			}
			catch
			{
				retVal = false;
			}

			return retVal;
		}

		#endregion

		#region Save

		/// <summary>
		/// Saves changes.
		/// </summary>
		public void Save()
		{
			Context.SaveChanges();
		}

		/// <summary>
		/// Saves changes.
		/// </summary>
		public int SaveWithCount()
		{
			return Context.SaveChanges();
		}

		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		public async Task SaveAsync()
		{
			await Context.SaveChangesAsync();
		}

		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		public async Task<int> SaveWithCountAsync()
		{
			return await Context.SaveChangesAsync();
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="cancellationToken"></param>
		/// <returns></returns>
		public async Task SaveAsync(CancellationToken cancellationToken)
		{
			await Context.SaveChangesAsync(cancellationToken);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="cancellationToken"></param>
		/// <returns></returns>
		public async Task<int> SaveWithCountAsync(CancellationToken cancellationToken)
		{
			return await Context.SaveChangesAsync(cancellationToken);
		}

		#endregion

		#endregion
	}
}
