using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OfficesManager.Database.EntityForRepository
{
    public class OfficeEntity
    {
        public Guid Id { get; set; }

        public string Address { get; set; }

        public Guid PhotoId { get; set; }

        public string RegistryPhoneNumber { get; set; }
    }
}