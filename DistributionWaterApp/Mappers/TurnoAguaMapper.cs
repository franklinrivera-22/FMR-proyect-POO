

using DistributionWaterApp.Dtos.Barrios;
using DistributionWaterApp.Dtos.TurnosAgua;
using DistributionWaterApp.Entities;
using Microsoft.AspNetCore.Mvc;

namespace DistributionWaterApp.Mappers
{
  public static class TurnoAguaMapper
    {
        public static TurnoAguaEntity CreateDtoToEntity(TurnoAguaCreateDto dto)
        {
            return new TurnoAguaEntity
            {
                Id = Guid.NewGuid().ToString(),
                BarrioId = dto.BarrioId,
                Fecha = dto.Fecha,
                HoraInicio = dto.HoraInicio,
                HoraFin = dto.HoraFin,
                Estado = dto.Estado,
                Observaciones = dto.Observaciones
            };
        }

        public static TurnoAguaEntity EditDtoToEntity(TurnoAguaEntity entity, TurnoAguaEditDto dto)
        {
            entity.BarrioId = dto.BarrioId;
            entity.Fecha = dto.Fecha;
            entity.HoraInicio = dto.HoraInicio;
            entity.HoraFin = dto.HoraFin;
            entity.Estado = dto.Estado;
            entity.Observaciones = dto.Observaciones;
            return entity;
        }

        public static TurnoAguaDto EntityToDto(TurnoAguaEntity entity)
        {
            return new TurnoAguaDto
            {
                Id = entity.Id,
                Barrio = new BarrioDto
                {
                    Id = entity.BarrioId,
                    Nombre = entity.Barrio?.Nombre,
                    CantidadCasas = entity.Barrio?.CantidadCasas ?? 0
                },
                Fecha = entity.Fecha,
                HoraInicio = entity.HoraInicio,
                HoraFin = entity.HoraFin,
                Estado = entity.Estado,
                Observaciones = entity.Observaciones
            };
        }

        public static List<TurnoAguaDto> ListEntityToListDto(List<TurnoAguaEntity> entities)
        {
            return entities.Select(t => EntityToDto(t)).ToList();
        }
    }

}