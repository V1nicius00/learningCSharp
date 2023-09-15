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
    public IActionResult Cadastrar(Carro carro)
    {
        _dbContext.Add(carro);
        _dbContext.SaveChanges();
        return Created("", carro);
    }

    [HttpGet]
    [Route("listar")]
    public async Task<ActionResult<IEnumerable<Carro>>> Listar()
    {
        return await _dbContext.Carro.ToListAsync();
    }

    [HttpGet]
    [Route("listar/{placa}")]
    public async Task<ActionResult<Carro>> Buscar(string placa)
    {
        var carroTemp = await _dbContext.Carro.FindAsync(placa);
        if(carroTemp is null)
            return NotFound();
        return carroTemp;
        
    }
}