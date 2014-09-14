using Education.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RemoteEducation.DAL.Repositories
{
    public abstract class RepositoryBase<T> where T : EntityBase
    {

        protected EEducationDbContext Context { get; set; }

        public RepositoryBase(EEducationDbContext context)
        {
            Context = context;
        }

        public virtual IQueryable<T> GetAll()
        {
            return Context.Set<T>();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual T Get(int id)
        {
            return Context.Set<T>().Find(id);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
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
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
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
    }
}
