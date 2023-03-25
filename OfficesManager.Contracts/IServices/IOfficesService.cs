using OfficesManager.Domain.Model;
using OfficesManager.DTO.Office;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OfficesManager.Contracts.IServices
{
    public interface IOfficesService
    {
        Task<IEnumerable<Office>> GetOffices(int pageNumber, int countOfEntities);
        Task<Office> GetOfficeById(Guid id);
        Task<Guid> CreateOffice(Office office);
        Task DeleteOffice(Guid id);
        Task UpdateOffice(Office office);
    }
}
