using AutoMapper;
using SmartSchool.WebAPI.DTO;
using SmartSchool.WebAPI.Models;

namespace SmartSchool.WebAPI.Helpers
{
    public class SmartSchoolProfile : Profile
    {
        public SmartSchoolProfile()
        {
            CreateMap<Aluno, AlunoDTO>()
                     .ForMember(
                        destino => destino.Nome,
                        opt => opt.MapFrom(src => $"{src.Nome} {src.Sobrenome}")
                     ).ForMember(
                        destino => destino.Idade,
                        opt => opt.MapFrom(src => src.DataNasc.GetCurrentAge())
                     );

            CreateMap<AlunoDTO, Aluno>();
            CreateMap<Aluno, AlunoRegisterDTO>().ReverseMap();

            CreateMap<Professor, ProfessorDTO>()
                     .ForMember(
                        detsino => detsino.Nome,
                        opt => opt.MapFrom(src => $"{src.Nome} {src.Sobrenome}")
                     );

            CreateMap<ProfessorDTO, Professor>();
            CreateMap<Professor, ProfessorRegistroDTO>().ReverseMap();
        }
    }
}