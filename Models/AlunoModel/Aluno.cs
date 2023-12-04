

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace tapr_2023_equipe1_historicoaluno_dotnet.Models.AlunoModel;




[Table("aluno")]
public class Aluno
{

    
    [Column("id")]
    [Required]
    public Guid Id {get; set;}

    [Column("nome")]
    [Required]
    public string Nome {get; set;} = string.Empty;
}