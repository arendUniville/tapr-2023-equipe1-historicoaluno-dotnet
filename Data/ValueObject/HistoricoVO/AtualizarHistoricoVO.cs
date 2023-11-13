namespace tapr_2023_equipe1_historicoaluno_dotnet.Data.HistoricoVO;



public class AtualizarHistoricoVO
{
    public Guid Id {get; set;}
    public string MatriculaAluno {get; set;} = string.Empty;
    
    public string NomeAluno {get; set;} = string.Empty;
    
    public string IdCurso {get; set;} = string.Empty;
    
    public string NivelCurso {get; set;} = string.Empty;
    
    public string FaseDoAno {get; set;} = string.Empty;
    
    public string NotaMedia {get; set;} = string.Empty;
}