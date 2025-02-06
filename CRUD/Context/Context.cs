using Microsoft.EntityFrameworkCore;
using CRUD.Models;


namespace CRUD.Context
{
    public class AppDbContext : DbContext
    {
        private readonly IConfiguration _configuration;

        public AppDbContext(DbContextOptions<AppDbContext> options, IConfiguration configuration) 
            : base(options)
        {
            this._configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var connectionString = _configuration.GetConnectionString("DefaultConnection");
                var serverVersion = new MySqlServerVersion(new Version(_configuration["DatabaseVersion"]));
                optionsBuilder.UseMySql(connectionString, serverVersion);
            }
        }

        public DbSet<Users> Users { get; set; }


    }
    
    
}
