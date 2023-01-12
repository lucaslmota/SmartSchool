using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartSchool.WebAPI.Data;
using SmartSchool.WebAPI.Models;

namespace SmartSchool.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AlunoController : ControllerBase
    {
        private readonly IRepository _repository;

        public AlunoController(IRepository repository)
        {
            _repository = repository; ;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var result = _repository.GetAllAlunos(true);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var aluno = _repository.GetAllAlunosById(id);
            return Ok(aluno);
        }

        // [HttpGet("{ByName}")]
        // public IActionResult GetByName(string nome, string sobreNome)
        // {
        //     var aluno = _smartContext.Alunos.FirstOrDefault(x => x.Nome.Contains(nome) && x.Sobrenome.Contains(sobreNome));
        //     if (aluno == null) return BadRequest("O aluno não foi encontrado");

        //     return Ok(aluno);
        // }

        [HttpPost]
        public IActionResult Post(Aluno aluno)
        {
            _repository.Add(aluno);
            if (_repository.SaveChanges())
            {
                return Ok(aluno);
            }
            return BadRequest("Aluno não encontrado");
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, Aluno aluno)
        {
            var alunoPut = _repository.GetAllAlunosById(id);
            if (alunoPut == null) return BadRequest("Aluno não foi encontrado");

            _repository.Update(aluno);
            if (_repository.SaveChanges())
            {
                return Ok(aluno);
            }

            return BadRequest("Aluno não atualizado");
        }

        [HttpPatch]
        public IActionResult Patch(int id, Aluno aluno)
        {
            var alunoPatch = _repository.GetAllAlunosById(id);
            if (alunoPatch == null) return BadRequest("Aluno não foi encontrado");

            _repository.Update(aluno);
            if (_repository.SaveChanges())
            {
                return Ok(aluno);
            }

            return BadRequest("Aluno não atualizado");
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var alunoDelete = _repository.GetAllAlunosById(id);
            if (alunoDelete == null) return BadRequest("Aluno não foi encontrado");

            _repository.Delete(alunoDelete);
            if (_repository.SaveChanges())
            {
                return Ok("Aluno deletado.");
            }

            return BadRequest("Aluno não deletado");
        }
    }
}