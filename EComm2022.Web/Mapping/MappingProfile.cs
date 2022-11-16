using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using EComm2022.Entidades.Dtos;
using EComm2022.Entidades.Dtos.Usuario;
using EComm2022.Entidades.Entidades;
using EComm2022.Web.Models.Categoria;
using EComm2022.Web.Models.Marca;
using EComm2022.Web.Models.Producto;
using EComm2022.Web.Models.Usuario;

namespace EComm2022.Web.Mapping
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            LoadCategoriaMapping();
            LoadMarcaMapping();
            LoadProductoMapping();
            LoadUsuarioMapping();
        }

        private void LoadUsuarioMapping()
        {
            CreateMap<UsuarioListDto, UsuarioListVm>()
                .ForMember(dest => dest.Rol, opt => opt.MapFrom(src => $"{((Rol)src.RolId).ToString()}"));
            CreateMap<UsuarioEditVm, Usuario>();
        }

        private void LoadProductoMapping()
        {
            CreateMap<ProductoListDto, ProductoListVm>();
            CreateMap<Producto, ProductoEditVm>().ReverseMap();
            CreateMap<Producto, ProductoListVm>()
                .ForMember(dest => dest.Categoria, opt => opt.MapFrom(src => src.Categoria.Descripcion))
                .ForMember(dest => dest.Marca, opt => opt.MapFrom(src => src.Marca.NombreMarca));
        }

        private void LoadMarcaMapping()
        {
            CreateMap<Marca, MarcaListVm>();
            CreateMap<Marca, MarcaEditVm>().ReverseMap();

        }

        private void LoadCategoriaMapping()
        {
            CreateMap<Categoria, CategoriaEditVm>().ReverseMap();

        }
    }
}