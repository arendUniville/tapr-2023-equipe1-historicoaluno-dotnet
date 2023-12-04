using Azure.Identity;
using Microsoft.Azure.Cosmos;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ValueGeneration;
using tapr_2023_equipe1_historicoaluno_dotnet.Models.AlunoModel;
using tapr_2023_equipe1_historicoaluno_dotnet.Models.DiplomaModel;
using tapr_2023_equipe1_historicoaluno_dotnet.Models.HistoricoModel;


namespace tapr_2023_equipe1_historicoaluno_dotnet.Models.Context;



public class RepositoryDbContext : DbContext
{
    public DbSet<Historico> Historicos {get; set;}
    public DbSet<Diploma> Diplomas {get; set;}
    public DbSet<Aluno> Alunos {get; set;}


    private IConfiguration _configuration;
    
    
    
    public RepositoryDbContext(IConfiguration configuration)
    {
        _configuration = configuration;
    }




    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseCosmos(
            accountEndpoint: _configuration["CosmosDBURL"],
            tokenCredential: new DefaultAzureCredential(),
            databaseName: _configuration["CosmosDBDBName"],
            options => { options.ConnectionMode(ConnectionMode.Gateway); });
    }




    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Historico>()
            .HasNoDiscriminator();
        modelBuilder.Entity<Historico>()
            .ToContainer("historico");
        modelBuilder.Entity<Historico>()
            .Property(p => p.Id)
            .HasValueGenerator<GuidValueGenerator>();
        modelBuilder.Entity<Historico>()
            .HasPartitionKey(o => o.MatriculaAluno);
        

        
        modelBuilder.Entity<Diploma>()
            .HasNoDiscriminator();
        modelBuilder.Entity<Diploma>()
            .ToContainer("diploma_instituicao");
        modelBuilder.Entity<Diploma>()
            .Property(p => p.Id)
            .HasValueGenerator<GuidValueGenerator>();
        modelBuilder.Entity<Diploma>()
            .HasPartitionKey(o => o.MatriculaAluno);
        
    }
}


