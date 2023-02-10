using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SmartSchool.WebAPI.Data;
using SmartSchool.WebAPI.DTO;
using SmartSchool.WebAPI.Helpers;
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
        public async Task<IActionResult> Get([FromQuery] PagesParams pagesParams)
        {
            var alunos = await _repository.GetAllAlunosAsync(pagesParams, true);

            var alunosResult = _mapper.Map<IEnumerable<AlunoDTO>>(alunos);

            Response.AddPagination(alunos.CurrentPage, alunos.PageSize, alunos.TotalCount, alunos.TotalPages);

            return Ok(alunosResult);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var aluno = await _repository.GetAllAlunosByIdAsync(id);

            var alunoDTO = _mapper.Map<AlunoRegisterDTO>(aluno);
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
        public async Task<IActionResult> Put(int id, AlunoRegisterDTO alunoDTO)
        {
            var alunoPut = _repository.GetAllAlunosByIdAsync(id);
            if (alunoPut == null) return BadRequest("Aluno não foi encontrado");

            await _mapper.Map(alunoDTO, alunoPut);

            _repository.Update(alunoPut);
            if (_repository.SaveChanges())
            {
                return Created($"/api/aluno{alunoDTO.Id}", _mapper.Map<AlunoDTO>(alunoPut));
            }

            return BadRequest("Aluno não atualizado");
        }

        [HttpPatch]
        public async Task<IActionResult> Patch(int id, AlunoPatchDTO alunoDTO)
        {
            var alunoPatch = _repository.GetAllAlunosByIdAsync(id);
            if (alunoPatch == null) return BadRequest("Aluno não foi encontrado");

            await _mapper.Map(alunoDTO, alunoPatch);

            _repository.Update(alunoPatch);
            if (_repository.SaveChanges())
            {
                return Created($"/api/aluno{alunoDTO.Id}", _mapper.Map<AlunoDTO>(alunoPatch));
            }

            return BadRequest("Aluno não atualizado");
        }

        // api/aluno/{id}/trocarEstado
        [HttpPatch("{id}/trocarEstado")]
        public async Task<IActionResult> trocarEstado(int id, TrocaEstadoDto trocaEstado)
        {
            var aluno = await _repository.GetAllAlunosByIdAsync(id);
            if (aluno == null) return BadRequest("Aluno não encontrado");

            aluno.Ativo = trocaEstado.Estado;

            _repository.Update(aluno);
            if (_repository.SaveChanges())
            {
                var msn = aluno.Ativo ? "ativado" : "desativado";
                return Ok(new { message = $"Aluno {msn} com sucesso!" });
            }

            return BadRequest("Aluno não Atualizado");
        }


        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var alunoDelete = await _repository.GetAllAlunosByIdAsync(id);
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