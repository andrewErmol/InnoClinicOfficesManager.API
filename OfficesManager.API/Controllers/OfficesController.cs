using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using OfficesManager.Contracts.IServices;
using OfficesManager.Domain.Model;
using OfficesManager.DTO;
using OfficesManager.DTO.Office;

namespace OfficesManager.API.Controllers
{
    [Route("api/offices")]
    [ApiController]
    public class OfficesController : ControllerBase
    {
        private readonly IOfficesService _officesService;
        private readonly HttpClient _httpClient;
        private readonly IMapper _mapper;

        public OfficesController(IOfficesService officesService, HttpClient httpClient, ILogger<OfficesController> logger, IMapper mapper)
        {
            _officesService = officesService;
            _httpClient = httpClient;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetOffices([FromQuery]ArgumentsForPagination arg)
        {
            var offices = await _officesService.GetOffices(arg.offset, arg.limit);

            return Ok(offices);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetOffice(Guid id)
        {
            var office = await _officesService.GetOfficeById(id);

            return Ok(office);
        }

        [HttpPost]
        public async Task<IActionResult> CreateOffice([FromBody] OfficeForCreationRequest officeForCreation)
        {
            var office = _mapper.Map<Office>(officeForCreation);

            var officeToReturn = await _officesService.CreateOffice(office);

            return Ok(officeToReturn.Id);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _officesService.DeleteOffice(id);

            return NoContent();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] OfficeForUpdateRequest officeForUpdate)
        {
            var office = await _officesService.GetOfficeById(id);
            _mapper.Map(officeForUpdate, office);

            await _officesService.UpdateOffice(office);

            return NoContent();
        }

        [HttpPost("test")]
        public async Task<IActionResult> GetToken([FromBody] AccountForAuthenticationDto account)
        {
            var form = new MultipartFormDataContent
            {
                { new StringContent(account.UserName), nameof(AccountForAuthenticationDto.UserName)},
                { new StringContent(account.Password), nameof(AccountForAuthenticationDto.Password)},
            };
            var token = await _httpClient.PostAsync("https://localhost:7130/api/Account/Login", form);
            return Ok(await token.Content.ReadAsStringAsync());
        }
    }
}
