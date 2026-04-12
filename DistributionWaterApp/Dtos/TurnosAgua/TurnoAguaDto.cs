using DistributionWaterApp.Dtos.Barrios;

namespace DistributionWaterApp.Dtos.TurnosAgua
{
    public class TurnoAguaDto
    {
        public string Id { get; set; }
        public BarrioDto Barrio { get; set; }
        public DateTime Fecha { get; set; }
        public string HoraInicio { get; set; }
        public string HoraFin { get; set; }
        public string Estado { get; set; }
        public string Observaciones { get; set; }
    }
}