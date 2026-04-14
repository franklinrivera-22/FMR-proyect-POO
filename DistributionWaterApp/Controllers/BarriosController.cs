using DistributionWaterApp.Dtos.Barrios;
using DistributionWaterApp.Services.Barrios;
using Microsoft.AspNetCore.Mvc;

namespace DistributionWaterApp.Controllers
{
    [Route("api/barrios")]
    [ApiController]
    public class BarriosController: ControllerBase
    {
        private readonly IBarrioService _barrioService;

        public BarriosController(IBarrioService barrioService)
        {
            _barrioService = barrioService;
        }

        [HttpGet]
        public async Task<ActionResult> GetPage(
            string searchTerm = "", int page = 1, int pageSize = 10)
        {
            var response = await _barrioService.GetPageAsync(searchTerm, page, pageSize);
            return StatusCode(response.StatusCode, response);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetOne(string id)
        {
            var response = await _barrioService.GetOneByIdAsync(id);
            return StatusCode(response.StatusCode, response);
        }

        [HttpPost]
        public async Task<ActionResult> Create(BarrioCreateDto dto)
        {
            var response = await _barrioService.CreateAsync(dto);
            return StatusCode(response.StatusCode, response);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Edit(string id, BarrioEditDto dto)
        {
            var response = await _barrioService.EditAsync(id, dto);
            return StatusCode(response.StatusCode, response);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(string id)
        {
            var response = await _barrioService.DeleteAsync(id);
            return StatusCode(response.StatusCode, response);
        }
    }

}



