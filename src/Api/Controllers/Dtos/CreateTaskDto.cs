using System.ComponentModel.DataAnnotations;

namespace Api.Controllers.Dtos;

public class CreateTaskDto
{
  [Required]
  public string Name { get; set; }
}