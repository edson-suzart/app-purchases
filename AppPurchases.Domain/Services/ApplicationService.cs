using AppPurchases.Application.ContractsRepositories;
using AppPurchases.Application.DTOs;
using AppPurchases.Domain.ContractsServices;
using AppPurchases.Domain.Entities;
using AutoMapper;
using CSharpFunctionalExtensions;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;

namespace AppPurchases.Domain.Services
{
    public class ApplicationService : IApplicationService
    {
        private readonly IMapper _mapper;
        private readonly IApplicationRepository _applicationrepository;
        private readonly IDistributedCache _cache;
        private const string CACHE_KEY = "apps";

        public ApplicationService(
            IDistributedCache cache,
            IMapper mapper,
            IApplicationRepository applicationrepository)
        {
            _cache = cache;
            _mapper = mapper;
            _applicationrepository = applicationrepository;
        }

        public async Task<Result<List<AppModel>>> GetAllAppsRegistered()
        {
            var listApps = new List<AppModel>();
            var apps = new List<AppDTO>();

            var json = await _cache.GetStringAsync(CACHE_KEY);
            if (json is not null)
                apps = JsonConvert.DeserializeObject<List<AppDTO>>(json);

            else
            {
                apps = await _applicationrepository.GetAllRegisteredApps();
                if (!apps.Any()) Result.Failure("Não existe nenhum aplicativo cadastrado na base de dados. ");
                await _cache.SetStringAsync(CACHE_KEY, JsonConvert.SerializeObject(apps));
            }

            foreach (var app in apps)
            {
                listApps.Add(_mapper.Map<AppModel>(app));
            }

            return Result.Success(listApps);
        }
    }
}
