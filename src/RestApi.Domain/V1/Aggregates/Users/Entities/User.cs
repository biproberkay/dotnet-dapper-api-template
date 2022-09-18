﻿using RestApi.Domain.V1.Shared;

namespace RestApi.Domain.V1.Aggregates.Users.Entities
{
    public class User : Entity<User>, IAggregateRoot
    {
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string Email { get; private set; }
        public DateTime Birthdate { get; private set; }
        public string PasswordHash { get; private set; }
        public string? ResetPasswordCode { get; private set; }
        public DateTime? ResetPasswordExpiration { get; private set; }

        public User()
        {

        }

        public User(string firstName, string lastName, string email, DateTime birthdate)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            Birthdate = birthdate;
        }

        public void ConfigureResetPassword(string code) 
        {
            ResetPasswordCode = code;
            ResetPasswordExpiration = DateTime.Now.AddHours(24);
        }

        public void SetPassword(string hash)
        {
            PasswordHash = hash;
        }
    }
}
