using Domain.Models.TestModels;
using Domain.Repositories;
using Shared.Response;

namespace Application.UseCases.TestAttachmentsUseCases
{
    public class RemoveAttachmentFromTestUseCase
    {
        private readonly ITestAttachmentsRepository _repository;


        public RemoveAttachmentFromTestUseCase(ITestAttachmentsRepository repository)
        {
          
            _repository = repository;
           
        }
        
        public async Task<GenericResponse> Execute(int idTest, int idAttachment)
        {
            var testAttachments = new TestAttachment(){
                Id = 0,
                AttachmentId = idAttachment,
                TestId = idTest
            };
            var response = await _repository.RemoveAttachmentFromTest(testAttachments);
            return new GenericResponse(){
                IsSuccessful  = response.id>0?true:false,
                Message = response.message
            };

        }
        
    }
}