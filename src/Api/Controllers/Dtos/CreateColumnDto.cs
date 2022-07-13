using System.ComponentModel.DataAnnotations;

namespace Api.Controllers.Dtos;

public class CreateColumnDto
{
  [Required]
  public string Name { get; set; } = "";
}