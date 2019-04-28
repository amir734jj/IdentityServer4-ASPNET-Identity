using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Dal.Interfaces;
using Logic.Interfaces;
using Models.Interfaces;

namespace Logic.Abstracts
{
    public abstract class BasicCrudLogicAbstract<T> : IBasicCrudLogic<T> where T : IEntity
    {
        /// <summary>
        /// Returns instance of basic DAL
        /// </summary>
        /// <returns></returns>
        protected abstract IBasicCrudDal<T> GetBasicCrudDal();

        /// <summary>
        /// Call forwarding
        /// </summary>
        /// <returns></returns>
        public virtual async Task<IEnumerable<T>> GetAll() => await GetBasicCrudDal().GetAll();

        /// <summary>
        /// Call forwarding
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual async Task<T> Get(Guid id) => await GetBasicCrudDal().Get(id);

        /// <summary>
        /// Call forwarding
        /// </summary>
        /// <param name="instance"></param>
        /// <returns></returns>
        public virtual async Task<T> Save(T instance) => await GetBasicCrudDal().Save(instance);

        /// <summary>
        /// Call forwarding
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual async Task<T> Delete(Guid id) => await GetBasicCrudDal().Delete(id);

        /// <summary>
        /// Call forwarding
        /// </summary>
        /// <param name="id"></param>
        /// <param name="updatedInstance"></param>
        /// <returns></returns>
        public virtual async Task<T> Update(Guid id, T updatedInstance) => await GetBasicCrudDal().Update(id, updatedInstance);
        
        /// <summary>
        /// Call forwarding
        /// </summary>
        /// <param name="id"></param>
        /// <param name="modifyAction"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public virtual async Task<T> Update(Guid id, Action<T> modifyAction) => await GetBasicCrudDal().Update(id, modifyAction);
    }
}
