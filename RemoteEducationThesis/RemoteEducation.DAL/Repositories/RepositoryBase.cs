using RemoteEducation.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RemoteEducation.DAL.Repositories
{
    public abstract class RepositoryBase<T> where T : EntityBase
    {
        /// <summary>
        /// 
        /// </summary>
        protected RemoteEducationDbContext Context { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public RepositoryBase(RemoteEducationDbContext context)
        {
            Context = context;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
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
    }
}
