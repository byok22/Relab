

using Shared.Dtos;
using Shared.Response;

namespace Domain.Services
{
    public interface ICustomerService
    {
        Task<GenericResponse> AddCustomer(CustomerDto customerDto);
        Task<List<CustomerDto>> GetAllCustomer();
        Task<CustomerDto> GetCustomerById(int id);
        //get customer by customer id
        Task<CustomerDto> GetCustomerByCustomerId(string customerId);
        Task<GenericResponse> PatchCustomer(CustomerDto customerDto);
    }
}