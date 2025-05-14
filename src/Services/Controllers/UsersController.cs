using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/users")]
public class UsersController : ControllerBase
{
    [HttpGet]
    public IActionResult GetAllUsers()
    {
        // L�gica para obtener todos los usuarios
        return Ok(new { Message = "Lista de usuarios." });
    }

    [HttpPost]
    public IActionResult CreateUser([FromBody] UserRequest request)
    {
        // L�gica para crear un usuario
        return Created("", new { Message = "Usuario creado." });
    }

    [HttpPut("{id}")]
    public IActionResult UpdateUser(int id, [FromBody] UserRequest request)
    {
        // L�gica para actualizar un usuario
        return Ok(new { Message = "Usuario actualizado." });
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteUser(int id)
    {
        // L�gica para eliminar un usuario
        return Ok(new { Message = "Usuario eliminado." });
    }
}

public class UserRequest
{
    public string Username { get; set; }
    public string Password { get; set; }
    public string Email { get; set; }
}
