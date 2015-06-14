using Education.Model;
using System;
using System.Linq;

namespace Education.DAL.Repositories
{
    public class ETableRepository<T>
        where T : ETableBase
    {
        #region Properties

        protected EEducationDbContext Context { get; set; }

        #endregion

        #region Constructor

        /// <summary>
        /// Creates a new instance of the <see cref="Education.DAL.Repositories.ETableRepository"/> class.
        /// </summary>
        /// <param name="context">The <see cref="Education.DAL.EEducationDbContext"/> instance.</param>
        public ETableRepository(EEducationDbContext context)
        {
            Context = context;
        }

        #endregion

        #region Methods

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IQueryable<T> GetAll()
        {
            return Context.Set<T>();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
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

        #endregion
    }
}
