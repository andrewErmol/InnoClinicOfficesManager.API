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
        Task<Office> GetOffice(Guid fridgeId, bool trackChanges);
        Task CreateOffice(Office fridge);
        void DeleteOffice(Office fridge);
        void UpdateOffice(Office fridge);
        Task Save();
    }
}
