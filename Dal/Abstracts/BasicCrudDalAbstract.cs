using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.EntityFrameworkCore;
using Dal.DbContext;
using Dal.Interfaces;
using Microsoft.EntityFrameworkCore;
using Models.Interfaces;

namespace DAL.Abstracts
{
    public abstract class BasicCrudDalAbstract<T> : IBasicCrudDal<T> where T : class, IEntity
    {
        /// <summary>
        /// Abstract to get IMapper
        /// </summary>
        /// <returns></returns>
        protected abstract IMapper GetMapper();
        
        /// <summary>
        /// Abstract to get database context
        /// </summary>
        /// <returns></returns>
        protected abstract IEntityContext GetDbContext();
        
        /// <summary>
        /// Abstract to get entity set
        /// </summary>
        /// <returns></returns>
        protected abstract DbSet<T> GetDbSet();

        /// <summary>
        /// Returns all enities
        /// </summary>
        /// <returns></returns>
        public virtual async Task<IEnumerable<T>> GetAll()
        {
            return await GetDbSet().ToListAsync();
        }

        /// <summary>
        /// Returns an entity given the id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual async Task<T> Get(Guid id)
        {
            return await DbSetInclude().FirstOrDefaultAsync(x => x.Id == id);
        }

        /// <summary>
        /// Saves an instance
        /// </summary>
        /// <param name="instance"></param>
        /// <returns></returns>
        public virtual async Task<T> Save(T instance)
        {
            GetDbSet().Add(instance);
            
            await GetDbContext().SaveChangesAsync();
                        
            return instance;
        }

        /// <summary>
        /// Deletes enitity given the id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual async Task<T> Delete(Guid id)
        {
            var instance = await Get(id);

            if (instance != null)
            {
                GetDbSet().Remove(instance);

                await GetDbContext().SaveChangesAsync();

                return instance;
            }

            return null;
        }

        /// <summary>
        /// Handles update
        /// </summary>
        /// <param name="id"></param>
        /// <param name="instance"></param>
        /// <returns></returns>
        public virtual async Task<T> Update(Guid id, T instance)
        {                
            if (instance != null)
            {
                GetDbSet().Persist(GetMapper()).InsertOrUpdate(instance);

                // Save and dispose
                await GetDbContext().SaveChangesAsync();

                // Returns the updated entity
                return instance;
            }

            // Not found
            return null;
        }

        /// <summary>
        /// Handles manual update
        /// </summary>
        /// <param name="id"></param>
        /// <param name="modifyAction"></param>
        /// <returns></returns>
        public virtual async Task<T> Update(Guid id, Action<T> modifyAction)
        {
            var entity = await Get(id);
                
            if (entity != null)
            {
                // Update
                modifyAction(entity);

                await GetDbContext().SaveChangesAsync();

                return entity;
            }

            // Not found
            return null;
        }

        /// <summary>
        /// Applies the eager loading of specified fields
        /// </summary>
        /// <returns></returns>
        protected virtual IQueryable<T> DbSetInclude()
        {
            return GetDbSet();
        }
    }
}