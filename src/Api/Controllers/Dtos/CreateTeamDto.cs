using System.ComponentModel.DataAnnotations;

namespace Api.Controllers.Dtos
{
    public class CreateTeamDto
    {

        [Required]
        public string Name { get; set; }
    }
}