using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.EntityFrameworkCore;
using Dal.Interfaces;
using Microsoft.EntityFrameworkCore;
using Models.Models;

namespace Dal.Dals
{
    public class QuestionDal : IQuestionDal
    {   
        private readonly EntityDbContext _dbContext;

        private readonly IMapper _mapper;
 
        private readonly DbSet<Question> _dbSet;

        public QuestionDal(EntityDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _dbSet = dbContext.Questions;
            _mapper = mapper;
        }

        /// <summary>
        /// Returns all enities
        /// </summary>
        /// <returns></returns>
        public virtual async Task<IEnumerable<Question>> GetAll()
        {
            // TODO: get all entities
            // NOTE: make sure to use DbSetInclude() method or eager load properties
            throw new NotImplementedException();
        }

        /// <summary>
        /// Returns an entity given the id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual async Task<Question> Get(Guid id)
        {
            // TODO: get entity with Id = id
            // NOTE: make sure to use DbSetInclude() method or eager load properties
            throw new NotImplementedException();
        }

        /// <summary>
        /// Saves an instance
        /// </summary>
        /// <param name="instance"></param>
        /// <returns></returns>
        public virtual async Task<Question> Save(Question instance)
        {
            // TODO: save entity and return the saved entity
            // hint you need to use both the dbset and the context to save
            throw new NotImplementedException();
        }

        /// <summary>
        /// Deletes enitity given the id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual async Task<Question> Delete(Guid id)
        {
            // TODO: find the entity to be deleted by id
            // TODO: delete entity and SaveChanges
            throw new NotImplementedException();
        }

        /// <summary>
        /// Handles update, two approaches:
        ///     1) Manual updating entity from DTO
        ///     2) Using AutoMapper
        /// </summary>
        /// <param name="id"></param>
        /// <param name="dto"></param>
        /// <returns></returns>
        public virtual async Task<Question> Update(Guid id, Question dto)
        {
            if (dto != null)
            {
                // Approach #1
                // var entity = await Get(id);
                // ManualUpdate(entity, dto);
                
                // Approach #2
                _dbSet.Persist(_mapper).InsertOrUpdate(dto);

                // Save and dispose
                await _dbContext.SaveChangesAsync();

                // Returns the updated entity
                return dto;
            }

            // Not found
            return null;
        }

        /// <summary>
        ///     Handles manual update
        /// </summary>
        /// <param name="id"></param>
        /// <param name="modifyAction"></param>
        /// <returns></returns>
        public virtual async Task<Question> Update(Guid id, Action<Question> modifyAction)
        {
            var entity = await Get(id);
                
            if (entity != null)
            {
                // Update
                modifyAction(entity);

                await _dbContext.SaveChangesAsync();

                return entity;
            }

            // Not found
            return null;
        }

        /// <summary>
        ///     Eager load Tags and Answers properties
        /// </summary>
        /// <returns></returns>
        private IQueryable<Question> DbSetInclude()
        {
            return _dbSet
                .Include(x => x.Tags)
                .Include(x => x.Answers);
        }

        /// <summary>
        ///     Manual updating of Entity from DTO (data transfer object)
        ///     Generally this approach is not recommended, using AutoMapper is much simpler and less prone to error
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="dto"></param>
        private void ManualUpdate(Question entity, Question dto)
        {
            // 1) Update flat properties
            _dbContext.Entry(entity).CurrentValues.SetValues(dto);

            var tagChanges = new List<Tag>();
                
            // 2) Update tags one-by-one
            dto.Tags.ForEach(x =>
            {
                var existingTag = entity.Tags.FirstOrDefault(y => y.Id == x.Id);

                // New tag was added to dto.Tags; hence add it to entity.Tags
                if (existingTag == null)
                {
                    tagChanges.Add(x);
                    entity.Tags.Add(x);
                }
                // Update the tag
                else
                {
                    tagChanges.Add(existingTag);

                    // Update the properties of Tag one-by-one 
                    _dbContext.Entry(existingTag).CurrentValues.SetValues(x);
                }
            });
                
            // 3) Remove the one that are in entity.Tags but not in dto.Tags 
            entity.Tags.Except(tagChanges).ToList().ForEach(x =>
            {
                entity.Tags.Remove(x);
            });

            var answerChanges = new List<Answer>();
                
            // 4) Update answers one-by-one
            dto.Answers.ForEach(x =>
            {
                var existing = entity.Answers.FirstOrDefault(y => y.Id == x.Id);
                
                // New answer was added to dto.Answers; hence add it to entity.Answers
                if (existing == null)
                {
                    answerChanges.Add(x);
                    entity.Answers.Add(x);
                }
                // Update the Answer
                else
                {
                    answerChanges.Add(existing);
                    
                    // Update the properties of Answer one-by-one 
                    _dbContext.Entry(existing).CurrentValues.SetValues(x);
                }
            });

            // 5) Remove the one that are in entity.Answers but not in dto.Answers 
            entity.Answers.Except(answerChanges).ToList().ForEach(x =>
            {
                entity.Answers.Remove(x);
            }); 
        }
    }
}