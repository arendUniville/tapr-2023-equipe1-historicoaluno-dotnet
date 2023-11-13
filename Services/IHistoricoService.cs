using tapr_2023_equipe1_historicoaluno_dotnet.Data.HistoricoVO;
using tapr_2023_equipe1_historicoaluno_dotnet.Models.HistoricoModel;



namespace tapr_2023_equipe1_historicoaluno_dotnet.Services;



public interface IHistoricoService
{
    Task<List<Historico>> GetAllAsync(); 
    Task<Historico> GetByIdAsync(Guid id); 


    Task<Historico> CreateAsync(Historico vo); 
    Task<Historico> UpdateAsync(AtualizarHistoricoVO vo); 
   
   
    Task<Historico> DeleteAsync(Guid id); 
}