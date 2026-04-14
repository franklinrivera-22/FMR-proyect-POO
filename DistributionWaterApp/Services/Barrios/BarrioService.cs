using Microsoft.EntityFrameworkCore;
using DistributionWaterApp.Dtos.Common;
using DistributionWaterApp.Dtos.Barrios;
using DistributionWaterApp.Mappers;
using DistributionWaterApp.Constants;
using DistributionWaterApp.Dtos.TurnosAgua;
using DistributionWaterApp.Services.Barrios;
using DistributionWaterApp.Database;

namespace DistributionWaterApp.Services.Barrios
{
    public class BarrioService : IBarrioService
    {
        private readonly AppDbContext _context;
        private readonly int PAGE_SIZE;
        private readonly int PAGE_SIZE_LIMIT;

        public BarrioService(AppDbContext context, IConfiguration configuration)
        {
            _context = context;
            PAGE_SIZE = configuration.GetValue<int>("PageSize");
            PAGE_SIZE_LIMIT = configuration.GetValue<int>("PageSizeLimit");
        }

        public async Task<ResponseDto<PageDto<List<BarrioDto>>>> GetPageAsync(string searchTerm = "", int page = 1, int pageSize = 10)
        {
            page = Math.Abs(page);
            pageSize = Math.Abs(pageSize);
            pageSize = pageSize <= 0 ? PAGE_SIZE : pageSize;
            pageSize = pageSize > PAGE_SIZE_LIMIT ? PAGE_SIZE_LIMIT : pageSize;

            int startIndex = (page - 1) * pageSize;

            IQueryable<DistributionWaterApp.Entities.Barrio> barrioQuery = _context.Barrios;

            if (!string.IsNullOrEmpty(searchTerm))
            {
                barrioQuery = barrioQuery.Where(b => b.Nombre.Contains(searchTerm));
            }

            int totalRows = await barrioQuery.CountAsync();

            var entities = await barrioQuery
                .OrderBy(b => b.Nombre)
                .Skip(startIndex)
                .Take(pageSize)
                .ToListAsync();

            return new ResponseDto<PageDto<List<BarrioDto>>>
            {
                StatusCode =HttpStatusCode.OK,
                status = true,
                Message = HttpMessageResponse.REGISTERS_FOUND,
                Data = new PageDto<List<BarrioDto>>
                {
                    CurrentPage = page == 0 ? 1 : page,
                    PageSize = pageSize,
                    TotalItems = totalRows,
                    TotalPages = (int)Math.Ceiling((double)totalRows / pageSize),
                    Items = BarrioMapper.ListEntityToListDto(entities),
                    HasNextPage = startIndex + pageSize < totalRows &&
                                  page < (int)Math.Ceiling((double)totalRows / pageSize),
                    HasPreviousPage = page > 1
                }
            };
        }

        public async Task<ResponseDto<BarrioDto>> GetOneByIdAsync(string id)
        {
            var entity = await _context.Barrios
                .FirstOrDefaultAsync(b => b.Id == id);

            if (entity is null)
            {
                return new ResponseDto<BarrioDto>
                {
                    StatusCode = HttpStatusCode.NOT_FOUND,
                    status = false,
                    Message = HttpMessageResponse.REGISTER_NOT_FOUND
                };
            }

            return new ResponseDto<BarrioDto>
            {
                StatusCode = HttpStatusCode.OK,
                status = true,
                Message = HttpMessageResponse.REGISTER_FOUND,
                Data = BarrioMapper.EntityToDto(entity)
            };
        }

        public async Task<ResponseDto<BarrioActionResponseDto>> CreateAsync(BarrioCreateDto dto)
        {
          
            bool exists = await _context.Barrios
                .AnyAsync(b => b.Nombre == dto.Nombre);

            if (exists)
            {
                return new ResponseDto<BarrioActionResponseDto>
                {
                    StatusCode = HttpStatusCode.CONFLICT,
                    status = false,
                    Message = "Ya existe un barrio con ese nombre"
                };
            }

            var entity = BarrioMapper.CreateDtoToEntity(dto);

            _context.Barrios.Add(entity);
            await _context.SaveChangesAsync();

            return new ResponseDto<BarrioActionResponseDto>
            {
                StatusCode = HttpStatusCode.CREATED,
                status = true,
                Message = HttpMessageResponse.REGISTER_CREATED,
                Data = new BarrioActionResponseDto { Id = entity.Id }
            };
        }

        public async Task<ResponseDto<BarrioActionResponseDto>> EditAsync( string id, BarrioEditDto dto)
        {
            var entity = await _context.Barrios
                .FirstOrDefaultAsync(b => b.Id == id);

            if (entity is null)
            {
                return new ResponseDto<BarrioActionResponseDto>
                {
                    StatusCode = HttpStatusCode.NOT_FOUND,
                    status = false,
                    Message = HttpMessageResponse.REGISTER_NOT_FOUND
                };
            }


            bool exists = await _context.Barrios
                .AnyAsync(b => b.Nombre == dto.Nombre && b.Id != id);

            if (exists)
            {
                return new ResponseDto<BarrioActionResponseDto>
                {
                    StatusCode = HttpStatusCode.CONFLICT,
                    status = false,
                    Message = "Ya existe un barrio con ese nombre"
                };
            }

            var updated = BarrioMapper.EditDtoToEntity(entity, dto);

            _context.Barrios.Update(updated);
            await _context.SaveChangesAsync();

            return new ResponseDto<BarrioActionResponseDto>
            {
                StatusCode = HttpStatusCode.OK,
                status = true,
                Message = HttpMessageResponse.REGISTER_UPDATED,
                Data = new BarrioActionResponseDto { Id = updated.Id }
            };
        }

        public async Task<ResponseDto<BarrioActionResponseDto>> DeleteAsync(string id)
        {
            var entity = await _context.Barrios
                .FirstOrDefaultAsync(b => b.Id == id);

            if (entity is null)
            {
                return new ResponseDto<BarrioActionResponseDto>
                {
                    StatusCode = HttpStatusCode.NOT_FOUND,
                    status = false,
                    Message = HttpMessageResponse.REGISTER_NOT_FOUND
                };
            }

            bool tieneTurnos = await _context.TurnoAgua
                .AnyAsync(t => t.BarrioId == id);

            if (tieneTurnos)
            {
                return new ResponseDto<BarrioActionResponseDto>
                {
                    StatusCode = HttpStatusCode.CONFLICT,
                    status = false,
                    Message = "No se puede eliminar el barrio porque tiene turnos de agua asignados"
                };
            }

            _context.Barrios.Remove(entity);
            await _context.SaveChangesAsync();

            return new ResponseDto<BarrioActionResponseDto>
            {
                StatusCode = HttpStatusCode.OK,
                status = true,
                Message = HttpMessageResponse.REGISTER_DELETED,
                Data = new BarrioActionResponseDto { Id = id }
            };
        }

    }
}