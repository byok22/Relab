using Domain.Models;

using AutoMapper;
using Shared.Dtos;
using Domain.Models.TestRequests;
using Domain.Models.Employees;

namespace Shared.AutoMap
{
    public class AutoMapperForApp: Profile
    {
        public AutoMapperForApp()
        {

             CreateMap<Test, TestDto>();

              CreateMap<TestDto, Test>();

               CreateMap<TestRequestDto, TestRequest>();
               CreateMap< TestRequest, TestRequestDto>();

               CreateMap<SampleDto, Samples>();
               CreateMap< Samples, SampleDto>();

               CreateMap<EmployeeDto, Employee>();
               CreateMap< Employee, EmployeeDto>();

               CreateMap<SpecificationDto, Specification>();
               CreateMap< Specification, SpecificationDto>();



               

               

                // Mapeo entre Attachment y AttachmentDto
            CreateMap<Attachment, AttachmentDto>()
                .ForMember(dest => dest.File, opt => opt.Ignore()); // Omitir el campo File si no es mapeable

            CreateMap<AttachmentDto, Attachment>();
                //.ForMember(dest => dest.Id, opt => opt.Ignore()); // Si el Id se genera en el servidor, lo ignoramos en el mapeo inverso
           


         

            
        }

    
          /*Mapper.Initialize(cfg =>
            {
                cfg.RecognizePostfixes("Field");
                cfg.CreateMap<Source, Dest>();
            });
              CreateMap<MesDataDiag, MesDataDiagDto>()
               .ForMember(dest => dest.Department, opt => opt.UseValue("Development"));
            
            */
    }
}