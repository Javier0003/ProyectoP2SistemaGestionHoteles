using AutoMapper;
using SGHT.Application.Dtos.Usuarios;
using SGHT.Domain.Entities;

namespace SGHT.Application.Mappings
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Usuarios, UsuarioDto>();
            CreateMap<Usuarios, UpdateUsuarioDto>();
            CreateMap<Usuarios, DeleteUsuarioDto>();
            CreateMap<Usuarios, SaveUsuarioDto>();
        }
    }
} 