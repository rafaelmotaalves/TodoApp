using System.ComponentModel.DataAnnotations;

namespace Api.Controllers.Dtos
{
    public class AuthenticationDto
    {
        [Required]
        public string? Name { get; set; }

        [Required]
        public string? Password { get; set; }
    }
}