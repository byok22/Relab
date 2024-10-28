
using Domain.Models;
using Domain.Models.TestModels;
using Domain.Repositories;
using Shared.Response;

namespace Application.UseCases.TestAttachmentsUseCases
{
    public class CreateAndAsignAttachmentUseCase
    {
        private readonly ITestAttachmentsRepository _repository;
        private readonly IAttachmentRepository _attachmentsRepository;


        public CreateAndAsignAttachmentUseCase(ITestAttachmentsRepository repository, IAttachmentRepository attachmentRepository)
        {
          
            _repository = repository;
            _attachmentsRepository = attachmentRepository;
           
        }
        
        public async Task<GenericResponse> Execute(int idTest, Attachment attachment)
        {
            var response = await _attachmentsRepository.AddAsync(attachment);
            if(response!=null && response.Id>0){


                var testAttachments = new TestAttachment(){
                    Id = 0,
                    AttachmentId = response.Id,
                    TestId = idTest
                };
                var responser = await _repository.AssignAttachmentToTest(testAttachments);
                return new GenericResponse(){
                    IsSuccessful  = responser.id>0?true:false,
                    Message = responser.message
                };
            }
            return new GenericResponse(){
                IsSuccessful  = false,
                Message = "Attachment not saved"
            };

        }
    }
}