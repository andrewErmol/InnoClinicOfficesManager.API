using OfficesManager.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OfficesManager.Contracts.IServices
{
    public interface IPublishService
    {
        Task PublishOfficeUpdatedMessage(Office office);
        Task PublishOfficeDeletedMessage(Office office);
    }
}
