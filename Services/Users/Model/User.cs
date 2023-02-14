using System;
using System.Text.Json.Serialization;

namespace Users.Model
{
    public class User
    {
        [JsonIgnore]
        public int Id { get; set; }

        public string Name { get; set; } = "";

        public string Login { get; set; }

        public string Password { get; set; }

        [JsonIgnore]
        public DateTime CreationDateTime {get;set;} = DateTime.Now.ToUniversalTime();

        [JsonIgnore]
        public bool IsDeleted { get ; set; } = false;

        public User(string login, string password) 
        {
            Login = login;
            Password = password;   
        }

        public void ChangeData(User dataUser)
        {
            this.Login = dataUser.Login;
            this.Name = dataUser.Name;
            this.Password = dataUser.Password;
        }
    }
}