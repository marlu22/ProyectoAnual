using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/users")]
public class UsersController : ControllerBase
{
    [HttpGet]
    public IActionResult GetAllUsers()
    {
        // Lógica para obtener todos los usuarios
        return Ok(new { Message = "Lista de usuarios." });
    }

    [HttpPost]
    public IActionResult CreateUser([FromBody] UserRequest request)
    {
        // Lógica para crear un usuario
        return Created("", new { Message = "Usuario creado." });
    }

    [HttpPut("{id}")]
    public IActionResult UpdateUser(int id, [FromBody] UserRequest request)
    {
        // Lógica para actualizar un usuario
        return Ok(new { Message = "Usuario actualizado." });
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteUser(int id)
    {
        // Lógica para eliminar un usuario
        return Ok(new { Message = "Usuario eliminado." });
    }
}

public class UserRequest
{
    public string Username { get; set; }
    public string Password { get; set; }
    public string Email { get; set; }
}
