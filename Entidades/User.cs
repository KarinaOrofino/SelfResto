
namespace KO.Entities
{
    public class User : BaseEntity
    {
        public byte Id { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public byte AccessType { get; set; }
    }
}
