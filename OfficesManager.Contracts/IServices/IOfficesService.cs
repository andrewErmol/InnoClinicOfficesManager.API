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
        Task<Office> GetOfficeById(Guid id);
        Task<Office> CreateOffice(OfficeForCreationDto officeForCreation);
        Task<bool> DeleteOffice(Guid id);
        Task<bool> UpdaateOffice(Guid id, OfficeForUpdateDto officeForUpdate);
    }
}
