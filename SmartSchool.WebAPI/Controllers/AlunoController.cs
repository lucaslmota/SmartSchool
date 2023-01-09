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
        private readonly SmartContext _smartContext;

        public AlunoController(SmartContext smartContext)
        {
            _smartContext = smartContext;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_smartContext.Alunos);
        }

        [HttpGet("byId/{id}")]
        public IActionResult GetById(int id)
        {
            var aluno = _smartContext.Alunos.FirstOrDefault(x => x.Id == id);
            return Ok(aluno);
        }

        [HttpGet("{ByName}")]
        public IActionResult GetByName(string nome, string sobreNome)
        {
            var aluno = _smartContext.Alunos.FirstOrDefault(x => x.Nome.Contains(nome) && x.Sobrenome.Contains(sobreNome));
            if (aluno == null) return BadRequest("O aluno não foi encontrado");

            return Ok(aluno);
        }

        [HttpPost]
        public IActionResult Post(Aluno aluno)
        {
            _smartContext.Add(aluno);
            _smartContext.SaveChanges();
            return Ok(aluno);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, Aluno aluno)
        {
            var alunoPut = _smartContext.Alunos.AsNoTracking().FirstOrDefault(x => x.Id == id);
            if (alunoPut == null) return BadRequest("Aluno não foi encontrado");

            _smartContext.Update(aluno);
            _smartContext.SaveChanges();
            return Ok(aluno);
        }

        [HttpPatch]
        public IActionResult Patch(int id, Aluno aluno)
        {
            var alunoPatch = _smartContext.Alunos.AsNoTracking().FirstOrDefault(x => x.Id == id);
            if (alunoPatch == null) return BadRequest("Aluno não foi encontrado");

            _smartContext.Update(aluno);
            _smartContext.SaveChanges();
            return Ok(aluno);
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var alunoDelete = _smartContext.Alunos.FirstOrDefault(x => x.Id == id);
            if (alunoDelete == null) return BadRequest("Aluno não foi encontrado");

            _smartContext.Remove(alunoDelete);
            _smartContext.SaveChanges();
            return Ok();
        }
    }
}