using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OfficesManager.Domain.Model
{
    public class Office
    {
        [Required(ErrorMessage = "Office Id is a required field")]
        public Guid Id { get; set; }
        
        [Required(ErrorMessage = "Address is a required field")]
        public string Address { get; set; }

        public Guid PhotoId { get; set; }

        [Required(ErrorMessage = "Registry_phone_number is a required field")]
        public string RegistryPhoneNumber { get; set; }
    }
}
