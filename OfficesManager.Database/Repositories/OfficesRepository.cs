using AutoMapper;
using Microsoft.EntityFrameworkCore;
using OfficesManager.Contracts.IRepoitories;
using OfficesManager.Database.EntityForRepository;
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
        private readonly IMapper _mapper;

        public OfficesRepository(OfficesManagerDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public async Task<IEnumerable<Office>> GetOffices(int startIndex, int endIndex, bool trackChanges)
        {
            IEnumerable<OfficeEntity> officeEntities = !trackChanges
                ? await _dbContext.Offices.Skip(startIndex).Take(endIndex - startIndex).AsNoTracking().ToListAsync()
                : await _dbContext.Offices.Skip(startIndex).Take(endIndex - startIndex).ToListAsync();

            return _mapper.Map<IEnumerable<Office>>(officeEntities);
        }

        public async Task<Office> GetOffice(Guid officeId, bool trackChanges)
        {
            OfficeEntity officeEntities = !trackChanges
                ? await _dbContext.Offices.AsNoTracking().FirstOrDefaultAsync(o => o.Id == officeId)
                : await _dbContext.Offices.FirstOrDefaultAsync(o => o.Id == officeId);

            return _mapper.Map<Office>(officeEntities);
        }

        public async Task CreateOffice(Office office)
        {
            OfficeEntity officeEntity = _mapper.Map<OfficeEntity>(office);
            await _dbContext.Offices.AddAsync(officeEntity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteOffice(Office office)
        {
            OfficeEntity officeEntity = _mapper.Map<OfficeEntity>(office);
            _dbContext.Offices.Remove(officeEntity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateOffice(Office office)
        {
            OfficeEntity officeEntity = _mapper.Map<OfficeEntity>(office);
            _dbContext.Offices.Update(officeEntity);
            await _dbContext.SaveChangesAsync();
        }
    }
}
