using AutoMapper;
using FluentAssertions;
using Moq;
using OfficesManager.API.Services;
using OfficesManager.Contracts.IRepoitories;
using OfficesManager.Contracts.IServices;
using OfficesManager.Domain;
using OfficesManager.Domain.Model;
using OfficesManager.DTO.Office;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace OfficesManager.UnitTests.Services
{
    public class OfficesServiceTests
    {
        private readonly Mock<IOfficesRepository> _officesRepositoryMock;
        private readonly IOfficesService _officesService;
        private static IMapper _mapper;

        public OfficesServiceTests()
        {
            if (_mapper == null)
            {
                var mappingConfig = new MapperConfiguration(mc =>
                {
                    mc.AddProfile(new MappingProfile());
                });
                IMapper mapper = mappingConfig.CreateMapper();
                _mapper = mapper;
            }

            _officesRepositoryMock = new Mock<IOfficesRepository>();
            _officesService = new OfficesService(_officesRepositoryMock.Object, _mapper);
        }

        private List<Office> _testOffices = new List<Office>
        {
            new Office
            {
                Id = new Guid("3e77e8aa-b920-4bca-adb6-4a5e2de117f2"),
                Address = "pr Rechicki 135",
                PhotoId = new Guid("3e77e8aa-b920-4bca-adb6-4a5e2de117f3"),
                RegistryPhoneNumber = "+375(44)568-19-80"
            },
            new Office
            {
                Id = new Guid("e3d934ae-4002-41b7-8dcf-99709425f227"),
                Address = "pr Rechicki 132",
                PhotoId = new Guid("e3d934ae-4002-41b7-8dcf-99709425f228"),
                RegistryPhoneNumber = "+375(44)568-19-82"
            },
            new Office
            {
                Id = new Guid("7e1ca877-fb7a-46d0-b3d6-e0e102d5b6ad"),
                Address = "pr Rechicki 134",
                PhotoId = new Guid("8e1ca877-fb7a-46d0-b3d6-e0e102d5b6ad"),
                RegistryPhoneNumber = "+375(44)568-19-81"
            }
        };

        [Fact]
        public async Task GetAllOffices_OfficesExist_ReturnsListOfOffices()
        {
            // Arrange
            _officesRepositoryMock.Setup(r => r.GetAllOffices(false))
                .ReturnsAsync(_testOffices);

            // Act
            var result = await _officesService.GetAllOffices();

            // Assert
            result.Should().NotBeNull();
            result.Should().HaveSameCount(_testOffices);
            result.Should().BeEquivalentTo(_testOffices);
        }

        [Fact]
        public async Task GetAllOffices_OfficesDontExist_ReturnsEmpty()
        {
            // Arrange
            _officesRepositoryMock.Setup(r => r.GetAllOffices(false))
                .ReturnsAsync(new List<Office>());

            // Act
            var result = await _officesService.GetAllOffices();

            // Assertresult
            result.Should().NotBeNull();
            result.Should().HaveCount(0);
        }

        [Fact]
        public async Task GetAllOffices_DatabaseError_ThrowsException()
        {
            // Arrange
            _officesRepositoryMock.Setup(r => r.GetAllOffices(false))
                .ThrowsAsync(new Exception());

            // Act
            Task Act() => _officesService.GetAllOffices();

            // Assert
            await Assert.ThrowsAsync<Exception>(Act);
        }

        [Fact]
        public async Task GetOfficeInRange_OfficesExist_ReturnsListOfOfficesInRange()
        {
            // Arrange
            int startIndex = 1;
            int endIndex = 3;
            
            List<Office> offices = new List<Office>();
            offices.Add(_testOffices[0]);
            offices.Add(_testOffices[1]);

            _officesRepositoryMock.Setup(r => r.GetOfficeInRange(It.IsAny<int>(), It.IsAny<int>(), false))
                .ReturnsAsync(offices);

            // Act
            var result = await _officesService.GetOfficeInRange(startIndex, endIndex);

            // Assert
            result.Should().NotBeNull();
            result.Should().HaveSameCount(offices);
            result.Should().BeEquivalentTo(offices);
        }

        [Fact]
        public async Task GetOfficeInRange_InvalidRange_ThrowsException()
        {
            int startIndex = 1;
            int endIndex = 1;
            string errorMessage = "StartIndex should be less than enIndex";
            _officesRepositoryMock.Setup(r => r.GetOfficeInRange(It.IsAny<int>(), It.IsAny<int>(), false))
                .ReturnsAsync((IEnumerable<Office>)null);

            // Act
            Task Act() => _officesService.GetOfficeInRange(startIndex, endIndex);

            // Assert
            var result = await Assert.ThrowsAsync<ArgumentException>(Act);
            result.Message.Should().Be(errorMessage);
        }

        [Fact]
        public async Task GetOfficeInRange_DatabaseError_ThrowsException()
        {
            // Arrange
            _officesRepositoryMock.Setup(r => r.GetOfficeInRange(It.IsAny<int>(), It.IsAny<int>(), false))
                .ThrowsAsync(new Exception());

            // Act
            Task Act() => _officesService.GetOfficeInRange(new int(), new int());

            // Assert
            await Assert.ThrowsAsync<Exception>(Act);
        }

        [Fact]
        public async Task GetOfficeById_OfficeExists_ReturnsOffice()
        {
            // Arrange
            Office office = _testOffices[0];

            _officesRepositoryMock.Setup(r => r.GetOffice(It.IsAny<Guid>(), false))
                .ReturnsAsync(office);

            // Act
            var result = await _officesService.GetOfficeById(office.Id);

            // Assert
            result.Should().NotBeNull();
            result.Should().BeEquivalentTo(office);
        }

        [Fact]
        public async Task GetOfficeById_OfficesDoesntExist_ThrowsException()
        {
            // Arrange
            string errorMessage = "Office with entered Id does not exsist";
            _officesRepositoryMock.Setup(r => r.GetOffice(It.IsAny<Guid>(), false))
                .ReturnsAsync((Office)null);

            // Act
            Task Act() => _officesService.GetOfficeById(Guid.NewGuid());

            // Assert
            var result = await Assert.ThrowsAsync<NullReferenceException>(Act);
            result.Message.Should().Be(errorMessage);
        }

        [Fact]
        public async Task GetOfficeBiId_DatabaseError_ThrowsException()
        {
            // Arrange
            _officesRepositoryMock.Setup(r => r.GetOffice(It.IsAny<Guid>(), false))
                .ThrowsAsync(new Exception());

            // Act
            Task Act() => _officesService.GetOfficeById(Guid.NewGuid());

            // Assert
            await Assert.ThrowsAsync<Exception>(Act);
        }

        [Fact]
        public async Task CreateOffice_OfficeValid_ReturnsOffice()
        {
            // Arrange
            OfficeForCreationDto officeForCreation = new OfficeForCreationDto
            {
                Address = "pr Rechicki 135",
                PhotoId = new Guid("3e77e8aa-b920-4bca-adb6-4a5e2de117f3"),
                RegistryPhoneNumber = "+375(44)568-19-80"
            };

            _officesRepositoryMock.Setup(r => r.CreateOffice(It.IsAny<Office>()));
            _officesRepositoryMock.Setup(r => r.Save());

            // Act
            var result = await _officesService.CreateOffice(officeForCreation);

            // Assert
            result.Should().BeEquivalentTo(officeForCreation);
        }

        [Fact]
        public async Task CreateOffice_DatabaseError_ThrowsException()
        {
            // Arrange
            _officesRepositoryMock.Setup(r => r.CreateOffice(It.IsAny<Office>()))
                .ThrowsAsync(new Exception());

            // Act
            Task Act() => _officesService.CreateOffice(new OfficeForCreationDto());

            // Assert
            await Assert.ThrowsAsync<Exception>(Act);
        }

        [Fact]
        public async Task UpdateOffice_OfficeValid_UpdateOffice()
        {
            // Arrange
            Office office = _testOffices[0];

            OfficeForUpdateDto officeForUpdate = new OfficeForUpdateDto
            {
                Address = "pr Rechicki 135",
                PhotoId = new Guid("3e77e8aa-b920-4bca-adb6-4a5e2de117f3"),
                RegistryPhoneNumber = "+375(44)568-19-80"
            };

            _officesRepositoryMock.Setup(r => r.GetOffice(It.IsAny<Guid>(), true))
                .ReturnsAsync(office);
            _officesRepositoryMock.Setup(r => r.UpdateOffice(It.IsAny<Office>()));
            _officesRepositoryMock.Setup(r => r.Save());

            // Act
            await _officesService.UpdaateOffice(office.Id, officeForUpdate);
        }

        [Fact]
        public async Task UpdateOffice_OfficesDoesntExist_ThrowsException()
        {
            // Arrange
            string errorMessage = "Office with entered Id does not exsist";
            _officesRepositoryMock.Setup(s => s.GetOffice(It.IsAny<Guid>(), true))
                .ReturnsAsync((Office)null);

            // Act
            Task Act() => _officesService.UpdaateOffice(Guid.NewGuid(), new OfficeForUpdateDto());

            // Assert
            var result = await Assert.ThrowsAsync<NullReferenceException>(Act);
            result.Message.Should().Be(errorMessage);
        }

        [Fact]
        public async Task UpdateOffice_DatabaseError_ThrowsException()
        {
            // Arrange
            _officesRepositoryMock.Setup(r => r.GetOffice(It.IsAny<Guid>(), true))
                .ThrowsAsync(new Exception());

            // Act
            Task Act() => _officesService.UpdaateOffice(Guid.NewGuid(), null);

            // Assert
            await Assert.ThrowsAsync<Exception>(Act);
        }

        [Fact]
        public async Task Delete_ClientExists_DeleteOffice()
        {
            // Arrange
            Office office = _testOffices[0];

            _officesRepositoryMock.Setup(r => r.GetOffice(It.IsAny<Guid>(), false))
                .ReturnsAsync(office);
            _officesRepositoryMock.Setup(r => r.DeleteOffice(It.IsAny<Office>()));
            _officesRepositoryMock.Setup(r => r.Save());

            // Act
            await _officesService.DeleteOffice(office.Id);
        }

        [Fact]
        public async Task DeleteOffice_OfficesDoesntExist_ThrowsException()
        {
            // Arrange
            string errorMessage = "Office with entered Id does not exsist";
            _officesRepositoryMock.Setup(s => s.GetOffice(It.IsAny<Guid>(), false))
                .ReturnsAsync((Office)null);

            // Act
            Task Act() => _officesService.DeleteOffice(Guid.NewGuid());

            // Assert
            var result = await Assert.ThrowsAsync<NullReferenceException>(Act);
            result.Message.Should().Be(errorMessage);
        }

        [Fact]
        public async Task DeleteOffice_DatabaseError_ThrowsException()
        {
            // Arrange
            _officesRepositoryMock.Setup(r => r.GetOffice(It.IsAny<Guid>(), false))
                .ThrowsAsync(new Exception());

            // Act
            Task Act() => _officesService.DeleteOffice(Guid.NewGuid());

            // Assert
            await Assert.ThrowsAsync<Exception>(Act);
        }
    }
}
