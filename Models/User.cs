using System.ComponentModel.DataAnnotations;

namespace Bot.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        public string RealName { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Username { get; set; }

        public string LanguageCode { get; set; }
    }
}
