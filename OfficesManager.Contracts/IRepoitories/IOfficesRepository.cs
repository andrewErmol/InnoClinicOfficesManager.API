using OfficesManager.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OfficesManager.Contracts.IRepoitories
{
    public interface IOfficesRepository
    {
        Task<IEnumerable<Office>> GetOffices(int offset, int countOfEntities, bool trackChanges);
        Task<Office> GetOffice(Guid officeId, bool trackChanges);
        Task<Guid> CreateOffice(Office office);
        Task DeleteOffice(Office office);
        Task UpdateOffice(Office office);
    }
}
