
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace tapr_2023_equipe1_historicoaluno_dotnet.Models.DiplomaModel;


[Table("diploma_instituicao")]
public class Diploma
{
    
    [Column("id")]
    [Required]
    public Guid Id {get; set;}

    [Column("matriculaAluno")]
    [Required]
    public string MatriculaAluno {get; set;} = string.Empty;
    
    [Column("idCurso")]
    [Required]
    public string IdCurso {get; set;} = string.Empty;
    
    [Column("mediaFinal")]
    [Required]
    public double MediaFinal {get; set;} = 0.00;
    
}