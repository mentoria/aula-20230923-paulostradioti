using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace Citacoes.Api.Domain
{
    public class CitacoesDbContext : DbContext
    {
        private readonly IConfiguration config;

        public CitacoesDbContext(IConfiguration config)
        {
            this.config = config;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

            var dbPassword = config["DatabasePassword"];
            var connectionString = config.GetConnectionString("Default");

            var connectionStringBuilder = new SqlConnectionStringBuilder(connectionString);
            connectionStringBuilder.Password = dbPassword;


            optionsBuilder.UseSqlServer(connectionStringBuilder.ConnectionString);

            base.OnConfiguring(optionsBuilder);
        }

        public DbSet<Citacao> Citacoes { get; set; }
    }
}
