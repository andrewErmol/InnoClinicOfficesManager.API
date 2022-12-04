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
        Task<IEnumerable<Office>> GetAllOffices();
        Task<IEnumerable<Office>> GetOfficeInRange(int startIndex, int endIndex);
        Task<Office> GetOfficeById(Guid id);
        Task<Office> CreateOffice(OfficeForCreationDto officeForCreation);
        Task DeleteOffice(Guid id);
        Task UpdaateOffice(Guid id, OfficeForUpdateDto officeForUpdate);
    }
}
