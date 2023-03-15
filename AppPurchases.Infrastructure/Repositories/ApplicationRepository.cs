using AppPurchases.Application.ContractsRepositories;
using AppPurchases.Application.DTOs;
using MongoDB.Bson;
using MongoDB.Driver;

namespace AppPurchases.Infrastructure.Repositories
{
    public class ApplicationRepository : IApplicationRepository
    {
        private readonly string collectionName = "apps";
        private readonly string db = "products";

        private readonly IMongoClient _mongoClient;
        private readonly IMongoDatabase _mongoDatabase;
        private readonly IMongoCollection<AppDTO> _mongoCollection;

        public ApplicationRepository(IMongoClient mongoClient)
        {
            _mongoClient = mongoClient;
            _mongoDatabase = _mongoClient.GetDatabase(db);
            _mongoCollection = _mongoDatabase.GetCollection<AppDTO>(collectionName);
        }

        public async Task<List<AppDTO>> GetAllRegisteredApps()
        {
            try
            {
                if (await CollectionExistsAsync(_mongoDatabase, collectionName))
                    return await _mongoCollection.Find(_ => true).ToListAsync();
                else
                    await InsertApps();

                return await _mongoCollection.Find(_ => true).ToListAsync();
            }
            catch (MongoException ex)
            {
                throw new MongoException(
                    $"Ocorreu um erro ao tentar buscar os aplicativos na base de dados. " +
                    $"CompleteError: {ex.Message}");
            }
        }

        public async Task<AppDTO> GetRegisteredApp(string? appId)
        {
            try
            {
                var filter = Builders<AppDTO>.Filter.Eq("_id", ObjectId.Parse(appId));
                return await _mongoCollection.Find(filter).FirstOrDefaultAsync();
            }
            catch (MongoException ex)
            {
                throw new MongoException(
                    $"Ocorreu um erro ao tentar buscar o aplicativo na base de dados. " +
                    $"CompleteError: {ex.Message}");
            }
        }

        /// <summary>
        /// Fluxo criado somente para popular a collections de apps na primeira vez executada
        /// </summary>
        /// <param name="appId"></param>
        /// <returns></returns>
        /// <exception cref="MongoException"></exception>
        private async Task InsertApps()
        {
            await _mongoCollection.InsertManyAsync(new List<AppDTO>
            {
                new AppDTO
                {
                    DescriptionApp = "Aplicativo para assistir vídeos.",
                    NameApp = "Youtube",
                    PriceApp = 10m
                },
                new AppDTO
                {
                    DescriptionApp = "Aplicativo para conversar online.",
                    NameApp = "Whatsapp",
                    PriceApp = 100m
                }
            });
        }

        private async Task<bool> CollectionExistsAsync(IMongoDatabase db, string collectionName)
        {
            var filter = new BsonDocument("name", collectionName);
            var collection = await db.ListCollectionsAsync(new ListCollectionsOptions { Filter = filter });
            return collection.Any();
        }
    }
}
