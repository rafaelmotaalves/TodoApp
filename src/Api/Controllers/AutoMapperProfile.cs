using AutoMapper;
using Core.Entities;
using Api.Controllers.Dtos;

namespace Api.Controllers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile() {
            CreateMap<Board, BoardDto>(); 
            CreateMap<Board, BoardWithColumnsDto>();
            CreateMap<Column, ColumnDto>();
            CreateMap<Card, CardDto>();
        }
    }
}