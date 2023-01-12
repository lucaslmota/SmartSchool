using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SmartSchool.WebAPI.Data;
using SmartSchool.WebAPI.Models;

namespace SmartSchool.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProfessorController : ControllerBase
    {
        private readonly IRepository _repository;

        public ProfessorController(IRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var result = _repository.GetAllProfessores(true);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var aluno = _repository.GetAllAlunosById(id);
            return Ok(aluno);
        }

        [HttpPost]
        public IActionResult Post(Professor professor)
        {
            _repository.Add(professor);
            if (_repository.SaveChanges())
            {
                return Ok(professor);
            }

            return BadRequest("Professor não add");
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, Professor professor)
        {
            var profePut = _repository.GetAllAlunosById(id);
            if (profePut == null) return BadRequest("Aluno não foi encontrado");

            _repository.Update(professor);
            if (_repository.SaveChanges())
            {
                return Ok(professor);
            }

            return BadRequest("Professor não atualizado");
        }

        [HttpPatch]
        public IActionResult Patch(int id, Professor professor)
        {
            var profePatch = _repository.GetAllAlunosById(id);
            if (profePatch == null) return BadRequest("Aluno não foi encontrado");

            _repository.Update(professor);
            if (_repository.SaveChanges())
            {
                return Ok(professor);
            }

            return BadRequest("Aluno não atualizado");
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var profeDelete = _repository.GetAllAlunosById(id);
            if (profeDelete == null) return BadRequest("Aluno não foi encontrado");

            _repository.Delete(profeDelete);
            if (_repository.SaveChanges())
            {
                return Ok("Aluno deletado.");
            }

            return BadRequest("Aluno não deletado");
        }
    }
}