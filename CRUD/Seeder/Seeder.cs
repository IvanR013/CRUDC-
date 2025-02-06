using CRUD.Repositories;
using CRUD.Models;

namespace CRUD.Seeder
{
    public class Seeder
    {
        private readonly IUserRepository _userRepository;
        private readonly ILogger<Seeder> _logger;

        public Seeder(IUserRepository userRepository, ILogger<Seeder> logger)
        {
            _userRepository = userRepository;
            _logger = logger;
        }

        public async Task Seed()
        {
            var users = new List<Users>
                    {
                        new Users
                        {
                            Nombre = "Juan",
                            Apellido = "Perez",
                            Edad = 25,
                            Email = "juancito132@gmail",
                            Password = BCrypt.Net.BCrypt.HashPassword("password123")
                        },

                        new Users
                        {
                            Nombre = "Pablo",
                            Apellido = "Martinez",
                            Edad = 24,
                            Email = "juancito132@gmail",
                            Password = BCrypt.Net.BCrypt.HashPassword("password1023")
                        },

                        new Users
                        {
                            Nombre = "Elmer",
                            Apellido = "Test",
                            Edad = 32,
                            Email = "juancito132@gmail",
                            Password = BCrypt.Net.BCrypt.HashPassword("passsword123")
                        },

                        new Users
                        {
                            Nombre = "Luchi",
                            Apellido = "Gonzalez",
                            Edad = 20,
                            Email = "juancito132@gmail",
                            Password = BCrypt.Net.BCrypt.HashPassword("password1123")
                        },

                        new Users
                        {
                            Nombre = "Nico",
                            Apellido = "Paz",
                            Edad = 25,
                            Email = "nik2132@gmail",
                            Password = BCrypt.Net.BCrypt.HashPassword("password1238")
                        },

                        new Users
                        {
                            Nombre = "Fran",
                            Apellido = "Tello",
                            Edad = 25,
                            Email = "juancito132@gmail",
                            Password = BCrypt.Net.BCrypt.HashPassword("password123890")
                        }
                };

            foreach (var user in users)
            {
                await _userRepository.AddUsersAsync(user);
            }

            await _userRepository.SaveChangesAsync();
        }
    }
}
