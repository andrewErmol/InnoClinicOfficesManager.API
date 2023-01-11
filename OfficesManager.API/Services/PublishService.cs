using InnoClinic.Domain.Messages;
using InnoClinic.Domain.Messages.Office;
using MassTransit;
using OfficesManager.Contracts.IServices;
using OfficesManager.Domain.Model;

namespace OfficesManager.API.Services
{
    public class PublishService : IPublishService
    {
        private readonly IPublishEndpoint _publishEndpoint;

        public PublishService(IPublishEndpoint publishEndpoint)
        {
            _publishEndpoint = publishEndpoint;
        }

        public async Task PublishOfficeUpdatedMessage(Office office)
        {
            await _publishEndpoint.Publish(new OfficeUpdated
            {
                Id = office.Id,
                Address = office.Address
            });
        }

        public async Task PublishOfficeDeletedMessage(Office office)
        {
            await _publishEndpoint.Publish(new OfficeDeleted
            {
                Id = office.Id
            });
        }
    }
}
