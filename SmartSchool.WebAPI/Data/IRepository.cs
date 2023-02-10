using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SmartSchool.WebAPI.Helpers;
using SmartSchool.WebAPI.Models;

namespace SmartSchool.WebAPI.Data
{
    public interface IRepository
    {
        void Add<T>(T entity) where T : class;
        void Update<T>(T entity) where T : class;
        void Delete<T>(T entity) where T : class;
        bool SaveChanges();

        //Alunos
        Task<PageList<Aluno>> GetAllAlunosAsync(PagesParams pagesParams, bool includeProfessor = false);
        Aluno[] GetAllAlunosByDisciplinaId(int disciplinaId, bool includeProfessor = false);
        Task<Aluno> GetAllAlunosByIdAsync(int alunoId, bool includeProfessor = false);

        //Professores
        Task<Professor[]> GetAllProfessoresAsync(bool includeAlunos = false);
        Task<Professor[]> GetAllProfessoresByDisciplinaAsync(int disciplinaId, bool includeAlunos = false);
        Task<Professor> GetAllProfessoresByIdAsync(int professorId, bool includeAlunos = false);
        Task<Professor[]> GetAllProfessoresByAlunoId(int alunoId, bool includeAlunos = false);
    }
}