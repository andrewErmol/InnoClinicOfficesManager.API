using AutoMapper;
using OfficesManager.Contracts.IRepoitories;
using OfficesManager.Contracts.IServices;
using OfficesManager.Domain.Model;
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

        public async Task<Office> GetOfficeById(Guid id) =>
            await _officesRepository.GetOffice(id, trackChanges: false);

        public async Task<Office> CreateOffice(OfficeForCreationDto officeForCreation)
        {
            var office = _mapper.Map<Office>(officeForCreation);

            await _officesRepository.CreateOffice(office);
            await _officesRepository.Save();

            return office;
        }

        public async Task<bool> DeleteOffice(Guid id)
        {
            var office = await _officesRepository.GetOffice(id, trackChanges: false);

            if (office == null)
            {
                return false;
            }

            _officesRepository.DeleteOffice(office);
            await _officesRepository.Save();

            return true;
        }

        public async Task<bool> UpdaateOffice(Guid id, OfficeForUpdateDto officeForUpdate)
        {
            var office = await _officesRepository.GetOffice(id, trackChanges: true);

            if (office == null)
            {
                return false;
            }

            _mapper.Map(officeForUpdate, office);

            _officesRepository.UpdateOffice(office);
            await _officesRepository.Save();

            return true;
        }
    }
}
