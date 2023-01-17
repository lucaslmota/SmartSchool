using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SmartSchool.WebAPI.Data;
using SmartSchool.WebAPI.DTO;
using SmartSchool.WebAPI.Models;

namespace SmartSchool.WebAPI.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class AlunoController : ControllerBase
    {
        private readonly IRepository _repository;
        private readonly IMapper _mapper;

        public AlunoController(IRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var alunos = _repository.GetAllAlunos(true);

            return Ok(_mapper.Map<IEnumerable<AlunoDTO>>(alunos));
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var aluno = _repository.GetAllAlunosById(id);

            var alunoDTO = _mapper.Map<AlunoDTO>(aluno);
            return Ok(alunoDTO);
        }

        // [HttpGet("{ByName}")]
        // public IActionResult GetByName(string nome, string sobreNome)
        // {
        //     var aluno = _smartContext.Alunos.FirstOrDefault(x => x.Nome.Contains(nome) && x.Sobrenome.Contains(sobreNome));
        //     if (aluno == null) return BadRequest("O aluno não foi encontrado");

        //     return Ok(aluno);
        // }

        [HttpPost]
        public IActionResult Post(AlunoRegisterDTO alunoDTO)
        {
            var aluno = _mapper.Map<Aluno>(alunoDTO);

            _repository.Add(aluno);
            if (_repository.SaveChanges())
            {
                return Created($"/api/aluno{alunoDTO.Id}", _mapper.Map<AlunoDTO>(aluno));
            }
            return BadRequest("Aluno não encontrado");
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, AlunoRegisterDTO alunoDTO)
        {
            var alunoPut = _repository.GetAllAlunosById(id);
            if (alunoPut == null) return BadRequest("Aluno não foi encontrado");

            _mapper.Map(alunoDTO, alunoPut);

            _repository.Update(alunoPut);
            if (_repository.SaveChanges())
            {
                return Created($"/api/aluno{alunoDTO.Id}", _mapper.Map<AlunoDTO>(alunoPut));
            }

            return BadRequest("Aluno não atualizado");
        }

        [HttpPatch]
        public IActionResult Patch(int id, AlunoRegisterDTO alunoDTO)
        {
            var alunoPatch = _repository.GetAllAlunosById(id);
            if (alunoPatch == null) return BadRequest("Aluno não foi encontrado");

            _mapper.Map(alunoDTO, alunoPatch);

            _repository.Update(alunoPatch);
            if (_repository.SaveChanges())
            {
                return Created($"/api/aluno{alunoDTO.Id}", _mapper.Map<AlunoDTO>(alunoPatch));
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