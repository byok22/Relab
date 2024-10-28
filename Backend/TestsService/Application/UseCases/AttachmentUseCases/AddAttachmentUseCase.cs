
using AutoMapper;
using Domain.Models;
using Domain.Repositories;
using Domain.Services;
using Shared.Dtos;
using Shared.Response;

namespace Application.UseCases.AttachmentUseCases
{
    public class AddAttachmentUseCase
    {
        private readonly IMapper _mapper;
        private readonly IAttachmentRepository _repository;
        private readonly IFDataService _iFDataService;
        
        public AddAttachmentUseCase(IMapper mapper, IAttachmentRepository repository, IFDataService fDataService)
        {
            _mapper  = mapper;
            _repository = repository;
            _iFDataService = fDataService;            
        }

        public  async Task<GenericResponse> Execute(AttachmentDto attachmentDto){

            //var attachment = await  _iFDataService.Save(attachmentDto);

            var attachment = _mapper.Map<Attachment>(attachmentDto);

            if(attachment != null){

                var att = await _repository.AddAsync(attachment);
                return new GenericResponse(){
                    IsSuccessful = true,
                    Message = $"{attachment.Name} saved.",
                    Id = att.Id,
                };
            }
            return new GenericResponse(){
                IsSuccessful = false,
                Message = $"Not Saved"
            };

        }
        
    }
}