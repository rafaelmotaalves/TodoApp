using Microsoft.AspNetCore.Mvc;
using Core.Entities;
using Api.Controllers.Dtos;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;


namespace Api.Controllers;

[Authorize]
[ApiController]
[Route("[controller]")]
public class BoardController : ControllerBase
{

  private IBoardRepository boardRepository;
  private IBoardService boardService;
  private IMapper mapper;


  public BoardController(IBoardRepository _boardRepository, IBoardService _boardService, IMapper _mapper)
  {
    boardRepository = _boardRepository;
    boardService = _boardService;
    mapper = _mapper;
  }

  [HttpGet]
  async public Task<ActionResult<List<BoardDto>>> GetAll()
  {
    var userId = GetUserId();
    var boards = await boardRepository.GetAll(userId);

    return mapper.Map<List<Board>, List<BoardDto>>(boards);
  }

  [HttpGet("{id}")]
  async public Task<ActionResult<BoardWithColumnsDto>> Get(int id)
  {
    var userId = GetUserId();
    var board = await boardRepository.Get(userId, id);
    if (board is null)
      return NotFound();
    return mapper.Map<Board, BoardWithColumnsDto>(board);
  }

  [HttpPost]
  async public Task<ActionResult> Create(CreateBoardDto createBoardDto)
  {
    var userId = GetUserId();
    await boardService.CreateBoard(userId, createBoardDto.Name);
    return CreatedAtAction(nameof(Create), createBoardDto);
  }

  [HttpPost("{id}/Columns")]
  async public Task<ActionResult> CreateColumn(int id, CreateColumnDto createColumnDto)
  {
    try
    {
      var userId = GetUserId();
      await boardService.CreateColumn(userId, id, createColumnDto.Name);
      return CreatedAtAction(nameof(CreateColumn), createColumnDto);
    }
    catch (EntityNotFoundException)
    {
      return NotFound();
    }
    catch (UnauthorizedException)
    {
      return Unauthorized();
    }
  }

  [HttpPost("{boardId}/Columns/{columnId}/Tasks")]
  async public Task<ActionResult> CreateTask(int boardId, int columnId, CreateTaskDto createTaskDto)
  {
    try
    {
      var userId = GetUserId();
      await boardService.CreateTask(userId, boardId, columnId, createTaskDto.Name);
      return CreatedAtAction(nameof(CreateTask), createTaskDto);
    }
    catch (EntityNotFoundException)
    {
      return NotFound();
    }
    catch (UnauthorizedException)
    {
      return Unauthorized();
    }
  }

  [HttpPut("{boardId}/Columns/{columnId}/Tasks/{taskId}")]
  async public Task<ActionResult> UpdateTask(int boardId, int columnId, int taskId, UpdateTaskDto updateTaskDto)
  {
    try
    {
      var userId = GetUserId();
      await boardService.UpdateTask(userId, boardId, columnId, updateTaskDto.newColumnId, taskId);
      return NoContent();
    }
    catch (EntityNotFoundException)
    {
      return NotFound();
    }
    catch (UnauthorizedException)
    {
      return Unauthorized();
    }
  }

  private string GetUserId()
  {
    var userId = HttpContext.Items["UserId"];
    if (userId is null)
      throw new Exception();

    return (string)userId;
  }
}