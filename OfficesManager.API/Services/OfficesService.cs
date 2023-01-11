using AutoMapper;
using OfficesManager.Contracts.IRepoitories;
using OfficesManager.Contracts.IServices;
using OfficesManager.Domain.Model;
using OfficesManager.Domain.MyExceptions;
using OfficesManager.DTO.Office;

namespace OfficesManager.API.Services
{
    public class OfficesService : IOfficesService
    {
        private readonly IOfficesRepository _officesRepository;
        private readonly IPublishService _publishService;

        public OfficesService(IOfficesRepository officesRepository, IPublishService publishService)
        {
            _officesRepository = officesRepository;
            _publishService = publishService;
        }

        public async Task<IEnumerable<Office>> GetOffices(int pageNumber, int countOfEntities)
        {
            var offset = pageNumber * countOfEntities;
            var officesInRange = await _officesRepository.GetOffices(offset, countOfEntities, trackChanges: false);

            return officesInRange;
        }

        public async Task<Office> GetOfficeById(Guid id)
        {
            var office = await _officesRepository.GetOffice(id, trackChanges: false);

            if (office is null)  
            {
                throw new NotFoundException("Office with entered Id does not exsist");
            }

            return office;
        }            

        public async Task<Guid> CreateOffice(Office office)
        {
            var officeId = await _officesRepository.CreateOffice(office);

            return officeId;
        }

        public async Task DeleteOffice(Guid id)
        {
            var office = await _officesRepository.GetOffice(id, trackChanges: false);

            if (office is null)
            {
                throw new NotFoundException("Office with entered Id does not exsist");
            }

            await _officesRepository.DeleteOffice(office);
            await _publishService.PublishOfficeDeletedMessage(office);
        }

        public async Task UpdateOffice(Office office)
        {
            await _officesRepository.UpdateOffice(office);
            await _publishService.PublishOfficeUpdatedMessage(office);
        }
    }
}
