using Microsoft.AspNetCore.Mvc;
using tapr_2023_equipe1_historicoaluno_dotnet.Models.DiplomaModel;
using tapr_2023_equipe1_historicoaluno_dotnet.Services.DiplomaServ;




namespace tapr_2023_equipe1_historicoaluno_dotnet.Controllers;




[Route("/api/v1/[controller]")]
[ApiController]
public class DiplomaController : ControllerBase
{
    private IDiplomaService _diplomaService;


    
    public DiplomaController(IDiplomaService diplomaService)
    {
        _diplomaService = diplomaService;
    }



    
    [HttpGet]
    public async Task<List<Diploma>> Get()
    {

        var listaDiploma = await _diplomaService.GetAllAsync();


        return listaDiploma;
    }

    
    [HttpGet("byId/{id}")]
    public async Task<IResult> GetById(Guid id)
    {
        var umDiploma = await _diplomaService.GetByIdAsync(id);

        
        if(umDiploma == null)
            return Results.BadRequest($"\n\nErro: Ocorreu um erro ao buscar o diploma com id '{id}'.\nMotivo: Objeto retornou null.\n\n");    


        return Results.Ok(umDiploma);
    }



    
    [HttpPost]
    public async Task<Diploma> Create(Diploma vo)
    {
        var criarDiploma = await _diplomaService.CreateAsync(vo);

        return criarDiploma;
    }
  
    [HttpPut]
    public async Task<IResult> Update(Diploma vo)
    {
        if(vo == null)
            return Results.BadRequest("\n\nProblema: O modelo json para a atualização do diploma está vazio.\nSolução: Preencha todos os valores corretamente.\n\n");    


        var atualizarDiploma = await _diplomaService.UpdateAsync(vo);

        if(atualizarDiploma == null)
            return Results.BadRequest("\n\nErro: Ocorreu um erro ao atualizar o diploma.\nMotivo: Objeto retornou null.\n\n");    


        return Results.Ok(atualizarDiploma);
    }




    
    [HttpDelete("byId/{id}")]
    public async Task<IResult> DeleteById(Guid id)
    {
        var umDiploma = await _diplomaService.DeleteAsync(id);

        
        if(umDiploma == null)
            return Results.BadRequest($"\n\nErro: Ocorreu um erro ao excluir o diploma com id '{id}'.\nMotivo: Objeto retornou null.\n\n");    


        return Results.Ok(umDiploma);
    }



}