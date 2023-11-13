


using tapr_2023_equipe1_historicoaluno_dotnet.Models.DiplomaModel;

namespace tapr_2023_equipe1_historicoaluno_dotnet.Services.DiplomaServ;


public interface IDiplomaService
{
    
    Task<List<Diploma>> GetAllAsync(); 
    Task<Diploma> GetByIdAsync(Guid id); 


    Task<Diploma> CreateAsync(Diploma vo); 
    Task<Diploma> UpdateAsync(Diploma vo); 
   
   
    Task<Diploma> DeleteAsync(Guid id); 
}