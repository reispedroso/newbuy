using Microsoft.AspNetCore.Mvc;

using Microsoft.AspNetCore.Authorization;
using newbuy.Domain.Models;
using newbuy.Domain.Interfaces;

namespace newbuy.Presentations.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController(IUserInterface userRepository) : ControllerBase
{
    private readonly IUserInterface _userRepository = userRepository;

    [HttpPost("createuser")]
    public async Task<IActionResult> CreateUser([FromBody] User user)
    {
        try
        {
            await _userRepository.CreateUser(user);
            return Ok();
        }
        catch (Exception e)
        {
            return StatusCode(500, $"Erro ao criar usuário: {e.Message}");
        }
    }

    [HttpPost("getuserbyid/{id}")]
    
    public async Task<IActionResult> GetUserById(Guid id)
    {
        var user = await _userRepository.GetUserById(id);
       
        return Ok(user);
    }

    [HttpPost("getallusers")]
   
    public async Task<IActionResult> GetAllUsers()
    {
        IEnumerable<User> users = await _userRepository.GetAllUsers();
       
        return Ok(users);
    }
    [HttpPost("updateuser/{id}")]
   
    public async Task<IActionResult> UpdateUser(Guid id, [FromBody] User user)
    {
       
        await _userRepository.UpdateUser(id, user);
        return Ok();
    }
}


