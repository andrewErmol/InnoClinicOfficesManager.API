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

            throw new Exception("Failed to retrieve data");

            return Ok(offices);
        }

        [HttpGet("{id}", Name = "OfficeById")]
        public async Task<IActionResult> GetOffice(Guid id)
        {
            var office = await _officesService.GetOfficeById(id);

            if (office == null)
            {
                return NotFound($"Office with id: {id} doesn't exist in datebase");
            }

            return Ok(office);
        }

        [HttpPost]
        public async Task<IActionResult> CreateOffice([FromBody] OfficeForCreationDto office)
        {
            if (office == null)
            {
                return BadRequest("Object sent from office is null");
            }

            var officeToReturn = await _officesService.CreateOffice(office);

            return CreatedAtRoute("OfficeById", new { id = officeToReturn.Id }, officeToReturn);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var isOfficeFound = await _officesService.DeleteOffice(id);

            if (!isOfficeFound)
            {
                return NotFound($"Office with id: {id} doesn't exist in the database.");
            }

            return NoContent();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] OfficeForUpdateDto office)
        {
            if (office == null)
            {
                return BadRequest("Object sent from office is null");
            }

            var isOfficeFound = await _officesService.UpdaateOffice(id, office);

            if (!isOfficeFound)
            {
                return NotFound($"Office with id: {id} doesn't exist in datebase");
            }

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
        //public async Task<IActionResult> GetToken(string login, string password)
        //{
        //    var loginstring = new StringContent(login + password);
        //    var token = await _httpClient.PostAsync("https://localhost:7130/api/Account/Login", loginstring);

        //    var content = await token.Content.ReadAsStringAsync();

        //    return Ok(content);
        //}
    }
}
