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

  [HttpPost("{id}/Columns")]
  public ActionResult CreateColumn(int id, CreateColumnDto createColumnDto)
  {
    try
    {
      boardService.CreateColumn(id, createColumnDto.Name);
      return CreatedAtAction(nameof(CreateColumn), createColumnDto);
    }
    catch (EntityNotFoundException)
    {
      return NotFound();
    }
  }

  [HttpPost("{boardId}/Columns/{columnId}/Tasks")]
  public ActionResult CreateTask(int boardId, int columnId, CreateTaskDto createTaskDto)
  {
    try
    {
      boardService.CreateTask(boardId, columnId, createTaskDto.Name);
      return CreatedAtAction(nameof(CreateTask), createTaskDto);
    }
    catch (EntityNotFoundException)
    {
      return NotFound();
    }
  }

  [HttpPut("{boardId}/Columns/{columnId}/Tasks/{taskId}")]
  public ActionResult UpdateTask(int boardId, int columnId, int taskId, UpdateTaskDto updateTaskDto)
  {
    try
    {
      boardService.UpdateTask(boardId, columnId, updateTaskDto.newColumnId, taskId);
      return NoContent();
    }
    catch (EntityNotFoundException)
    {
      return NotFound();
    }
  }

}