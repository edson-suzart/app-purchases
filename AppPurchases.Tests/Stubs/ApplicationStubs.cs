using AppPurchases.Application.DTOs;
using AppPurchases.Domain.Entities;
using CSharpFunctionalExtensions;

namespace AppPurchases.Tests.Stubs
{
    public class ApplicationStubs
    {
        public AppDTO GetDTO() =>
            new()
            {
                Id = "1",
                DescriptionApp = "Aplicativo teste.",
                NameApp = "Teste",
                PriceApp = 10m
            };

        public Result<List<AppModel>> GetListAppModelService()
        {
            var listModel = new List<AppModel>
            {
                new AppModel
                {
                     Id = "1",
                     DescriptionApp = "Aplicativo teste.",
                     NameApp = "Teste",
                     PriceApp = 10m
                }
            };
            return Result.Success(listModel);
        }
        public List<AppDTO> GetListAppDtoRepository()
        {
            var listModel = new List<AppDTO>
            {
                new AppDTO
                {
                     Id = "1",
                     DescriptionApp = "Aplicativo teste.",
                     NameApp = "Teste",
                     PriceApp = 10m
                }
            };
            return listModel;
        }
    }
}
