using FluentAssertions;
using Moq;
using OfficesManager.API.Services;
using OfficesManager.Contracts.IRepoitories;
using OfficesManager.Contracts.IServices;
using OfficesManager.Domain.Model;
using OfficesManager.Domain.MyExceptions;
using Xunit;

namespace OfficesManager.UnitTests.Services
{
    public class OfficesServiceTests
    {
        private readonly Mock<IOfficesRepository> _officesRepositoryMock;
        private readonly IOfficesService _officesService;

        public OfficesServiceTests()
        {
            _officesRepositoryMock = new Mock<IOfficesRepository>();
            _officesService = new OfficesService(_officesRepositoryMock.Object);
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
        public async Task GetOffices_OfficesExist_ReturnsListOfOffices()
        {
            // Arrange
            int pageNumber = 0;
            int countOfEntities = 2;

            List<Office> offices = new List<Office>();
            offices.Add(_testOffices[0]);
            offices.Add(_testOffices[1]);

            _officesRepositoryMock.Setup(r => r.GetOffices(It.IsAny<int>(), It.IsAny<int>(), false))
                .ReturnsAsync(offices);

            // Act
            var result = await _officesService.GetOffices(pageNumber, countOfEntities);

            // Assert
            result.Should().NotBeNull();
            result.Should().HaveSameCount(offices);
            result.Should().BeEquivalentTo(offices);
        }

        [Fact]
        public async Task GetOffices_DatabaseError_ThrowsException()
        {
            // Arrange
            _officesRepositoryMock.Setup(r => r.GetOffices(It.IsAny<int>(), It.IsAny<int>(), false))
                .ThrowsAsync(new Exception());

            // Act
            Task Act() => _officesService.GetOffices(new int(), new int());

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
            var result = await Assert.ThrowsAsync<NotFoundException>(Act);
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
            Office office = new Office
            {
                Address = "pr Rechicki 135",
                PhotoId = new Guid("3e77e8aa-b920-4bca-adb6-4a5e2de117f3"),
                RegistryPhoneNumber = "+375(44)568-19-80"
            };

            _officesRepositoryMock.Setup(r => r.CreateOffice(It.IsAny<Office>()));

            // Act
            var result = await _officesService.CreateOffice(office);

            // Assert
            result.Should().BeEquivalentTo(office);
        }

        [Fact]
        public async Task CreateOffice_DatabaseError_ThrowsException()
        {
            // Arrange
            _officesRepositoryMock.Setup(r => r.CreateOffice(It.IsAny<Office>()))
                .ThrowsAsync(new Exception());

            // Act
            Task Act() => _officesService.CreateOffice(new Office());

            // Assert
            await Assert.ThrowsAsync<Exception>(Act);
        }

        [Fact]
        public async Task UpdateOffice_OfficeValid_UpdateOffice()
        {
            // Arrange
            Office office = _testOffices[0];

            _officesRepositoryMock.Setup(r => r.GetOffice(It.IsAny<Guid>(), true))
                .ReturnsAsync(office);
            _officesRepositoryMock.Setup(r => r.UpdateOffice(It.IsAny<Office>()));

            // Act
            await _officesService.UpdateOffice(office);
        }

        [Fact]
        public async Task UpdateOffice_DatabaseError_ThrowsException()
        {
            // Arrange
            _officesRepositoryMock.Setup(r => r.UpdateOffice(It.IsAny<Office>()))
                .ThrowsAsync(new Exception());

            // Act
            Task Act() => _officesService.UpdateOffice(new Office());

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
            var result = await Assert.ThrowsAsync<NotFoundException>(Act);
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
