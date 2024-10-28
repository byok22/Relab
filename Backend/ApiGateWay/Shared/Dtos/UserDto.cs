namespace Shared.Dtos
{
    public class UserDto
    {
        public int Id { get; set;}
        public string UserName { get; set; }=string.Empty;
        public string EmployeeAccount { get; set; }=string.Empty;
        public string Email{ get; set; }=string.Empty;
        public string UserType { get; set; }=string.Empty;
        
    }
}