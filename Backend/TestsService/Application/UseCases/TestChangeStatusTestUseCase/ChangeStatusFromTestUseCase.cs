
using Domain.Models;
using Domain.Repositories;

namespace Application.UseCases.TestChangeStatusTestUseCases
{
    public class ChangeStatusFromTestUseCase
    {
          private readonly ITestChangeStatusTestRepository _repository;
          private readonly IAttachmentRepository _attachmentRepository;
            public ChangeStatusFromTestUseCase(ITestChangeStatusTestRepository repository, IAttachmentRepository attachmentRepository)
            {
                _repository = repository;
                _attachmentRepository = attachmentRepository;
            }
       
            public async Task<List<ChangeStatusTest>> Execute(int idTest)
            {
                var statuses = await _repository.GetStatusByTestId(idTest);
                foreach (var status in statuses)
                {
                    if (status.attachment != null && status.attachment.Id != 0)
                    status.attachment = await _attachmentRepository.GetByIdAsync(status.attachment.Id);
                }
                return statuses;
            }

        
    }
}