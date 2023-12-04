using Dapr.Client;
using Microsoft.EntityFrameworkCore;
using tapr_2023_equipe1_historicoaluno_dotnet.Models.AlunoModel;
using tapr_2023_equipe1_historicoaluno_dotnet.Models.Context;


namespace tapr_2023_equipe1_historicoaluno_dotnet.Services.AlunoServ;



public class AlunoService : IAlunoService
{

    
    private IConfiguration _configuration;
    private DaprClient _daprClient;
    private RepositoryDbContext _dbContext;




    public AlunoService(RepositoryDbContext dbContext, IConfiguration configuration)
    {
        _dbContext = dbContext;
        _configuration = configuration;
        _daprClient = new DaprClientBuilder().Build();
    }







    //Método para publicar o novo evento
    private async Task PublishUpdateAsync(Aluno aluno)
    {
        await this._daprClient.PublishEventAsync(
            _configuration["AppComponentService"], 
            _configuration["AppComponentTopicHistoricoAluno"], 
            aluno);
    }
    
    public async Task<Aluno> saveNewAsync(Aluno aluno)
    {
        aluno.Id = Guid.Empty;

        await _dbContext.Alunos.AddAsync(aluno);
        await _dbContext.SaveChangesAsync();
        
        //chamar o método para publicar o evento
        await PublishUpdateAsync(aluno);
        return aluno;
    }


    public async Task<Aluno> updateAsync(Guid id, Aluno aluno)
    {
        var alunoAntigo = await _dbContext.Alunos.Where(c => c.Id.Equals(id)).FirstOrDefaultAsync();        
        if (alunoAntigo != null){
            
            //Atualizar cada atributo do objeto antigo 
            alunoAntigo.Nome = aluno.Nome;

            await _dbContext.SaveChangesAsync();
            
            //chamar o método para publicar o evento
            await PublishUpdateAsync(alunoAntigo);
        }

        return alunoAntigo;
    }







    public Task<List<Aluno>> GetAllAsync()
    {
        throw new NotImplementedException();
    }


    public async Task<Aluno> updateEventAsync(Aluno aluno)
    {
        var alunoAntigo = await _dbContext.Alunos.Where(c => c.Id.Equals(aluno.Id)).FirstOrDefaultAsync();
        if (alunoAntigo == null){
            await _dbContext.Alunos.AddAsync(alunoAntigo);
            await _dbContext.SaveChangesAsync();
        }else{
            await updateAsync(aluno.Id.ToString(),aluno);
        }
        return aluno;
    }

     

    
    public async Task<Aluno> updateAsync(string id, Aluno aluno)
    {
        throw new NotImplementedException();
    }
}