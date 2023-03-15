using AppPurchases.Application.ContractsRepositories;
using AppPurchases.Application.DTOs;
using MongoDB.Driver;

namespace AppPurchases.Infrastructure.Repositories
{
    public class PurchaseRepository : IPurchaseRepository
    {
        private readonly string collectionName = "purchases_transactions";
        private readonly string db = "purchases";

        private readonly IMongoClient _mongoClient;
        private readonly IMongoDatabase _mongoDatabase;
        private readonly IMongoCollection<PurchaseDTO> _mongoCollection;

        public PurchaseRepository(IMongoClient mongoClient)
        {
            _mongoClient = mongoClient;
            _mongoDatabase = _mongoClient.GetDatabase(db);
            _mongoCollection = _mongoDatabase.GetCollection<PurchaseDTO>(collectionName);
        }

        public async Task PurchaseApp(PurchaseDTO purchaseDTO)
        {
            try
            {
                await _mongoCollection.InsertOneAsync(purchaseDTO);
            }
            catch (MongoException ex)
            {
                throw new MongoException(
                    $"Ocorreu um erro ao tentar finalizar uma compra para o cliente. " +
                    $"CompleteError: {ex.Message}");
            }
        }
    }
}
