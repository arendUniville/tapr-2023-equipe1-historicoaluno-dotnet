

using tapr_2023_equipe1_historicoaluno_dotnet.Models.AlunoModel;

namespace tapr_2023_equipe1_historicoaluno_dotnet.Services.AlunoServ;


public interface IAlunoService
{
    Task<List<Aluno>> GetAllAsync();

    Task<Aluno> updateAsync(string id, Aluno aluno);

    Task<Aluno> updateEventAsync(Aluno aluno);
}