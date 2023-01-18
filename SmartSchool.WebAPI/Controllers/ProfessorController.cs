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
        public async Task<IActionResult> Get()
        {
            var professor = await _repository.GetAllProfessoresAsync(true);
            return Ok(_mapper.Map<ProfessorDTO>(professor));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var professor = await _repository.GetAllProfessoresByIdAsync(id);
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
        public async Task<IActionResult> Put(int id, ProfessorRegistroDTO professorRegistroDTO)
        {
            var profePut = _repository.GetAllProfessoresByIdAsync(id);
            if (profePut == null) return BadRequest("Aluno não foi encontrado");

            await _mapper.Map(professorRegistroDTO, profePut);

            _repository.Update(profePut);
            if (_repository.SaveChanges())
            {
                return Created($"/api/professor{professorRegistroDTO.Id}", _mapper.Map<ProfessorRegistroDTO>(profePut));
            }

            return BadRequest("Professor não atualizado");
        }

        [HttpPatch]
        public async Task<IActionResult> Patch(int id, ProfessorRegistroDTO professorRegistroDTO)
        {
            var profePatch = _repository.GetAllProfessoresByIdAsync(id);
            if (profePatch == null) return BadRequest("Aluno não foi encontrado");

            await _mapper.Map(professorRegistroDTO, profePatch);

            _repository.Update(profePatch);
            if (_repository.SaveChanges())
            {
                return Created($"/api/professor{professorRegistroDTO.Id}", _mapper.Map<ProfessorRegistroDTO>(profePatch));
            }

            return BadRequest("Aluno não atualizado");
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var profeDelete = await _repository.GetAllProfessoresByIdAsync(id);
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