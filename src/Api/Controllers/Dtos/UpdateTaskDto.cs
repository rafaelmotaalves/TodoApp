using System.ComponentModel.DataAnnotations;

namespace Api.Controllers.Dtos;

public class UpdateTaskDto
{
  [Required]
  public int newColumnId { get; set; }
}