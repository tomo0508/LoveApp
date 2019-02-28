using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using DatingApp.API.Data;
using DatingApp.API.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DatingApp.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IDatingRepository repo;
        private readonly IMapper mapper;

        public UsersController(IDatingRepository repo, IMapper mapper)
        {
            this.repo= repo;
            this.mapper = mapper;
        }

        [HttpGet]

        public async Task<IActionResult> GetUsers(){
            var users = await this.repo.GetUsers();

             var userToReturn= this.mapper.Map<IEnumerable<UserToListDto>>(users);
            return Ok(userToReturn);
           
        }

        [HttpGet("{id}")]

        public async Task <IActionResult> GetUser(int id){
            var user = await this.repo.GetUser(id);

            var userToReturn= this.mapper.Map<UserToDetailsDto>(user);
            return Ok(userToReturn);
        }
    }
}