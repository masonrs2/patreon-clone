using System;

namespace Paramatic.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        
        public string FirstName { get; set } = string.Empty;
        public string LastName { get; set } = string.Empty;

    }
}