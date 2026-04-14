using System.Collections.Generic;
using System.Threading.Tasks;
using DistributionWaterApp.Dtos.Barrios;
using DistributionWaterApp.Dtos.Common;


namespace DistributionWaterApp.Services.Barrios
{
    public interface IBarrioService
    {
        Task<ResponseDto<PageDto<List<BarrioDto>>>> GetPageAsync(string searchTerm = "", int page = 1, int pageSize = 10);
        Task<ResponseDto<BarrioDto>> GetOneByIdAsync(string id);
        Task<ResponseDto<BarrioActionResponseDto>> CreateAsync(BarrioCreateDto dto);
        Task<ResponseDto<BarrioActionResponseDto>> EditAsync(string id, BarrioEditDto dto);
        Task<ResponseDto<BarrioActionResponseDto>> DeleteAsync(string id);
    }
}