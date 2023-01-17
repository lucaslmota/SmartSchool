using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SmartSchool.WebAPI.Data;
using SmartSchool.WebAPI.DTO;
using SmartSchool.WebAPI.Models;

namespace SmartSchool.WebAPI.Controllers
{
    [ApiController]
    [ApiVersion("2.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class ProfessorController : ControllerBase
    {
        private readonly IRepository _repository;
        private readonly IMapper _mapper;

        public ProfessorController(IRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var professor = _repository.GetAllProfessores(true);
            return Ok(_mapper.Map<ProfessorDTO>(professor));
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var professor = _repository.GetAllProfessoresById(id);
            var professorDTO = _mapper.Map<ProfessorDTO>(professor);
            return Ok(professorDTO);
        }

        [HttpPost]
        public IActionResult Post(ProfessorRegistroDTO professorRegistroDTO)
        {
            var professor = _mapper.Map<Professor>(professorRegistroDTO);

            _repository.Add(professor);
            if (_repository.SaveChanges())
            {
                return Created($"/api/professor{professorRegistroDTO.Id}", _mapper.Map<ProfessorRegistroDTO>(professor));
            }

            return BadRequest("Professor não add");
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, ProfessorRegistroDTO professorRegistroDTO)
        {
            var profePut = _repository.GetAllAlunosById(id);
            if (profePut == null) return BadRequest("Aluno não foi encontrado");

            _mapper.Map(professorRegistroDTO, profePut);

            _repository.Update(profePut);
            if (_repository.SaveChanges())
            {
                return Created($"/api/professor{professorRegistroDTO.Id}", _mapper.Map<ProfessorRegistroDTO>(profePut));
            }

            return BadRequest("Professor não atualizado");
        }

        [HttpPatch]
        public IActionResult Patch(int id, ProfessorRegistroDTO professorRegistroDTO)
        {
            var profePatch = _repository.GetAllAlunosById(id);
            if (profePatch == null) return BadRequest("Aluno não foi encontrado");

            _mapper.Map(professorRegistroDTO, profePatch);

            _repository.Update(profePatch);
            if (_repository.SaveChanges())
            {
                return Created($"/api/professor{professorRegistroDTO.Id}", _mapper.Map<ProfessorRegistroDTO>(profePatch));
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