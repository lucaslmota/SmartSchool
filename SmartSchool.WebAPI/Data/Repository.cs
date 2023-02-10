using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SmartSchool.WebAPI.Helpers;
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

        public async Task<PageList<Aluno>> GetAllAlunosAsync(PagesParams pagesParams, bool includeProfessor = false)
        {
            IQueryable<Aluno> query = _smartContext.Alunos;
            if (includeProfessor)
            {
                query = query.Include(a => a.AlunosDisciplinas)
                            .ThenInclude(ad => ad.Disciplina)
                            .ThenInclude(d => d.Professor);
            }

            if (!string.IsNullOrEmpty(pagesParams.Nome))
                query = query.Where(aluno => aluno.Nome
                                                  .ToUpper()
                                                  .Contains(pagesParams.Nome.ToUpper()) ||
                                             aluno.Sobrenome
                                                  .ToUpper()
                                                  .Contains(pagesParams.Nome.ToUpper()));
            if (pagesParams.Matricula > 0)
                query = query.Where(aluno => aluno.Matricula == pagesParams.Matricula);

            if (pagesParams.Ativo != null)
                query = query.Where(aluno => aluno.Ativo == (pagesParams.Ativo != 0));
            query = query.AsNoTracking().OrderBy(a => a.Id);
            //return await query.ToArrayAsync();
            return await PageList<Aluno>.CreateAsync(query, pagesParams.PageNumber, pagesParams.PageSize);
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

        public async Task<Aluno> GetAllAlunosByIdAsync(int alunoId, bool includeProfessor = false)
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

            return await query.FirstOrDefaultAsync();
        }

        public async Task<Professor[]> GetAllProfessoresAsync(bool includeAlunos = false)
        {
            IQueryable<Professor> query = _smartContext.Professores;
            if (includeAlunos)
            {
                query = query.Include(a => a.Disciplinas)
                            .ThenInclude(ad => ad.AlunosDisciplinas)
                            .ThenInclude(d => d.Aluno);
            }

            query = query.AsNoTracking().OrderBy(a => a.Id);
            return await query.ToArrayAsync();
        }

        public async Task<Professor[]> GetAllProfessoresByDisciplinaAsync(int disciplinaId, bool includeAlunos = false)
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
            return await query.ToArrayAsync();
        }

        public async Task<Professor> GetAllProfessoresByIdAsync(int professorId, bool includeAlunos = false)
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
                         .Where(professor => professor.Id == professorId);
            return await query.FirstOrDefaultAsync();
        }

        public async Task<Professor[]> GetAllProfessoresByAlunoId(int alunoId, bool includeAlunos = false)
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
                         .Where(aluno => aluno.Disciplinas.Any(
                            x => x.AlunosDisciplinas.Any(ad => ad.AlunoId == alunoId)
                         ));
            return await query.ToArrayAsync();
        }
    }
}