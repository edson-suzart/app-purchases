using AppPurchases.Application.Entities;
using AppPurchases.Domain.DTOs;
using AutoMapper;

namespace AppPurchases.Infrastructure.AutoMapper
{
    public class Mappers : Profile
    {
        public Mappers()
        {
            CreateMap<AppDTO, AppModel>();
            CreateMap<CreditCardModel, CreditCardDTO>();
            CreateMap<PurchaseModel, PurchaseDTO>();
            CreateMap<RegisterClientModel, RegisterDTO>();
        }
    }
}
