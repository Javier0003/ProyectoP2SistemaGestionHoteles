using AutoMapper;
using SGHT.Application.Dtos.RolUsuario;
using SGHT.Application.Dtos.Tarifa;
using SGHT.Application.Dtos.Usuarios;
using SGHT.Domain.Entities;

namespace SGHT.Application.Mappings
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            // Mappings for Usuarios
            CreateMap<Usuarios, UsuarioDto>().ReverseMap();
            CreateMap<Usuarios, UpdateUsuarioDto>().ReverseMap();
            CreateMap<Usuarios, DeleteUsuarioDto>().ReverseMap();
            CreateMap<Usuarios, SaveUsuarioDto>().ReverseMap();

            // Mappings for RolUsuario
            CreateMap<RolUsuario, RolUsuarioDto>().ReverseMap();
            CreateMap<RolUsuario, UpdateRolUsuarioDto>().ReverseMap();
            CreateMap<RolUsuario, DeleteRolUsuarioDto>().ReverseMap();
            CreateMap<RolUsuario, SaveRolUsuarioDto>().ReverseMap();

            // Mappings for Tarifa
            CreateMap<Tarifas, TarifaDto>().ReverseMap();
            CreateMap<Tarifas, UpdateTarifaDto>().ReverseMap();
            CreateMap<Tarifas, SaveTarifaDto>().ReverseMap();
            CreateMap<Tarifas, DeleteTarifaDto>().ReverseMap();
        }
    }
} 