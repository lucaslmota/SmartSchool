using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SmartSchool.WebAPI.Models;

namespace SmartSchool.WebAPI.Data
{
    public class Repository : IRepository
    {
        private readonly SmartContext _smartContext;

        public Repository(SmartContext smartContext)
        {
            _smartContext = smartContext;
        }
        public void Add<T>(T entity) where T : class
        {
            _smartContext.Add(entity);
        }
        public void Update<T>(T entity) where T : class
        {
            _smartContext.Update(entity);
        }

        public void Delete<T>(T entity) where T : class
        {
            _smartContext.Remove(entity);
        }

        public bool SaveChanges()
        {
            return (_smartContext.SaveChanges() > 0);
        }

        public Aluno[] GetAllAlunos(bool includeProfessor = false)
        {
            IQueryable<Aluno> query = _smartContext.Alunos;
            if (includeProfessor)
            {
                query = query.Include(a => a.AlunosDisciplinas)
                            .ThenInclude(ad => ad.Disciplina)
                            .ThenInclude(d => d.Professor);
            }

            query = query.AsNoTracking().OrderBy(a => a.Id);
            return query.ToArray();
        }

        public Aluno[] GetAllAlunosByDisciplinaId(int disciplinaId, bool includeProfessor = false)
        {
            IQueryable<Aluno> query = _smartContext.Alunos;
            if (includeProfessor)
            {
                query = query.Include(a => a.AlunosDisciplinas)
                            .ThenInclude(ad => ad.Disciplina)
                            .ThenInclude(d => d.Professor);
            }

            query = query.AsNoTracking()
                         .OrderBy(a => a.Id)
                         .Where(aluno => aluno.AlunosDisciplinas.Any(ad => ad.DisciplinaId == disciplinaId));
            return query.ToArray();
        }

        public Aluno GetAllAlunosById(int alunoId, bool includeProfessor = false)
        {
            IQueryable<Aluno> query = _smartContext.Alunos;

            if (includeProfessor)
            {
                query = query.Include(a => a.AlunosDisciplinas)
                            .ThenInclude(ad => ad.Disciplina)
                            .ThenInclude(d => d.Professor);
            }

            query = query.AsNoTracking()
                         .OrderBy(a => a.Id)
                         .Where(aluno => aluno.Id == alunoId);

            return query.FirstOrDefault();
        }

        public Professor[] GetAllProfessores(bool includeAlunos = false)
        {
            IQueryable<Professor> query = _smartContext.Professores;
            if (includeAlunos)
            {
                query = query.Include(a => a.Disciplinas)
                            .ThenInclude(ad => ad.AlunosDisciplinas)
                            .ThenInclude(d => d.Aluno);
            }

            query = query.AsNoTracking().OrderBy(a => a.Id);
            return query.ToArray();
        }

        public Professor[] GetAllProfessoresByDisciplina(int disciplinaId, bool includeAlunos = false)
        {
            IQueryable<Professor> query = _smartContext.Professores;
            if (includeAlunos)
            {
                query = query.Include(a => a.Disciplinas)
                            .ThenInclude(ad => ad.AlunosDisciplinas)
                            .ThenInclude(d => d.Aluno);
            }


            query = query.AsNoTracking()
                         .OrderBy(a => a.Id)
                         .Where(a => a.Disciplinas.Any(d => d.AlunosDisciplinas.Any(ad => ad.DisciplinaId == disciplinaId)));
            return query.ToArray();
        }

        public Professor GetAllProfessoresById(int professorId, bool includeProfessor = false)
        {
            IQueryable<Professor> query = _smartContext.Professores;
            if (includeProfessor)
            {
                query = query.Include(a => a.Disciplinas)
                            .ThenInclude(ad => ad.AlunosDisciplinas)
                            .ThenInclude(d => d.Aluno);
            }


            query = query.AsNoTracking()
                         .OrderBy(a => a.Id)
                         .Where(professor => professor.Id == professorId);
            return query.FirstOrDefault();
        }
    }
}