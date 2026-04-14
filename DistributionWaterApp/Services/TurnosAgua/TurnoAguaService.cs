
using Microsoft.EntityFrameworkCore;
using DistributionWaterApp.Constants;
using DistributionWaterApp.Database;
using DistributionWaterApp.Dtos.Common;
using DistributionWaterApp.Dtos.TurnosAgua;
using DistributionWaterApp.Mappers;

namespace DistributionWaterApp.Services.TurnosAgua
{
    public class TurnoAguaService : ITurnoAguaService
    {
        private readonly AppDbContext _context;
        private readonly int PAGE_SIZE;
        private readonly int PAGE_SIZE_LIMIT;

        public TurnoAguaService(AppDbContext context, IConfiguration configuration)
        {
            _context = context;
            PAGE_SIZE = configuration.GetValue<int>("PageSize");
            PAGE_SIZE_LIMIT = configuration.GetValue<int>("PageSizeLimit");
        }

        public async Task<ResponseDto<PageDto<List<TurnoAguaDto>>>> GetPageAsync(
            string searchTerm = "", int page = 1, int pageSize = 10)
        {
            page = Math.Abs(page);
            pageSize = Math.Abs(pageSize);
            pageSize = pageSize <= 0 ? PAGE_SIZE : pageSize;
            pageSize = pageSize > PAGE_SIZE_LIMIT ? PAGE_SIZE_LIMIT : pageSize;

            int startIndex = (page - 1) * pageSize;

            IQueryable<Entities.TurnoAguaEntity> query = _context.TurnoAgua
                .Include(t => t.Barrio);

            if (!string.IsNullOrEmpty(searchTerm))
            {
                query = query.Where(t =>
                    t.Barrio.Nombre.Contains(searchTerm) ||
                    t.Estado.Contains(searchTerm));
            }

            int totalRows = await query.CountAsync();

            var entities = await query
                .OrderByDescending(t => t.Fecha)
                .ThenBy(t => t.HoraInicio)
                .Skip(startIndex)
                .Take(pageSize)
                .ToListAsync();

            return new ResponseDto<PageDto<List<TurnoAguaDto>>>
            {
                StatusCode = HttpStatusCode.OK,
                status = true,
                Message = HttpMessageResponse.REGISTERS_FOUND,
                Data = new PageDto<List<TurnoAguaDto>>
                {
                    CurrentPage = page == 0 ? 1 : page,
                    PageSize = pageSize,
                    TotalItems = totalRows,
                    TotalPages = (int)Math.Ceiling((double)totalRows / pageSize),
                    Items = TurnoAguaMapper.ListEntityToListDto(entities),
                    HasNextPage = startIndex + pageSize < totalRows &&
                                  page < (int)Math.Ceiling((double)totalRows / pageSize),
                    HasPreviousPage = page > 1
                }
            };
        }

        public async Task<ResponseDto<TurnoAguaDto>> GetOneByIdAsync(string id)
        {
            var entity = await _context.TurnoAgua
                .Include(t => t.Barrio)
                .FirstOrDefaultAsync(t => t.Id == id);

            if (entity is null)
            {
                return new ResponseDto<TurnoAguaDto>
                {
                    StatusCode = HttpStatusCode.NOT_FOUND,
                    status = false,
                    Message = HttpMessageResponse.REGISTER_NOT_FOUND
                };
            }

            return new ResponseDto<TurnoAguaDto>
            {
                StatusCode = HttpStatusCode.OK,
                status = true,
                Message = HttpMessageResponse.REGISTER_FOUND,
                Data = TurnoAguaMapper.EntityToDto(entity)
            };
        }

        public async Task<ResponseDto<TurnoAguaActionResponseDto>> CreateAsync(
            TurnoAguaCreateDto dto)
        {
            bool barrioExists = await _context.Barrios
                .AnyAsync(b => b.Id == dto.BarrioId);

            if (!barrioExists)
            {
                return new ResponseDto<TurnoAguaActionResponseDto>
                {
                    StatusCode = HttpStatusCode.NOT_FOUND,
                    status = false,
                    Message = "El barrio especificado no existe"
                };
            }

            if (string.Compare(dto.HoraFin, dto.HoraInicio) <= 0)
            {
                return new ResponseDto<TurnoAguaActionResponseDto>
                {
                    StatusCode = HttpStatusCode.BAD_REQUEST,
                    status = false,
                    Message = "La hora de fin debe ser mayor que la hora de inicio"
                };
            }

            var entity = TurnoAguaMapper.CreateDtoToEntity(dto);
            _context.TurnoAgua.Add(entity);
            await _context.SaveChangesAsync();

            return new ResponseDto<TurnoAguaActionResponseDto>
            {
                StatusCode = HttpStatusCode.CREATED,
                status = true,
                Message = HttpMessageResponse.REGISTER_CREATED,
                Data = new TurnoAguaActionResponseDto { Id = entity.Id }
            };
        }

        public async Task<ResponseDto<TurnoAguaActionResponseDto>> EditAsync(
            string id, TurnoAguaEditDto dto)
        {
            var entity = await _context.TurnoAgua
                .FirstOrDefaultAsync(t => t.Id == id);

            if (entity is null)
            {
                return new ResponseDto<TurnoAguaActionResponseDto>
                {
                    StatusCode = HttpStatusCode.NOT_FOUND,
                    status = false,
                    Message = HttpMessageResponse.REGISTER_NOT_FOUND
                };
            }

            bool barrioExists = await _context.Barrios
                .AnyAsync(b => b.Id == dto.BarrioId);

            if (!barrioExists)
            {
                return new ResponseDto<TurnoAguaActionResponseDto>
                {
                    StatusCode = HttpStatusCode.NOT_FOUND,
                    status = false,
                    Message = "El barrio especificado no existe"
                };
            }

            if (string.Compare(dto.HoraFin, dto.HoraInicio) <= 0)
            {
                return new ResponseDto<TurnoAguaActionResponseDto>
                {
                    StatusCode = HttpStatusCode.BAD_REQUEST,
                    status = false,
                    Message = "La hora de fin debe ser mayor que la hora de inicio"
                };
            }

            var updated = TurnoAguaMapper.EditDtoToEntity(entity, dto);
            _context.TurnoAgua.Update(updated);
            await _context.SaveChangesAsync();

            return new ResponseDto<TurnoAguaActionResponseDto>
            {
                StatusCode = HttpStatusCode.OK,
                status = true,
                Message = HttpMessageResponse.REGISTER_UPDATED,
                Data = new TurnoAguaActionResponseDto { Id = updated.Id }
            };
        }

        public async Task<ResponseDto<TurnoAguaActionResponseDto>> DeleteAsync(string id)
        {
            var entity = await _context.TurnoAgua
                .FirstOrDefaultAsync(t => t.Id == id);

            if (entity is null)
            {
                return new ResponseDto<TurnoAguaActionResponseDto>
                {
                    StatusCode = HttpStatusCode.NOT_FOUND,
                    status = false,
                    Message = HttpMessageResponse.REGISTER_NOT_FOUND
                };
            }

            _context.TurnoAgua.Remove(entity);
            await _context.SaveChangesAsync();

            return new ResponseDto<TurnoAguaActionResponseDto>
            {
                StatusCode = HttpStatusCode.OK,
                status = true,
                Message = HttpMessageResponse.REGISTER_DELETED,
                Data = new TurnoAguaActionResponseDto { Id = id }
            };
        }
    }
}