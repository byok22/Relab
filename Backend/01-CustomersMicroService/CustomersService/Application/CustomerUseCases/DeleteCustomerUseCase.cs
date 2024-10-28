using Application.CustomerUseCases;
using AutoMapper;
using Domain.Models;

using Domain.Repositories;
using Shared.Dtos;
using Shared.Response;

namespace Application.UseCases.CustomersUseCases
{
    public class DeleteCustomerUseCase : CustomerGenericUseCase
    {
        public DeleteCustomerUseCase(ICustomersRepository repository, IMapper mapper) : base(repository, mapper)
        {
        }

        public async Task<GenericResponse> Execute(Customer Customer)
        {
            
            if (Customer == null)
                throw new ArgumentNullException(nameof(Customer));


           var result = await _repository.RemoveAsync(Customer);
            return new GenericResponse
            {
                IsSuccessful = result.id>0?true:false,
                Message = result.message
            };
            
        }
    }
    
}