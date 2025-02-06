using CRUD.Context;
using CRUD.Models;
using Microsoft.EntityFrameworkCore;


namespace CRUD.Repositories
{
    public interface IUserRepository
    {
        // READ
        Task<List<Users>> GetUsersById(List<int> id);

        // CREATE
        Task AddUsersAsync(Users users);
        
        // VALIDACION
        Task<bool> ValidateUserCredentialsAsync(string username, string password);
        
        //SAVE
        Task SaveChangesAsync();
        
        // DELETE
        Task DeleteChangesAsync(List<int> id);
        
        //UPDATE
        Task UpdateUser(Users users);
    }

    public partial class UserRepository : IUserRepository
    {
        private readonly AppDbContext _appContext;
        private readonly ILogger<UserRepository> _logger;

        public UserRepository(AppDbContext appContext, ILogger<UserRepository> logger)
        {
            this._appContext = appContext;
            this._logger = logger;
        }

        public async Task SaveChangesAsync()
        {
            await _appContext.SaveChangesAsync();

            _logger.LogInformation("Datos Guardados Exitosamente.");
        }

        public async Task<List<Users>> GetUsersById(List<int> id)
        {
            if (id == null || id.Count == 0)
            {
                _logger.LogWarning("La lista de IDs está vacía o es nula.");
                return new List<Users>();
            }

            _logger.LogInformation("Buscando usuarios con los siguientes IDs: {Ids}", string.Join(", ", id));

            var users = await _appContext.Users.Where(u => id.Contains(u.Id)).ToListAsync();

            if (users.Count == 0)
            {
                _logger.LogWarning("No se encontraron usuarios con los IDs proporcionados.");
            }

            return users;
        }

        public async Task AddUsersAsync(Users users)
        {
            if (users == null)
            {
                throw new ArgumentNullException(nameof(users), "El usuario no puede ser nulo.");
            }

            // Hashear la contraseña antes de guardarla en la base de datos
            users.Password = BCrypt.Net.BCrypt.HashPassword(users.Password);

            await _appContext.Users.AddAsync(users);
            await _appContext.SaveChangesAsync();

            _logger.LogInformation("Usuario añadido exitosamente: {Name}", users.Nombre);
        }

        //Busca usuarios por su id y contraseña y valida el hash de la contraseña con la ingresada.
        public async Task<bool> ValidateUserCredentialsAsync(string name, string password)
        {
            
            var user = await _appContext.Users.FirstOrDefaultAsync(u => u.Nombre == name);

            if (user == null)
            {
               throw new Exception("Credenciales Inválidas.");
            }

            return BCrypt.Net.BCrypt.Verify(password, user.Password);
        }
    }

    public partial class UserRepository : IUserRepository
    {
        
        // Buscamos usuarios por id y si hay registros se permite borrar uno o mas usuarios.
        public async Task DeleteChangesAsync(List<int> id)
        {
            var UserSet =  await _appContext.Users.Where(u => id.Contains(u.Id)).ToListAsync();

            if (!UserSet.Any())
            {
                throw new Exception("Error: No hay usuarios en BBDD");
            }

            _appContext.Users.RemoveRange(UserSet);

            await _appContext.SaveChangesAsync();
        }
       
        // Verifica usuarios existentes por id, valida si no tienen valores null y despues permite cambios.
        public async Task UpdateUser(Users users)
        {
            var ExistUsers = await _appContext.Users.FindAsync(users.Id);


            if (ExistUsers == null)
            {
                throw new Exception("Error: No hay usuarios en BBDD");
            }

            _appContext.Entry(ExistUsers).CurrentValues.SetValues(users);
            await _appContext.SaveChangesAsync();
        }
    }
}
