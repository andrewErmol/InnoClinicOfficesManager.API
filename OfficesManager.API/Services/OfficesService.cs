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
        private readonly IMapper _mapper;

        public OfficesService(IOfficesRepository officesRepository, IMapper mapper)
        {
            _officesRepository = officesRepository;
            _mapper = mapper;
        }           

        public async Task<IEnumerable<Office>> GetOffices(int offset, int limit)
        {
            var officesInRange = await _officesRepository.GetOffices(offset, limit, trackChanges: false);

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
            

        public async Task<Office> CreateOffice(Office office)
        {
            await _officesRepository.CreateOffice(office);

            return office;
        }

        public async Task DeleteOffice(Guid id)
        {
            var office = await _officesRepository.GetOffice(id, trackChanges: false);

            if (office is null)
            {
                throw new NotFoundException("Office with entered Id does not exsist");
            }

            _officesRepository.DeleteOffice(office);
        }

        public async Task UpdateOffice(Office office)
        {
            _officesRepository.UpdateOffice(office);
        }
    }
}
