﻿using AutoMapper;
using OfficesManager.Domain.Model;
using OfficesManager.DTO.Office;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OfficesManager.Domain
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<OfficeForUpdateDto, Office>();
            CreateMap<OfficeForCreationDto, Office>();
        }
    }
}
