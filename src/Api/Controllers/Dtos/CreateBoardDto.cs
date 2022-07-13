using System.ComponentModel.DataAnnotations;

namespace Api.Controllers.Dtos;

public class CreateBoardDto
{
  [Required]
  public String Name { get; set; } = "";
}