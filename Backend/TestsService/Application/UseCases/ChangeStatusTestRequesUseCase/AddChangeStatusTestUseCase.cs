using AutoMapper;
using Domain.Models;
using Domain.Repositories;
using Shared.Response;

namespace Application.UseCases.ChangeStatusTestUseCases
{
    public class AddChangeStatusTestUseCase : ChangeStatusTestAbstract
    {
        public AddChangeStatusTestUseCase(IChangeStatusTestRepository repository, IMapper mapper) : base(repository, mapper)
        {
        }

        public  async Task<GenericResponse> Execute(ChangeStatusTest ChangeStatusTest){

            if(ChangeStatusTest == null){
                return new GenericResponse(){
                    IsSuccessful = false,
                    Message = $"ChangeStatusTest is null"
                };
            }
            //revisar q el idtest sea mayor a 0
            if(ChangeStatusTest.idTest <= 0){
                return new GenericResponse(){
                    IsSuccessful = false,
                    Message = $"IdTest is invalid"
                };
            }



            if(ChangeStatusTest != null){

                var att = await _repository.AddAsync(ChangeStatusTest);
                return new GenericResponse(){
                    IsSuccessful = true,
                    Message = $"ChangeStatusTest Saved saved.",
                    Id = att.Id
                };
                
            }
            return new GenericResponse(){
                IsSuccessful = false,
                Message = $"Not Saved"
            };

        }
    }
}