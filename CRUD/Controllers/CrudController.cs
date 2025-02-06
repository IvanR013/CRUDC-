using CRUD.Models;
using CRUD.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CRUD.Controllers
{
    //http://localhost:5248/api/Crud/READ

    [ApiController]
    [Route("api/[controller]")]
    public partial class CrudController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly ILogger<CrudController> _logger;

        public CrudController(IUserRepository userRepository, ILogger<CrudController> logger)
        {
            this._userRepository = userRepository;
            this._logger = logger;
        }

        // Tarea READ
        [HttpGet("READ")]
        public async Task<IActionResult> GetUsers([FromQuery] List<int> id)
        {
            if (id == null || id.Count == 0)
            {
                return BadRequest(new { Status = "Error", Message = "No se recibieron IDs" });
            }

            _logger.LogInformation("IDs recibidos en el controlador: {Ids}", string.Join(", ", id));

            var users = await _userRepository.GetUsersById(id);

            if (users.Count == 0)
            {
                return NotFound(new { Status = "Error", Message = "No se encontraron usuarios" });
            }

            return Ok(new { Status = "Success", Users = users });
        }

        // Tarea CREATE
        [HttpPost("CREATE")]
        public async Task<IActionResult> AddUser([FromBody] Users users)
        {
            if (users == null)
            {
                return BadRequest(new { Status = "Error", Message = "No se recibieron datos" });
            }

            await _userRepository.AddUsersAsync(users);
            await _userRepository.SaveChangesAsync();

            return Ok(new { Status = "Success", Message = $"Usuario de nombre: {users.Nombre} creado exitosamente." });
        }
    }


    public partial class CrudController
    {
        // Tarea UPDATE
        [HttpPut("UPDATE")]
        public async Task<IActionResult> UpdateUser([FromBody] Users user)
        {
            if (user == null || user.Id <= 0)
            {
                return BadRequest(new { Status = "Error", Message = "No se recibieron datos" });

            }

            var existsUsers = await _userRepository.GetUsersById(new List<int> { user.Id });
            if (existsUsers.Count == 0 || existsUsers == null)
            {
                return NotFound(new { Status = "Error", Message = "No se encontraron usuarios" });

            }

            await _userRepository.UpdateUser(user);

            return Ok(new { Status = "Success", Message = "Usuario actualizado exitosamente." });
        }

        // Tarea DELETE
        [HttpDelete("DELETE")]
        public async Task<IActionResult> DeleteUser([FromBody] Users user)
        {
            if (user == null || user.Id <= 0)
            {
                return BadRequest(new { Status = "Error", Message = "No se recibieron datos" });

            }

            var existsUsers = await _userRepository.GetUsersById(new List<int> { user.Id });

            if (existsUsers.Count == 0 || existsUsers == null)
            {
                return NotFound(new { Status = "Error", Message = $"No se encontraron usuarios con id: {user.Id}" });
            }

            await _userRepository.DeleteChangesAsync(new List<int> { user.Id});
            await _userRepository.SaveChangesAsync();

            return Ok(new {status="Success", Message = $"Usuario con id: {user.Id} eliminado con éxito."});
        }
    }
}


