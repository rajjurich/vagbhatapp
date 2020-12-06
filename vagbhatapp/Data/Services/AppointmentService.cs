using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using vagbhatapp.Data.Core;
using vagbhatapp.Data.Entities;

namespace vagbhatapp.Data.Services
{
    public interface IAppointmentService : IEntityRepository<Appointment>
    {
    }
    public class AppointmentService : IAppointmentService
    {
        private readonly IEntityRepository<Appointment> entityRepository;

        public AppointmentService(IEntityRepository<Appointment> entityRepository)
        {
            this.entityRepository = entityRepository;
        }
        public async Task<Appointment> AddAsync(Appointment entity)
        {
            return await entityRepository.AddAsync(entity);
        }

        public Task<int> CountAsync(Expression<Func<Appointment, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public Task<int> CountAsync()
        {
            throw new NotImplementedException();
        }

        public IQueryable<Appointment> Find(Expression<Func<Appointment, bool>> predicate)
        {
            return entityRepository.Find(predicate);
        }

        public IQueryable<Appointment> Get()
        {
            throw new NotImplementedException();
        }

        public Task<Appointment> GetAsync(string key)
        {
            throw new NotImplementedException();
        }

        public Task<Appointment> RemoveAsync(Appointment entity)
        {
            throw new NotImplementedException();
        }

        public Task<Appointment> UpdateAsync(Appointment entity)
        {
            return entityRepository.UpdateAsync(entity);
        }
    }
}
