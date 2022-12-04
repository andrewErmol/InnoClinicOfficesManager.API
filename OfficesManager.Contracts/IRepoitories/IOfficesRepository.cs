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
        Task<IEnumerable<Office>> GetAllOffices(bool trackChanges);
        Task<IEnumerable<Office>> GetOfficeInRange(int startIndex, int endIndex, bool trackChanges);
        Task<Office> GetOffice(Guid officeId, bool trackChanges);
        Task CreateOffice(Office office);
        void DeleteOffice(Office office);
        void UpdateOffice(Office office);
        Task Save();
    }
}
