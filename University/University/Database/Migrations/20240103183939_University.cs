using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace University.Database.Migrations
{
    /// <inheritdoc />
    public partial class University : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "facultad",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    estado = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    nombre_facultad = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    codigo_facultad = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: false),
                    creado_tmstp = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getdate()"),
                    actualizado_tmstp = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_facultad", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "carrera",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    estado = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    facultad_id = table.Column<int>(type: "int", nullable: false),
                    nombre_carrera = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    codigo_carrera = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: false),
                    creado_tmstp = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getdate()"),
                    actualizado_tmstp = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_carrera", x => x.id);
                    table.ForeignKey(
                        name: "FK_carrera_facultad_facultad_id",
                        column: x => x.facultad_id,
                        principalTable: "facultad",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "facultad",
                columns: new[] { "id", "codigo_facultad", "actualizado_tmstp", "nombre_facultad", "estado" },
                values: new object[,]
                {
                    { 1, "FC", null, "Facultad de Ciencias", true },
                    { 2, "FAUA", null, "Facultad de Arquitectura", true },
                    { 3, "FIC", null, "Facultad de Ingenieria Civil", true },
                    { 4, "FIM", null, "Facultad de Ingenieria Mecánica", true }
                });

            migrationBuilder.InsertData(
                table: "carrera",
                columns: new[] { "id", "codigo_carrera", "facultad_id", "actualizado_tmstp", "nombre_carrera", "estado" },
                values: new object[,]
                {
                    { 1, "EF", 1, null, "Escuela de Física", true },
                    { 2, "EM", 1, null, "Escuela de Matemática", true },
                    { 3, "EA", 2, null, "Escuela de Arquitectura", true }
                });

            migrationBuilder.CreateIndex(
                name: "IX_carrera_facultad_id",
                table: "carrera",
                column: "facultad_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "carrera");

            migrationBuilder.DropTable(
                name: "facultad");
        }
    }
}
