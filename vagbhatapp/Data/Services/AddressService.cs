using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using vagbhatapp.Data.Core;
using vagbhatapp.Data.Entities;

namespace vagbhatapp.Data.Services
{
    public interface IAddressService : IEntityRepository<Address>
    {
    }
    public class AddressService : IAddressService
    {
        private readonly IEntityRepository<Address> entityRepository;

        public AddressService(IEntityRepository<Address> entityRepository)
        {
            this.entityRepository = entityRepository;
        }
        public async Task<Address> AddAsync(Address entity)
        {
            return await entityRepository.AddAsync(entity);
        }

        public Task<int> CountAsync(Expression<Func<Address, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public Task<int> CountAsync()
        {
            throw new NotImplementedException();
        }

        public IQueryable<Address> Find(Expression<Func<Address, bool>> predicate)
        {
            return entityRepository.Find(predicate);
        }

        public IQueryable<Address> Get()
        {
            throw new NotImplementedException();
        }

        public Task<Address> GetAsync(string key)
        {
            throw new NotImplementedException();
        }

        public Task<Address> RemoveAsync(Address entity)
        {
            throw new NotImplementedException();
        }

        public Task<Address> UpdateAsync(Address entity)
        {
            return entityRepository.UpdateAsync(entity);
        }
    }
}
