

using Domain.Models;
using Domain.Repositories;

namespace Application.UseCases.Users
{
    public class GetUserByIdUseCase
    {

       
        private readonly IUserRepository _UserRepository;
        public GetUserByIdUseCase( IUserRepository UserRepository)
        {
            _UserRepository = UserRepository;
           
            
        }

        public  async Task<User> Execute(int id){           
            var result = await _UserRepository.GetByIdAsync(id);           
            return result;
        }
        
    }
}