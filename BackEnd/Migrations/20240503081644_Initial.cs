using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BackEnd.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NombreUsuario = table.Column<string>(type: "varchar(20)", nullable: false),
                    Password = table.Column<string>(type: "varchar(50)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Cuestionarios",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "varchar(100)", nullable: false),
                    Descripcion = table.Column<string>(type: "varchar(150)", nullable: false),
                    FechaCreacion = table.Column<DateTime>(nullable: false),
                    Activo = table.Column<int>(nullable: false),
                    UsuarioId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cuestionarios", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Cuestionarios_Usuarios_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Preguntas",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descripcion = table.Column<string>(type: "varchar(100)", nullable: false),
                    CuestionarioId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Preguntas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Preguntas_Cuestionarios_CuestionarioId",
                        column: x => x.CuestionarioId,
                        principalTable: "Cuestionarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RespuestasCuestionarios",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NombreParticipante = table.Column<string>(type: "varchar(100)", nullable: false),
                    Fecha = table.Column<DateTime>(nullable: false),
                    Activo = table.Column<int>(nullable: false),
                    CuestionarioId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RespuestasCuestionarios", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RespuestasCuestionarios_Cuestionarios_CuestionarioId",
                        column: x => x.CuestionarioId,
                        principalTable: "Cuestionarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Respuestas",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descripcion = table.Column<string>(type: "varchar(50)", nullable: false),
                    EsCorrecta = table.Column<bool>(nullable: false),
                    PreguntaId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Respuestas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Respuestas_Preguntas_PreguntaId",
                        column: x => x.PreguntaId,
                        principalTable: "Preguntas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RespuestasCuestionariosDetalles",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RespuestaCuestionarioId = table.Column<int>(nullable: false),
                    RespuestaId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RespuestasCuestionariosDetalles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RespuestasCuestionariosDetalles_RespuestasCuestionarios_RespuestaCuestionarioId",
                        column: x => x.RespuestaCuestionarioId,
                        principalTable: "RespuestasCuestionarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RespuestasCuestionariosDetalles_Respuestas_RespuestaId",
                        column: x => x.RespuestaId,
                        principalTable: "Respuestas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cuestionarios_UsuarioId",
                table: "Cuestionarios",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_Preguntas_CuestionarioId",
                table: "Preguntas",
                column: "CuestionarioId");

            migrationBuilder.CreateIndex(
                name: "IX_Respuestas_PreguntaId",
                table: "Respuestas",
                column: "PreguntaId");

            migrationBuilder.CreateIndex(
                name: "IX_RespuestasCuestionarios_CuestionarioId",
                table: "RespuestasCuestionarios",
                column: "CuestionarioId");

            migrationBuilder.CreateIndex(
                name: "IX_RespuestasCuestionariosDetalles_RespuestaCuestionarioId",
                table: "RespuestasCuestionariosDetalles",
                column: "RespuestaCuestionarioId");

            migrationBuilder.CreateIndex(
                name: "IX_RespuestasCuestionariosDetalles_RespuestaId",
                table: "RespuestasCuestionariosDetalles",
                column: "RespuestaId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RespuestasCuestionariosDetalles");

            migrationBuilder.DropTable(
                name: "RespuestasCuestionarios");

            migrationBuilder.DropTable(
                name: "Respuestas");

            migrationBuilder.DropTable(
                name: "Preguntas");

            migrationBuilder.DropTable(
                name: "Cuestionarios");

            migrationBuilder.DropTable(
                name: "Usuarios");
        }
    }
}
