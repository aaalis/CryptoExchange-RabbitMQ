namespace Users.Model.Dto
{
    public class UserDto
    {
        public string Name { get; set; } = "";

        public string Login { get; set; } = "";

        public UserDto() { }

        public UserDto(string login, string name) 
        {
            Login = login;
            Name = name;  
        }  
    }
}