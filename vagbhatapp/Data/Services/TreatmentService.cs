﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using vagbhatapp.Data.Core;
using vagbhatapp.Data.Entities;

namespace vagbhatapp.Data.Services
{
    public interface ITreatmentService : IEntityRepository<Treatment>
    {
    }
    public class TreatmentService : ITreatmentService
    {
        private readonly IEntityRepository<Treatment> entityRepository;

        public TreatmentService(IEntityRepository<Treatment> entityRepository)
        {
            this.entityRepository = entityRepository;
        }
        public async Task<Treatment> AddAsync(Treatment entity)
        {
            return await entityRepository.AddAsync(entity);
        }

        public Task<int> CountAsync(Expression<Func<Treatment, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public Task<int> CountAsync()
        {
            throw new NotImplementedException();
        }

        public IQueryable<Treatment> Find(Expression<Func<Treatment, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Treatment> Get(int start, int length)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Treatment> Get()
        {
            throw new NotImplementedException();
        }

        public Task<Treatment> GetAsync(string key)
        {
            throw new NotImplementedException();
        }

        public Task<Treatment> RemoveAsync(Treatment entity)
        {
            throw new NotImplementedException();
        }

        public Task<Treatment> UpdateAsync(Treatment entity)
        {
            throw new NotImplementedException();
        }
    }
}
