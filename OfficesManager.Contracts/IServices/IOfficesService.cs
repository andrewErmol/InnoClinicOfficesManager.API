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
        Task<IEnumerable<Office>> GetOffices(int offset, int limit);
        Task<Office> GetOfficeById(Guid id);
        Task<Office> CreateOffice(Office office);
        Task DeleteOffice(Guid id);
        Task UpdateOffice(Office office);
    }
}
