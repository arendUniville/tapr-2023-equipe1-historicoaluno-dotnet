using Dapr;
using Microsoft.AspNetCore.Mvc;
using tapr_2023_equipe1_historicoaluno_dotnet.Data.HistoricoVO;
using tapr_2023_equipe1_historicoaluno_dotnet.Models.AlunoModel;
using tapr_2023_equipe1_historicoaluno_dotnet.Models.HistoricoModel;
using tapr_2023_equipe1_historicoaluno_dotnet.Services;



namespace tapr_2023_equipe1_historicoaluno_dotnet.Controllers;




[Route("/api/v1/[controller]")]
[ApiController]
public class HistoricoController : ControllerBase
{

    private IHistoricoService _historicoService;

    public HistoricoController(IHistoricoService historicoService)
    {
        _historicoService = historicoService;
    }



    [HttpGet]
    public async Task<List<Historico>> Get()
    {

        var listaHistorico = await _historicoService.GetAllAsync();


        return listaHistorico;
    }

    
    [HttpGet("byId/{id}")]
    public async Task<IResult> GetById(Guid id)
    {
        var umHistorico = await _historicoService.GetByIdAsync(id);

        
        if(umHistorico == null)
            return Results.BadRequest($"\n\nErro: Ocorreu um erro ao buscar o histórico com id '{id}'.\nMotivo: Objeto retornou null.\n\n");    


        return Results.Ok(umHistorico);
    }



    
    [HttpPost]
    public async Task<Historico> Create(Historico vo)
    {
        var criarHistorico = await _historicoService.CreateAsync(vo);

        return criarHistorico;
    }
  
    [HttpPut]
    public async Task<IResult> Update(AtualizarHistoricoVO vo)
    {
        if(vo == null)
            return Results.BadRequest("\n\nProblema: O modelo json para a atualização do histórico está vazio.\nSolução: Preencha todos os valores corretamente.\n\n");    


        var atualizarHistorico = await _historicoService.UpdateAsync(vo);

        if(atualizarHistorico == null)
            return Results.BadRequest("\n\nErro: Ocorreu um erro ao atualizar o histórico.\nMotivo: Objeto retornou null.\n\n");    


        return Results.Ok(atualizarHistorico);
    }




    
    [HttpDelete("byId/{id}")]
    public async Task<IResult> DeleteById(Guid id)
    {
        var umHistorico = await _historicoService.DeleteAsync(id);

        
        if(umHistorico == null)
            return Results.BadRequest($"\n\nErro: Ocorreu um erro ao excluir o histórico com id '{id}'.\nMotivo: Objeto retornou null.\n\n");    


        return Results.Ok(umHistorico);
    }







}