using AppPurchases.Application.DTOs;
using AppPurchases.Domain.Entities;
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
