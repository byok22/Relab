using Domain.Models;
using Shared.Dtos;

namespace Domain.Services
{
    public interface IUsersMicroService
    {
        public Task<List<UserDto>> GetAllUsers();
         Task<UserDto> GetUserByEmployeeAccount(string employeeAccount);

         Task<UserDto>  GetUserByID(int id);

         Task<UserDto> GetUserInfoLdapByEmployeeAccount(string employeeAccount);

         Task<List<UserDto>> GetUsersByType(string type);

          Task<UserDto>  AddUser(UserDto user);

          Task<UserDto>  UpdateUser(UserDto user);

          Task<List<UserProfile>> GetUserProfiles();



    }

}