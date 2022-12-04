using Microsoft.EntityFrameworkCore;
using OfficesManager.Contracts.IRepoitories;
using OfficesManager.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OfficesManager.Database.Repositories
{
    public class OfficesRepository : IOfficesRepository
    {
        private OfficesManagerDbContext _dbContext;

        public OfficesRepository(OfficesManagerDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Office>> GetAllOffices (bool trackChanges)
        {
            IQueryable<Office> offices = !trackChanges 
                ? _dbContext.Offices.AsNoTracking() 
                : _dbContext.Offices;

            return await offices.ToListAsync();
        }

        public async Task<IEnumerable<Office>> GetOfficeInRange(int startIndex, int endIndex, bool trackChanges)
        {
            return await _dbContext.Offices.Skip(startIndex).Take(endIndex - startIndex).ToListAsync();
        }

        public async Task<Office> GetOffice(Guid officeId, bool trackChanges)
        {
            IQueryable<Office> offices = !trackChanges 
                ? _dbContext.Offices.Where(o => o.Id.Equals(officeId)).AsNoTracking() 
                : _dbContext.Offices.Where(o => o.Id.Equals(officeId));

            return await offices.SingleOrDefaultAsync();
        }

        public async Task CreateOffice(Office office) => await _dbContext.Offices.AddAsync(office);

        public void DeleteOffice(Office office) => _dbContext.Offices.Remove(office);

        public void UpdateOffice(Office office) => _dbContext.Offices.Update(office);

        public async Task Save() => await _dbContext.SaveChangesAsync();   
    }
}
