using DistributionWaterApp.Dtos.TurnosAgua;
using DistributionWaterApp.Services.TurnosAgua;
using Microsoft.AspNetCore.Mvc;

namespace DistributionWaterApp.Controllers
{
    [Route("api/turnos-agua")]
    [ApiController]
    public class TurnosAguaController : ControllerBase
    {
        private readonly ITurnoAguaService _turnoAguaService;

        public TurnosAguaController(ITurnoAguaService turnoAguaService)
        {
            _turnoAguaService = turnoAguaService;
        }

        [HttpGet]
        public async Task<ActionResult> GetPage(
            string searchTerm = "", int page = 1, int pageSize = 10)
        {
            var response = await _turnoAguaService.GetPageAsync(searchTerm, page, pageSize);
            return StatusCode(response.StatusCode, response);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetOne(string id)
        {
            var response = await _turnoAguaService.GetOneByIdAsync(id);
            return StatusCode(response.StatusCode, response);
        }

        [HttpPost]
        public async Task<ActionResult> Create(TurnoAguaCreateDto dto)
        {
            var response = await _turnoAguaService.CreateAsync(dto);
            return StatusCode(response.StatusCode, response);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Edit(string id, TurnoAguaEditDto dto)
        {
            var response = await _turnoAguaService.EditAsync(id, dto);
            return StatusCode(response.StatusCode, response);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(string id)
        {
            var response = await _turnoAguaService.DeleteAsync(id);
            return StatusCode(response.StatusCode, response);
        }
    }
}