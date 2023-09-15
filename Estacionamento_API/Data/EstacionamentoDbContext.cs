using Estacionamento_API.Models;
using Microsoft.EntityFrameworkCore;

namespace Estacionamento_API.Data;

public class EstacionamentoDbContext : DbContext
{
    public DbSet<Carro>? Carro { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite(connectionString: "DataSource=estacionamento.db;Cache=Shared;");
    }
}