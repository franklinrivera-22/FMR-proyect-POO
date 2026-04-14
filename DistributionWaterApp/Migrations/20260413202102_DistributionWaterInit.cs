using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DistributionWaterApp.Migrations
{
    /// <inheritdoc />
    public partial class DistributionWaterInit : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_turnos_agua_zonas_zona_id",
                table: "turnos_agua");

            migrationBuilder.DropTable(
                name: "zonas");

            migrationBuilder.RenameColumn(
                name: "zona_id",
                table: "turnos_agua",
                newName: "barrio_id");

            migrationBuilder.RenameIndex(
                name: "IX_turnos_agua_zona_id",
                table: "turnos_agua",
                newName: "IX_turnos_agua_barrio_id");

            migrationBuilder.CreateTable(
                name: "barrios",
                columns: table => new
                {
                    id = table.Column<string>(type: "TEXT", nullable: false),
                    nombre = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    cantidad_casas = table.Column<int>(type: "INTEGER", nullable: false),
                    created_by_id = table.Column<string>(type: "TEXT", nullable: true),
                    created_date = table.Column<string>(type: "TEXT", nullable: true),
                    updated_by_id = table.Column<string>(type: "TEXT", nullable: true),
                    updated_date = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_barrios", x => x.id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_turnos_agua_barrios_barrio_id",
                table: "turnos_agua",
                column: "barrio_id",
                principalTable: "barrios",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_turnos_agua_barrios_barrio_id",
                table: "turnos_agua");

            migrationBuilder.DropTable(
                name: "barrios");

            migrationBuilder.RenameColumn(
                name: "barrio_id",
                table: "turnos_agua",
                newName: "zona_id");

            migrationBuilder.RenameIndex(
                name: "IX_turnos_agua_barrio_id",
                table: "turnos_agua",
                newName: "IX_turnos_agua_zona_id");

            migrationBuilder.CreateTable(
                name: "zonas",
                columns: table => new
                {
                    id = table.Column<string>(type: "TEXT", nullable: false),
                    cantidad_casas = table.Column<int>(type: "INTEGER", nullable: false),
                    created_by_id = table.Column<string>(type: "TEXT", nullable: true),
                    created_date = table.Column<string>(type: "TEXT", nullable: true),
                    nombre_zona = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    updated_by_id = table.Column<string>(type: "TEXT", nullable: true),
                    updated_date = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_zonas", x => x.id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_turnos_agua_zonas_zona_id",
                table: "turnos_agua",
                column: "zona_id",
                principalTable: "zonas",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
