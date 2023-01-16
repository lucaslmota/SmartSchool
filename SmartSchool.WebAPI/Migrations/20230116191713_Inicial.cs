using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SmartSchool.WebAPI.Migrations
{
    public partial class Inicial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Alunos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Matricula = table.Column<int>(type: "int", nullable: false),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Sobrenome = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Telefone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DataNasc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataIni = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataFim = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Ativo = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Alunos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Cursos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NomeCurso = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cursos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Professores",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Registro = table.Column<int>(type: "int", nullable: false),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Sobrenome = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Telefone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DataIniMatricula = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataFimMatricula = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Ativo = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Professores", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AlunosCursos",
                columns: table => new
                {
                    AlunoId = table.Column<int>(type: "int", nullable: false),
                    CursoId = table.Column<int>(type: "int", nullable: false),
                    DataInicio = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataFim = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AlunosCursos", x => new { x.AlunoId, x.CursoId });
                    table.ForeignKey(
                        name: "FK_AlunosCursos_Alunos_AlunoId",
                        column: x => x.AlunoId,
                        principalTable: "Alunos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AlunosCursos_Cursos_CursoId",
                        column: x => x.CursoId,
                        principalTable: "Cursos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Disciplina",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CargaHoraria = table.Column<int>(type: "int", nullable: false),
                    PreRequisitoId = table.Column<int>(type: "int", nullable: true),
                    ProfessorId = table.Column<int>(type: "int", nullable: false),
                    CursoId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Disciplina", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Disciplina_Cursos_CursoId",
                        column: x => x.CursoId,
                        principalTable: "Cursos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Disciplina_Disciplina_PreRequisitoId",
                        column: x => x.PreRequisitoId,
                        principalTable: "Disciplina",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Disciplina_Professores_ProfessorId",
                        column: x => x.ProfessorId,
                        principalTable: "Professores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AlunosDisciplinas",
                columns: table => new
                {
                    AlunoId = table.Column<int>(type: "int", nullable: false),
                    DisciplinaId = table.Column<int>(type: "int", nullable: false),
                    DataInicio = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataFim = table.Column<DateTime>(type: "datetime2", nullable: true),
                    NotaDisciAluno = table.Column<float>(type: "real", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AlunosDisciplinas", x => new { x.AlunoId, x.DisciplinaId });
                    table.ForeignKey(
                        name: "FK_AlunosDisciplinas_Alunos_AlunoId",
                        column: x => x.AlunoId,
                        principalTable: "Alunos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AlunosDisciplinas_Disciplina_DisciplinaId",
                        column: x => x.DisciplinaId,
                        principalTable: "Disciplina",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Alunos",
                columns: new[] { "Id", "Ativo", "DataFim", "DataIni", "DataNasc", "Matricula", "Nome", "Sobrenome", "Telefone" },
                values: new object[,]
                {
                    { 1, true, null, new DateTime(2023, 1, 16, 16, 17, 13, 101, DateTimeKind.Local).AddTicks(5639), new DateTime(2005, 5, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "Marta", "Kent", "33225555" },
                    { 2, true, null, new DateTime(2023, 1, 16, 16, 17, 13, 101, DateTimeKind.Local).AddTicks(5646), new DateTime(2005, 5, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, "Paula", "Isabela", "3354288" },
                    { 3, true, null, new DateTime(2023, 1, 16, 16, 17, 13, 101, DateTimeKind.Local).AddTicks(5650), new DateTime(2005, 5, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, "Laura", "Antonia", "55668899" },
                    { 4, true, null, new DateTime(2023, 1, 16, 16, 17, 13, 101, DateTimeKind.Local).AddTicks(5653), new DateTime(2005, 5, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, "Luiza", "Maria", "6565659" },
                    { 5, true, null, new DateTime(2023, 1, 16, 16, 17, 13, 101, DateTimeKind.Local).AddTicks(5656), new DateTime(2005, 5, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), 5, "Lucas", "Machado", "565685415" },
                    { 6, true, null, new DateTime(2023, 1, 16, 16, 17, 13, 101, DateTimeKind.Local).AddTicks(5660), new DateTime(2005, 5, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), 6, "Pedro", "Alvares", "456454545" },
                    { 7, true, null, new DateTime(2023, 1, 16, 16, 17, 13, 101, DateTimeKind.Local).AddTicks(5663), new DateTime(2005, 5, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), 7, "Paulo", "José", "9874512" }
                });

            migrationBuilder.InsertData(
                table: "Cursos",
                columns: new[] { "Id", "NomeCurso" },
                values: new object[,]
                {
                    { 1, "Tecnologia da Informação" },
                    { 2, "Sistemas de Informação" },
                    { 3, "Ciência da Computação" }
                });

            migrationBuilder.InsertData(
                table: "Professores",
                columns: new[] { "Id", "Ativo", "DataFimMatricula", "DataIniMatricula", "Nome", "Registro", "Sobrenome", "Telefone" },
                values: new object[,]
                {
                    { 1, false, null, new DateTime(2023, 1, 16, 16, 17, 13, 101, DateTimeKind.Local).AddTicks(5418), "Lauro", 1, "Oliveira", null },
                    { 2, false, null, new DateTime(2023, 1, 16, 16, 17, 13, 101, DateTimeKind.Local).AddTicks(5429), "Roberto", 2, "Soares", null },
                    { 3, false, null, new DateTime(2023, 1, 16, 16, 17, 13, 101, DateTimeKind.Local).AddTicks(5430), "Ronaldo", 3, "Marconi", null },
                    { 4, false, null, new DateTime(2023, 1, 16, 16, 17, 13, 101, DateTimeKind.Local).AddTicks(5431), "Rodrigo", 4, "Carvalho", null },
                    { 5, false, null, new DateTime(2023, 1, 16, 16, 17, 13, 101, DateTimeKind.Local).AddTicks(5431), "Alexandre", 5, "Montanha", null }
                });

            migrationBuilder.InsertData(
                table: "Disciplina",
                columns: new[] { "Id", "CargaHoraria", "CursoId", "Nome", "PreRequisitoId", "ProfessorId" },
                values: new object[,]
                {
                    { 1, 0, 1, "Matemática", null, 1 },
                    { 2, 0, 3, "Matemática", null, 1 },
                    { 3, 0, 3, "Física", null, 2 },
                    { 4, 0, 1, "Português", null, 3 },
                    { 5, 0, 1, "Inglês", null, 4 },
                    { 6, 0, 2, "Inglês", null, 4 },
                    { 7, 0, 3, "Inglês", null, 4 },
                    { 8, 0, 1, "Programação", null, 5 },
                    { 9, 0, 2, "Programação", null, 5 },
                    { 10, 0, 3, "Programação", null, 5 }
                });

            migrationBuilder.InsertData(
                table: "AlunosDisciplinas",
                columns: new[] { "AlunoId", "DisciplinaId", "DataFim", "DataInicio", "NotaDisciAluno" },
                values: new object[,]
                {
                    { 1, 2, null, new DateTime(2023, 1, 16, 16, 17, 13, 101, DateTimeKind.Local).AddTicks(5681), null },
                    { 1, 4, null, new DateTime(2023, 1, 16, 16, 17, 13, 101, DateTimeKind.Local).AddTicks(5683), null },
                    { 1, 5, null, new DateTime(2023, 1, 16, 16, 17, 13, 101, DateTimeKind.Local).AddTicks(5683), null },
                    { 2, 1, null, new DateTime(2023, 1, 16, 16, 17, 13, 101, DateTimeKind.Local).AddTicks(5684), null },
                    { 2, 2, null, new DateTime(2023, 1, 16, 16, 17, 13, 101, DateTimeKind.Local).AddTicks(5685), null },
                    { 2, 5, null, new DateTime(2023, 1, 16, 16, 17, 13, 101, DateTimeKind.Local).AddTicks(5686), null },
                    { 3, 1, null, new DateTime(2023, 1, 16, 16, 17, 13, 101, DateTimeKind.Local).AddTicks(5687), null },
                    { 3, 2, null, new DateTime(2023, 1, 16, 16, 17, 13, 101, DateTimeKind.Local).AddTicks(5687), null },
                    { 3, 3, null, new DateTime(2023, 1, 16, 16, 17, 13, 101, DateTimeKind.Local).AddTicks(5688), null },
                    { 4, 1, null, new DateTime(2023, 1, 16, 16, 17, 13, 101, DateTimeKind.Local).AddTicks(5689), null },
                    { 4, 4, null, new DateTime(2023, 1, 16, 16, 17, 13, 101, DateTimeKind.Local).AddTicks(5690), null },
                    { 4, 5, null, new DateTime(2023, 1, 16, 16, 17, 13, 101, DateTimeKind.Local).AddTicks(5690), null },
                    { 5, 4, null, new DateTime(2023, 1, 16, 16, 17, 13, 101, DateTimeKind.Local).AddTicks(5691), null },
                    { 5, 5, null, new DateTime(2023, 1, 16, 16, 17, 13, 101, DateTimeKind.Local).AddTicks(5691), null },
                    { 6, 1, null, new DateTime(2023, 1, 16, 16, 17, 13, 101, DateTimeKind.Local).AddTicks(5692), null },
                    { 6, 2, null, new DateTime(2023, 1, 16, 16, 17, 13, 101, DateTimeKind.Local).AddTicks(5693), null },
                    { 6, 3, null, new DateTime(2023, 1, 16, 16, 17, 13, 101, DateTimeKind.Local).AddTicks(5693), null },
                    { 6, 4, null, new DateTime(2023, 1, 16, 16, 17, 13, 101, DateTimeKind.Local).AddTicks(5695), null },
                    { 7, 1, null, new DateTime(2023, 1, 16, 16, 17, 13, 101, DateTimeKind.Local).AddTicks(5695), null },
                    { 7, 2, null, new DateTime(2023, 1, 16, 16, 17, 13, 101, DateTimeKind.Local).AddTicks(5696), null },
                    { 7, 3, null, new DateTime(2023, 1, 16, 16, 17, 13, 101, DateTimeKind.Local).AddTicks(5697), null },
                    { 7, 4, null, new DateTime(2023, 1, 16, 16, 17, 13, 101, DateTimeKind.Local).AddTicks(5697), null },
                    { 7, 5, null, new DateTime(2023, 1, 16, 16, 17, 13, 101, DateTimeKind.Local).AddTicks(5698), null }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AlunosCursos_CursoId",
                table: "AlunosCursos",
                column: "CursoId");

            migrationBuilder.CreateIndex(
                name: "IX_AlunosDisciplinas_DisciplinaId",
                table: "AlunosDisciplinas",
                column: "DisciplinaId");

            migrationBuilder.CreateIndex(
                name: "IX_Disciplina_CursoId",
                table: "Disciplina",
                column: "CursoId");

            migrationBuilder.CreateIndex(
                name: "IX_Disciplina_PreRequisitoId",
                table: "Disciplina",
                column: "PreRequisitoId");

            migrationBuilder.CreateIndex(
                name: "IX_Disciplina_ProfessorId",
                table: "Disciplina",
                column: "ProfessorId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AlunosCursos");

            migrationBuilder.DropTable(
                name: "AlunosDisciplinas");

            migrationBuilder.DropTable(
                name: "Alunos");

            migrationBuilder.DropTable(
                name: "Disciplina");

            migrationBuilder.DropTable(
                name: "Cursos");

            migrationBuilder.DropTable(
                name: "Professores");
        }
    }
}
