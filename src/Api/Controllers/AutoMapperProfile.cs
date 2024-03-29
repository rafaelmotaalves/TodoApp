using AutoMapper;

using Core.User;
using Core.Board;
using Core.Team;
using Api.Controllers.Dtos;

namespace Api.Controllers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile() {
            CreateMap<Board, BoardDto>(); 
            CreateMap<UserBoard, BoardDto>();
            CreateMap<TeamBoard, BoardDto>();
            CreateMap<Board, BoardWithColumnsDto>();
            CreateMap<Column, ColumnDto>();
            CreateMap<Card, CardDto>();
            CreateMap<Team, TeamDto>();
            CreateMap<User, UserDto>();
            CreateMap<TeamUser, TeamUserDto>();
        }
    }
}