using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OfficesManager.DTO.Office
{
    public class OfficeForUpdateDto
    {
        public string Address { get; set; }

        public Guid PhotoId { get; set; }

        public string RegistryPhoneNumber { get; set; }
    }
}
