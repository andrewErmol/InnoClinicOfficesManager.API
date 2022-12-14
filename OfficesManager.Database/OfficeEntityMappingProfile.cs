using AutoMapper;
using OfficesManager.Database.EntityForRepository;
using OfficesManager.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OfficesManager.Database
{
    public class OfficeEntityMappingProfile : Profile
    {
        public OfficeEntityMappingProfile()
        {
            CreateMap<Office, OfficeEntity>();
            CreateMap<OfficeEntity, Office>();
        }        
    }
}
