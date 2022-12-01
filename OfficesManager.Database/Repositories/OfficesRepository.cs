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
        private OfficesManagerDbContext OfficesManagerDbContext;

        public OfficesRepository(OfficesManagerDbContext officesManagerDbContext)
        {
            OfficesManagerDbContext = officesManagerDbContext;
        }

        public async Task<IEnumerable<Office>> GetAllOffices (bool trackChanges)
        {
            IQueryable<Office> offices = !trackChanges ? OfficesManagerDbContext.Offices.AsNoTracking() : OfficesManagerDbContext.Offices;

            return await offices.ToListAsync();
        }

        public async Task<Office> GetOffice(Guid officeId, bool trackChanges)
        {
            IQueryable<Office> offices = !trackChanges ? OfficesManagerDbContext.Offices.Where(o => o.Id.Equals(officeId)).AsNoTracking() : OfficesManagerDbContext.Offices.Where(o => o.Id.Equals(officeId));

            return await offices.SingleOrDefaultAsync();
        }

        public async Task CreateOffice(Office office) => await OfficesManagerDbContext.Offices.AddAsync(office);

        public void DeleteOffice(Office office) => OfficesManagerDbContext.Offices.Remove(office);

        public void UpdateOffice(Office office) => OfficesManagerDbContext.Offices.Update(office);

        public async Task Save() => await OfficesManagerDbContext.SaveChangesAsync();
    }
}
