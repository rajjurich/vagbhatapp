using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using vagbhatapp.Data.Core;
using vagbhatapp.Data.Entities;

namespace vagbhatapp.Data.Services
{
    public interface IPatientService : IEntityRepository<Patient>
    {
    }
    public class PatientService : IPatientService
    {
        private readonly IEntityRepository<Patient> entityRepository;

        public PatientService(IEntityRepository<Patient> entityRepository)
        {
            this.entityRepository = entityRepository;
        }
        public async Task<Patient> AddAsync(Patient entity)
        {
            return await entityRepository.AddAsync(entity);
        }

        public Task<int> CountAsync(Expression<Func<Patient, bool>> predicate)
        {
            return entityRepository.CountAsync(predicate);
        }

        public Task<int> CountAsync()
        {
            return entityRepository.CountAsync();
        }

        public IQueryable<Patient> Find(Expression<Func<Patient, bool>> predicate)
        {
            return entityRepository.Find(predicate);
        }

        public IQueryable<Patient> Get()
        {
            return entityRepository.Get();
        }

        public Task<Patient> GetAsync(string key)
        {
            return entityRepository.GetAsync(key);
        }

        public Task<Patient> RemoveAsync(Patient entity)
        {
            throw new NotImplementedException();
        }

        public Task<Patient> UpdateAsync(Patient entity)
        {
            return entityRepository.UpdateAsync(entity);
        }
    }
}
