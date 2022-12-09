using AutoMapper;
using OfficesManager.Domain.Model;
using OfficesManager.DTO.Office;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OfficesManager.Domain
{
    public class OfficeMappingProfile : Profile
    {
        public OfficeMappingProfile()
        {
            CreateMap<OfficeForUpdateRequest, Office>();
            CreateMap<OfficeForCreationRequest, Office>();
        }
    }
}
