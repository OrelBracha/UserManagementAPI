
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    private readonly IUserRepository _userRepository;

    public UserController(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    [HttpPost("create")]
    public async Task<IActionResult> CreateUser([FromBody] User user)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var createdUser = await _userRepository.CreateUser(user);
        Console.WriteLine(createdUser);
        return CreatedAtAction(nameof(CreateUser), new { id = createdUser.UserId }, createdUser);
    }

    [HttpDelete("delete/{userId}")]
    public async Task<IActionResult> DeleteUser(int userId)
    {
        var result = await _userRepository.DeleteUser(userId);
        if (!result) return NotFound();
        return NoContent();
    }

    [HttpPost("validate")]
    public async Task<IActionResult> ValidateUser([FromBody] User user)
    {
        var isValid = await _userRepository.ValidateUser(user.UserName, user.UserPassword);
        if (!isValid) return Unauthorized();
        return Ok();
    }
}
