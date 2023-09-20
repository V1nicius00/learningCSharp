using Estacionamento_API.Data;
using Estacionamento_API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
namespace Estacionamento_API.Controllers;

[ApiController]
[Route("[controller]")]

public class CarroController : ControllerBase
{
    private EstacionamentoDbContext _dbContext;

    public CarroController(EstacionamentoDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    [HttpPost]
    [Route("cadastrar")]
    public async Task<IActionResult> Cadastrar(Carro carro)
    {
        await _dbContext.AddAsync(carro);
        await _dbContext.SaveChangesAsync();
        return Created("", carro);
    }

    [HttpGet]
    [Route("listar")]
    public async Task<ActionResult<IEnumerable<Carro>>> Listar()
    {
        return await _dbContext.Carro.ToListAsync();
    }

    // [HttpGet]
    // [Route("listar")]
    // public async Task<ActionResult<IEnumerable<Carro>>> Listar()
    // {
    //     return await _dbContext.Carro.ToListAsync();
    // }

    [HttpGet]
    [Route("listar/{placa}")]
    public async Task<ActionResult<Carro>> Buscar(string placa)
    {
        if(_dbContext.Carro is null) return NotFound();
        var carroTemp = await _dbContext.Carro.FindAsync(placa);
        if(carroTemp is null) return NotFound();
        return carroTemp;
        
    }

    [HttpPut()]
    [Route("alterar")]
    public async Task<ActionResult> Alterar(Carro carro)
    {
        if(_dbContext.Carro is null) return NotFound();
        if(await _dbContext.Carro.FindAsync(carro.Placa) is null) return NotFound();
        _dbContext.Update(carro);
        await _dbContext.SaveChangesAsync();
        return Ok();
    }

    [HttpPatch()]
    [Route("mudardescricao/{placa}")]
    public async Task<ActionResult> MudarDescricao(string placa, [FromForm] string descricao)
    {
        if(_dbContext.Carro is null) return NotFound();
        var carroTemp = await _dbContext.Carro.FindAsync(placa);
        if(carroTemp is null) return NotFound();
        carroTemp.Descricao = descricao;
        await _dbContext.SaveChangesAsync();
        return Ok();
    }

    [HttpDelete]
    [Route("excluir")]
    public async Task<ActionResult> Excluir(string placa)
    {
        if(_dbContext.Carro is null) return NotFound();
        var carroTemp = await _dbContext.Carro.FindAsync(placa);
        if(carroTemp is null) return NotFound();
        _dbContext.Carro.Remove(carroTemp);
        await _dbContext.SaveChangesAsync();
        return Ok();
    }
}