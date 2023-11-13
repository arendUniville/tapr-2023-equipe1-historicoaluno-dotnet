





using Microsoft.EntityFrameworkCore;
using tapr_2023_equipe1_historicoaluno_dotnet.Models.Context;
using tapr_2023_equipe1_historicoaluno_dotnet.Models.DiplomaModel;

namespace tapr_2023_equipe1_historicoaluno_dotnet.Services.DiplomaServ;


public class DiplomaService : IDiplomaService
{
    
    private RepositoryDbContext _dbContext;



    public DiplomaService(RepositoryDbContext dbContext)
    {
        _dbContext = dbContext;
    }




    public async Task<List<Diploma>> GetAllAsync()
    {
        var listaDiplomas = await _dbContext.Diplomas.ToListAsync();

        return listaDiplomas;
    }

    public async Task<Diploma> GetByIdAsync(Guid id)
    {
        var umDiploma = await _dbContext.Diplomas.FindAsync(id) ?? new Diploma();

        if(umDiploma == null)
        {
            throw new Exception($"\n\nProblema: Não foi possível encontrar um diploma com id '{id}'.\nSolução: Verifique se o id inserido é válido.\n\n");
        }

        return umDiploma;
    }




    public async Task<Diploma> CreateAsync(Diploma vo)
    {
        vo.Id = Guid.Empty;
        await _dbContext.Diplomas.AddAsync(vo);
        await _dbContext.SaveChangesAsync();

        return vo; //O id não vai retornar null aqui?
    }

    public async Task<Diploma> UpdateAsync(Diploma vo)
    {
        var buscarDiploma = await _dbContext.Diplomas.Where(c => c.Id == vo.Id).FirstOrDefaultAsync();        
        
        if (buscarDiploma != null)
        {
            buscarDiploma.MatriculaAluno = vo.MatriculaAluno;
            buscarDiploma.IdCurso = vo.IdCurso;
            buscarDiploma.MediaFinal = vo.MediaFinal;

            _dbContext.Update(buscarDiploma); //Na referencia do código não foi atualizado o contexto, não é necessário?

            await _dbContext.SaveChangesAsync();

            return buscarDiploma;
        }
        else
        {
            throw new ArgumentException($"\n\nProblema: Não foi possível encontrar um diploma com id '{vo.Id}'.\nSolução: Verifique se o id inserido é válido.\n\n");
        }
    }




    public async Task<Diploma> DeleteAsync(Guid id)
    {
        var buscarDiploma = await _dbContext.Diplomas.Where(c => c.Id == id).FirstOrDefaultAsync();

        if (buscarDiploma != null)
        {
            _dbContext.Remove(buscarDiploma);
        
            await _dbContext.SaveChangesAsync();
            
            return buscarDiploma;
        }
        else
        {
            throw new ArgumentException($"\n\nProblema: Não foi possível encontrar um diploma com id '{id}'.\nSolução: Verifique se o id inserido é válido.\n\n");
        }
    }

}