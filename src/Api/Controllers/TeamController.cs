using Api.Controllers.Dtos;
using Core.Exceptions;
using Core.Team;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using AutoMapper;

namespace Api.Controllers
{
  [Authorize]
  [ApiController]
  [Route("[controller]")]
  public class TeamController : ControllerBase
  {

    private readonly ITeamRepository _teamRepository;
    private readonly ITeamService _teamService;
    private readonly IMapper _mapper;

    public TeamController(ITeamRepository teamRepository, IMapper mapper, ITeamService teamService)
    {
      _teamRepository = teamRepository;
      _mapper = mapper;
      _teamService = teamService;
    }

    [HttpGet]
    async public Task<ActionResult<List<TeamDto>>> GetAll()
    {
      var teams = await _teamRepository.GetAll();

      return _mapper.Map<List<Team>, List<TeamDto>>(teams);
    }

    [HttpGet("{id}")]
    async public Task<ActionResult<TeamDto>> Get(string id) {
        var team = await _teamRepository.Get(id);

        if (team is null)
            return NotFound();

        return _mapper.Map<Team, TeamDto>(team);
    }

    [HttpPost]
    async public Task<ActionResult<List<TeamDto>>> Create(CreateTeamDto createTeamDto)
    {
      var userId = GetUserId();
      await _teamService.Create(userId, createTeamDto.Name);
      return CreatedAtAction(nameof(Create), createTeamDto);
    }

    [HttpPost("{teamId}/members")]
    async public Task<ActionResult<List<TeamDto>>> AddMember(string teamId, AddMemberDto addMemberDto)
    {
      var userId = GetUserId();
      ActionResult actionResult;

      try {
        await _teamService.AddMember(teamId, userId, addMemberDto.userId);
        actionResult = NoContent();
      } catch (EntityNotFoundException) {
        actionResult = NotFound();
      } catch (UnauthorizedException) {
        actionResult = Unauthorized();
      }
      return actionResult;
    }

    [HttpPost("{teamId}/boards")]
    async public Task<ActionResult> AddBoard(string teamId, CreateBoardDto createBoardDto) {
      var userId = GetUserId();

      ActionResult actionResult;
      try {
        await _teamService.AddBoard(teamId, userId, createBoardDto.Name);
        actionResult = NoContent();
      } catch (EntityNotFoundException) {
        actionResult = NotFound();
      } catch (UnauthorizedException) {
        actionResult = Unauthorized();
      }
      return actionResult;
    }

    private string GetUserId()
    {
      var userId = HttpContext.Items["UserId"];
      if (userId is null)
        throw new Exception();

      return (string)userId;
    }

  }
}