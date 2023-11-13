using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace tapr_2023_equipe1_historicoaluno_dotnet.Models.HistoricoModel;


[Table("historico")]
public class Historico
{
    [Column("id")]
    [Required]
    public Guid Id {get; set;}

    [Column("matriculaAluno")]
    [Required]
    public string MatriculaAluno {get; set;} = string.Empty;
    
    [Column("nomeAluno")]
    [Required]
    public string NomeAluno {get; set;} = string.Empty;
    
    [Column("idCurso")]
    [Required]
    public string IdCurso {get; set;} = string.Empty;
    
    [Column("courseLevel")]
    [Required]
    public string NivelCurso {get; set;} = string.Empty;
    
    [Column("yearPhase")]
    [Required]
    public string FaseDoAno {get; set;} = string.Empty;
    
    [Column("notaMedia")]
    [Required]
    public string NotaMedia {get; set;} = string.Empty;
}