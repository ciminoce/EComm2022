using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using AutoMapper;
using EComm2022.Entidades.Dtos;
using EComm2022.Entidades.Entidades;
using EComm2022.Tienda.Helpers;
using EComm2022.Tienda.Models.Cliente;
using EComm2022.Tienda.Models.Producto;

namespace EComm2022.Tienda.Mapping
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            LoadClienteMapping();
            LoadProductoMapping();
        }

        private void LoadProductoMapping()
        {
            CreateMap<Producto, ProductoListDto>();
            CreateMap<ProductoListDto, ProductoListVm>().ForMember(dest => dest.base64,
                    opt => opt.MapFrom(src => HelperImagen.ConvertirBase64(src.Imagen)))
                .ForMember(dest => dest.extensionArchivo, opt => opt.MapFrom(src => Path.GetExtension(src.Imagen)));
        }

        private void LoadClienteMapping()
        {
            CreateMap<Cliente, ClienteEditVm>().ReverseMap();
        }
    }
}