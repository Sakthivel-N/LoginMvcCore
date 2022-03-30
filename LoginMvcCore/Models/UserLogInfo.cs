using System.ComponentModel.DataAnnotations;

namespace LoginMvcCore.Models
{
    public class UserLogInfo
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
        [Required]
        public string City { get; set; }

    }
}
