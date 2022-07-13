using Microsoft.AspNetCore.Mvc;
using Core.Entities;
using Api.Controllers.Dtos;

namespace Api.Controllers;

[ApiController]
[Route("[controller]")]
public class BoardController : ControllerBase
{

  private IBoardRepository boardRepository;
  private IBoardService boardService;

  public BoardController(IBoardRepository _boardRepository, IBoardService _boardService)
  {
    boardRepository = _boardRepository;
    boardService = _boardService;
  }

  [HttpGet]
  public ActionResult<List<Board>> GetAll() => boardRepository.GetAll();

  [HttpGet("{id}")]
  public ActionResult<Board> Get(int id)
  {
    var board = boardRepository.Get(id);
    if (board is null)
      return NotFound();
    return board;
  }

  [HttpPost]
  public ActionResult Create(CreateBoardDto createBoardDto)
  {
    boardService.CreateBoard(createBoardDto.Name);
    return CreatedAtAction(nameof(Create), createBoardDto);
  }

}