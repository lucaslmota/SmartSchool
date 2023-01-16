using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartSchool.WebAPI.Models
{
    public class Curso
    {
        public Curso(int id, string nomeCurso)
        {
            Id = id;
            NomeCurso = nomeCurso;
        }

        public int Id { get; set; }
        public string NomeCurso { get; set; }
        public IEnumerable<Disciplina> Disciplinas { get; set; }
    }
}