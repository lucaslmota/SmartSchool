using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartSchool.WebAPI.DTO
{
    public class DisciplinaDto
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public int CargaHoraria { get; set; }
        public int? PrerequisitoId { get; set; } = null;
        public DisciplinaDto Prerequisito { get; set; }
        public int ProfessorId { get; set; }
        public ProfessorDTO Professor { get; set; }
        public int CursoId { get; set; }
        public CursoDto Curso { get; set; }
        public IEnumerable<AlunoDTO> Alunos { get; set; }
    }
}