using Dapr.Client;
using Microsoft.EntityFrameworkCore;
using tapr_2023_equipe1_historicoaluno_dotnet.Data.HistoricoVO;
using tapr_2023_equipe1_historicoaluno_dotnet.Models.Context;
using tapr_2023_equipe1_historicoaluno_dotnet.Models.HistoricoModel;



namespace tapr_2023_equipe1_historicoaluno_dotnet.Services;


public class HistoricoService : IHistoricoService
{

    private IConfiguration _configuration;
    private DaprClient _daprClient;
    private RepositoryDbContext _dbContext;


    public HistoricoService(RepositoryDbContext dbContext, IConfiguration configuration)
    {
        _dbContext = dbContext;
        _configuration = configuration;
        _daprClient = new DaprClientBuilder().Build();
    }





    public async Task<List<Historico>> GetAllAsync()
    {
        var listHistorico = await _dbContext.Historicos.ToListAsync();

        return listHistorico;
    }

    public async Task<Historico> GetByIdAsync(Guid id)
    {
        var umHistorico = await _dbContext.Historicos.FindAsync(id) ?? new Historico();

        if(umHistorico == null)
        {
            throw new Exception($"\n\nProblema: Não foi possível encontrar um histórico com id '{id}'.\nSolução: Verifique se o id inserido é válido.\n\n");
        }

        return umHistorico;
    }




    public async Task<Historico> CreateAsync(Historico vo)
    {
        vo.Id = Guid.Empty;
        await _dbContext.Historicos.AddAsync(vo);
        await _dbContext.SaveChangesAsync();

        //Enviando Dapr
        await PublishUpdateAsync(vo);

        return vo; //O id não vai retornar null aqui?
    }

    public async Task<Historico> UpdateAsync(AtualizarHistoricoVO vo)
    {
        var buscarHistorico = await _dbContext.Historicos.Where(c => c.Id == vo.Id).FirstOrDefaultAsync();        
        
        if (buscarHistorico != null)
        {
            buscarHistorico.MatriculaAluno = vo.MatriculaAluno;
            buscarHistorico.NomeAluno = vo.NomeAluno;
            buscarHistorico.IdCurso = vo.IdCurso;
            buscarHistorico.NivelCurso = vo.NivelCurso;
            buscarHistorico.FaseDoAno = vo.FaseDoAno;
            buscarHistorico.NotaMedia = vo.NotaMedia;

            _dbContext.Update(buscarHistorico); //Na referencia do código não foi atualizado o contexto, não é necessário?

            await _dbContext.SaveChangesAsync();

            
            //Enviando Dapr
            await PublishUpdateAsync(buscarHistorico);


            return buscarHistorico;
        }
        else
        {
            throw new ArgumentException($"\n\nProblema: Não foi possível encontrar um histórico com id '{vo.Id}'.\nSolução: Verifique se o id inserido é válido.\n\n");
        }

    }




    public async Task<Historico> DeleteAsync(Guid id)
    {
        var buscarHistorico = await _dbContext.Historicos.Where(c => c.Id == id).FirstOrDefaultAsync();

        if (buscarHistorico != null)
        {
            _dbContext.Remove(buscarHistorico);
        
            await _dbContext.SaveChangesAsync();
            
            //Enviando Dapr
            await PublishUpdateAsync(buscarHistorico);
            
            return buscarHistorico;
        }
        else
        {
            throw new ArgumentException($"\n\nProblema: Não foi possível encontrar um histórico com id '{id}'.\nSolução: Verifique se o id inserido é válido.\n\n");
        }
    }








    
    //Método para publicar o novo evento
    private async Task PublishUpdateAsync(Historico vo){
        await _daprClient.PublishEventAsync(_configuration["AppComponentService"], 
                                                _configuration["AppComponentTopicHistoricoAluno"], 
                                                vo);
    }
    
}