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
        
        public async Task<IEnumerable<Office>> GetAllOffices() =>
            await _officesRepository.GetAllOffices(trackChanges: false);

        public async Task<IEnumerable<Office>> GetOfficeInRange(int startIndex, int endIndex)
        {
            var officesInRange = await _officesRepository.GetOfficeInRange(startIndex, endIndex, trackChanges: false);

            if (startIndex >= endIndex)
            {
                throw new ArgumentsForPaginationException("StartIndex should be less than enIndex"); 
            }

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
            

        public async Task<Office> CreateOffice(OfficeForCreationRequest officeForCreation)
        {
            var office = _mapper.Map<Office>(officeForCreation);

            await _officesRepository.CreateOffice(office);
            await _officesRepository.Save();

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
            await _officesRepository.Save();
        }

        public async Task UpdaateOffice(Guid id, OfficeForUpdateRequest officeForUpdate)
        {
            var office = await _officesRepository.GetOffice(id, trackChanges: true);

            if (office is null)
            {
                throw new NotFoundException("Office with entered Id does not exsist");
            }

            _mapper.Map(officeForUpdate, office);

            _officesRepository.UpdateOffice(office);
            await _officesRepository.Save();
        }
    }
}
