using AutoMapper;
using SGHT.Application.Dtos.RolUsuario;
using SGHT.Application.Dtos.Tarifa;
using SGHT.Application.Dtos.Usuarios;
using SGHT.Application.Dtos.EstadoHabitacion;
using SGHT.Application.Dtos.Piso;
using SGHT.Application.Dtos.RecepcionDto;
using SGHT.Application.Dtos.ClienteDto;
using SGHT.Domain.Entities;
using SGHT.Domain.Entities.Reservation;
using SGHT.Application.Dtos.Categoria;
using SGHT.Application.Dtos.Habitacion;
using SGHT.Application.Dtos.Servicio;
using SGHT.Domain.Entities.Configuration;

namespace SGHT.Application.Mappings
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            //Mappings for Categorias
            CreateMap<Categoria, CategoriaDto>().ReverseMap();
            CreateMap<Categoria, UpdateCategoriaDto>().ReverseMap();
            CreateMap<Categoria, DeleteCategoriaDto>().ReverseMap();
            CreateMap<Categoria, SaveCategoriaDto>().ReverseMap();

            //Mappings for Habitacion
            CreateMap<Habitacion, HabitacionDto>().ReverseMap();
            CreateMap<Habitacion, UpdateHabitacionDto>().ReverseMap();
            CreateMap<Habitacion, DeleteHabitacionDto>().ReverseMap();
            CreateMap<Habitacion, SaveHabitacionDto>().ReverseMap();

            //Mappings for Servicios 
            CreateMap<Servicios, ServiciosDto>().ReverseMap();
            CreateMap<Servicios, UpdateServiciosDto>().ReverseMap();
            CreateMap<Servicios, DeleteServiciosDto>().ReverseMap();
            CreateMap<Servicios, SaveServiciosDto>().ReverseMap();

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

            //Mapping for Recepcion
            CreateMap<Recepcion, RecepcionDto>().ReverseMap();
            CreateMap<Recepcion, UpdateRecepcionDto>().ReverseMap();
            CreateMap<Recepcion, SaveRecepcionDto>().ReverseMap();
            CreateMap<Recepcion, DeleteRecepcionDto>().ReverseMap();

            //Mapping for Cliente
            CreateMap<Cliente, ClienteDto>().ReverseMap();
            CreateMap<Cliente, UpdateClienteDto>().ReverseMap();
            CreateMap<Cliente, SaveClienteDto>().ReverseMap();
            CreateMap<Cliente, DeleteClienteDto>().ReverseMap();

            
            CreateMap<EstadoHabitacion, EstadoHabitacionDto>().ReverseMap();
            CreateMap<EstadoHabitacion, UpdateEstadoHabitacionDto>().ReverseMap();
            CreateMap<EstadoHabitacion, SaveEstadoHabitacionDto>().ReverseMap();
            CreateMap<EstadoHabitacion, DeleteEstadoHabitacionDto>().ReverseMap();

            CreateMap<Piso, PisoDto>().ReverseMap();
            CreateMap<Piso, UpdatePisoDto>().ReverseMap();
            CreateMap<Piso, SavePisoDto>().ReverseMap();
            CreateMap<Piso, DeletePisoDto>().ReverseMap();
        }
    }
} 