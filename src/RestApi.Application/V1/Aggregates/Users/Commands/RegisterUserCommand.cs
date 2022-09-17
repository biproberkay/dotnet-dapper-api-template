﻿namespace RestApi.Application.V1.Aggregates.Users.Commands
{
    public class RegisterUserCommand
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Birthdate { get; set; }
        public string Password { get; set; }
        public string PasswordConfirmation { get; set; }
    }
}
