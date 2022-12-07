using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OfficesManager.Contracts.IServices;
using OfficesManager.DTO;
using OfficesManager.DTO.Office;

namespace OfficesManager.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OfficesController : ControllerBase
    {
        private readonly IOfficesService _officesService;
        private readonly HttpClient _httpClient;
        private readonly ILogger<OfficesController> _logger;

        public OfficesController(IOfficesService officesService, HttpClient httpClient, ILogger<OfficesController> logger)
        {
            _officesService = officesService;
            _httpClient = httpClient;
            _logger = logger;
        }

        //[Authorize]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var offices = await _officesService.GetAllOffices();

            return Ok(offices);
        }

        [HttpGet("pagination")]
        public async Task<IActionResult> GetOfficeInRange([FromQuery]int startIndex, [FromQuery]int endIndex)
        {
            var officesInRange = await _officesService.GetOfficeInRange(startIndex, endIndex);

            return Ok(officesInRange);
        }

        [HttpGet("{id}", Name = "OfficeById")]
        public async Task<IActionResult> GetOffice(Guid id)
        {
            var office = await _officesService.GetOfficeById(id);

            return Ok(office);
        }

        [HttpPost]
        public async Task<IActionResult> CreateOffice([FromBody] OfficeForCreationRequest office)
        {
            var officeToReturn = await _officesService.CreateOffice(office);

            return CreatedAtRoute("OfficeById", new { id = officeToReturn.Id }, officeToReturn);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _officesService.DeleteOffice(id);
            return NoContent();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] OfficeForUpdateRequest office)
        {
            await _officesService.UpdaateOffice(id, office);

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
