﻿namespace LoginSystemBackEnd.Models
{
    public class Users
    {

        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? Surname { get; set; }
        public string Email{ get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
    }
}
