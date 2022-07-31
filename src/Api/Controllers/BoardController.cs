using Microsoft.AspNetCore.Mvc;
using Core.Entities;
using Api.Controllers.Dtos;
using AutoMapper;

namespace Api.Controllers;

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
  async public Task<ActionResult<List<BoardDto>>> GetAll() {
    var boards = await boardRepository.GetAll();

    return mapper.Map<List<Board>, List<BoardDto>>(boards);
  }

  [HttpGet("{id}")]
  async public Task<ActionResult<BoardWithColumnsDto>> Get(int id)
  {
    var board = await boardRepository.Get(id);
    if (board is null)
      return NotFound();
    return mapper.Map<Board, BoardWithColumnsDto>(board);
  }

  [HttpPost]
  async public Task<ActionResult> Create(CreateBoardDto createBoardDto)
  {
    await boardService.CreateBoard(createBoardDto.Name);
    return CreatedAtAction(nameof(Create), createBoardDto);
  }

  [HttpPost("{id}/Columns")]
  async public Task<ActionResult> CreateColumn(int id, CreateColumnDto createColumnDto)
  {
    try
    {
      await boardService.CreateColumn(id, createColumnDto.Name);
      return CreatedAtAction(nameof(CreateColumn), createColumnDto);
    }
    catch (EntityNotFoundException)
    {
      return NotFound();
    }
  }

  [HttpPost("{boardId}/Columns/{columnId}/Tasks")]
  async public Task<ActionResult> CreateTask(int boardId, int columnId, CreateTaskDto createTaskDto)
  {
    try
    {
      await boardService.CreateTask(boardId, columnId, createTaskDto.Name);
      return CreatedAtAction(nameof(CreateTask), createTaskDto);
    }
    catch (EntityNotFoundException)
    {
      return NotFound();
    }
  }

  [HttpPut("{boardId}/Columns/{columnId}/Tasks/{taskId}")]
  async public Task<ActionResult> UpdateTask(int boardId, int columnId, int taskId, UpdateTaskDto updateTaskDto)
  {
    try
    {
      await boardService.UpdateTask(boardId, columnId, updateTaskDto.newColumnId, taskId);
      return NoContent();
    }
    catch (EntityNotFoundException)
    {
      return NotFound();
    }
  }

}