

using Dapr;
using Microsoft.AspNetCore.Mvc;
using tapr_2023_equipe1_historicoaluno_dotnet.Models.AlunoModel;
using tapr_2023_equipe1_historicoaluno_dotnet.Services.AlunoServ;

namespace tapr_2023_equipe1_historicoaluno_dotnet.Controllers;


public class AlunoController
{

    private IAlunoService _service;



    public AlunoController()
    {
        
    }


    

    [Topic(pubsubName:"servicebus-pubsub",name:"topico-equipe-1-aluno")] 
    [HttpPost("/event")]
    public async Task<IResult> UpdateClient(Aluno aluno)
    {      
        if(aluno == null){
            return Results.BadRequest();
        }
        Console.WriteLine("EVENT" + aluno.Nome);
        await _service.updateEventAsync(aluno);

        return Results.Ok(aluno);
    }

}