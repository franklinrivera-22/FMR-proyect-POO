
using DistributionWaterApp.Dtos.Common;
using DistributionWaterApp.Dtos.TurnosAgua;

namespace DistributionWaterApp.Services.TurnosAgua
{
    public interface ITurnoAguaService
    {
        Task<ResponseDto<PageDto<List<TurnoAguaDto>>>> GetPageAsync(string searchTerm = "", int page = 1, int pageSize = 10);
        Task<ResponseDto<TurnoAguaDto>> GetOneByIdAsync(string id);
        Task<ResponseDto<TurnoAguaActionResponseDto>> CreateAsync(TurnoAguaCreateDto dto);
        Task<ResponseDto<TurnoAguaActionResponseDto>> EditAsync(string id, TurnoAguaEditDto dto);
        Task<ResponseDto<TurnoAguaActionResponseDto>> DeleteAsync(string id);
    }
}