﻿
namespace KO.Entities
{
    public class User : BaseEntity
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public int AccessType { get; set; }
    }
}