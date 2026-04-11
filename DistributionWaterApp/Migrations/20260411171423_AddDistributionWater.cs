using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DistributionWaterApp.Migrations
{
    /// <inheritdoc />
    public partial class AddDistributionWater : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "zonas",
                columns: table => new
                {
                    id = table.Column<string>(type: "TEXT", nullable: false),
                    nombre_zona = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    cantidad_casas = table.Column<int>(type: "INTEGER", nullable: false),
                    created_by_id = table.Column<string>(type: "TEXT", nullable: true),
                    created_date = table.Column<string>(type: "TEXT", nullable: true),
                    updated_by_id = table.Column<string>(type: "TEXT", nullable: true),
                    updated_date = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_zonas", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "turnos_agua",
                columns: table => new
                {
                    id = table.Column<string>(type: "TEXT", nullable: false),
                    zona_id = table.Column<string>(type: "TEXT", nullable: false),
                    fecha = table.Column<DateTime>(type: "TEXT", nullable: false),
                    hora_inicio = table.Column<TimeSpan>(type: "TEXT", nullable: false),
                    hora_fin = table.Column<TimeSpan>(type: "TEXT", nullable: false),
                    estado = table.Column<string>(type: "TEXT", maxLength: 20, nullable: false),
                    observaciones = table.Column<string>(type: "TEXT", maxLength: 500, nullable: true),
                    created_by_id = table.Column<string>(type: "TEXT", nullable: true),
                    created_date = table.Column<string>(type: "TEXT", nullable: true),
                    updated_by_id = table.Column<string>(type: "TEXT", nullable: true),
                    updated_date = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_turnos_agua", x => x.id);
                    table.ForeignKey(
                        name: "FK_turnos_agua_zonas_zona_id",
                        column: x => x.zona_id,
                        principalTable: "zonas",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_turnos_agua_zona_id",
                table: "turnos_agua",
                column: "zona_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "turnos_agua");

            migrationBuilder.DropTable(
                name: "zonas");
        }
    }
}
