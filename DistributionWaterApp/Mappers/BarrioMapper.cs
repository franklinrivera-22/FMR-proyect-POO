

using DistributionWaterApp.Dtos.Barrios;
using DistributionWaterApp.Entities;

namespace DistributionWaterApp.Mappers
{
    public static class BarrioMapper
    {
        public static Barrio CreateDtoToEntity(BarrioCreateDto dto)
        {
            return new Barrio
            {
                Id = Guid.NewGuid().ToString(),
                Nombre = dto.Nombre,
                CantidadCasas = dto.CantidadCasas
            };
        }

        public static Barrio EditDtoToEntity(Barrio entity, BarrioEditDto dto)
        {
            entity.Nombre = dto.Nombre;
            entity.CantidadCasas = dto.CantidadCasas;
            return entity;
        }

        public static BarrioDto EntityToDto(Barrio entity)
        {
            return new BarrioDto
            {
                Id = entity.Id,
                Nombre = entity.Nombre,
                CantidadCasas = entity.CantidadCasas
            };
        }

        public static List<BarrioDto> ListEntityToListDto(List<Barrio> entities)
        {
            return entities.Select(b => EntityToDto(b)).ToList();
        }
    }
}