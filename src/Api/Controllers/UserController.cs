using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using AutoMapper;

using Api.Controllers.Dtos;

using Core.User;

namespace Api.Controllers
{  
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {

        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UserController(IUserRepository userRepository, IMapper mapper) {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        [HttpGet]
        async public Task<List<UserDto>> GetAll() {
            var users = await _userRepository.GetAll();

            return _mapper.Map<List<User>, List<UserDto>>(users);
        }
        
    }
}