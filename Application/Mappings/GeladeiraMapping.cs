using Application.DTOs;
using AutoMapper;
using Domain;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Application.Mappings
{
    public class GeladeiraMapping : Profile
    {
        public GeladeiraMapping()
        {
            CreateMap<UparGeladeiraDTO, ItemModel>();
        }
    }
}