using AppPurchases.Application.ContractsRepositories;
using AppPurchases.Application.DTOs;
using MongoDB.Driver;

namespace AppPurchases.Infrastructure.Repositories
{
    public class ClientRepository : IClientRepository
    {
        private readonly string collectionName = "registered-customers";
        private readonly string db = "clients";

        private readonly IMongoClient _mongoClient;
        private readonly IMongoDatabase _mongoDatabase;
        private readonly IMongoCollection<RegisterDTO> _mongoCollection;

        public ClientRepository(IMongoClient mongoClient)
        {
            _mongoClient = mongoClient;
            _mongoDatabase = _mongoClient.GetDatabase(db);
            _mongoCollection = _mongoDatabase.GetCollection<RegisterDTO>(collectionName);
        }

        public async Task RegisterNewClient(RegisterDTO register)
        {
            try
            {
                await _mongoCollection.InsertOneAsync(register);
            }
            catch (MongoException ex)
            {
                throw new MongoException(
                    $"Ocorreu um erro ao tentar cadastrar o cliente na base de dados. " +
                    $"CompleteError: {ex.Message}");
            }
        }

        public async Task<RegisterDTO> GetRegisteredClient(string cpf)
        {
            try
            {
                var filters = Builders<RegisterDTO>.Filter.Eq("CpfClient", cpf);
                return await _mongoCollection
                    .Find(filters)
                    .FirstOrDefaultAsync();
            }
            catch (MongoException ex)
            {
                throw new MongoException(
                    $"Ocorreu um erro ao tentar cadastrar um novo cliente. " +
                    $"Error: {ex.Message}");
            }
        }

        public async Task<RegisterDTO> GetRegisteredUser(string username, string password)
        {
            try
            {
                var filters =
                    Builders<RegisterDTO>.Filter.Eq("CpfClient", username) &
                    Builders<RegisterDTO>.Filter.Eq("Password", password);

                return await _mongoCollection
                    .Find(filters)
                    .FirstOrDefaultAsync();
            }
            catch (MongoException ex)
            {
                throw new MongoException(
                    $"Ocorreu um erro ao tentar cadastrar um novo cliente. " +
                    $"Error: {ex.Message}");
            }
        }

        public async Task RegisterNewCreditCardToClient(CreditCardDTO creditCard) 
        {
            try
            {
                var filter = Builders<RegisterDTO>.Filter.Eq("CpfClient", creditCard.CpfClient);
                var update = Builders<RegisterDTO>.Update.PushEach("CreditCard", new List<CreditCardDTO>() { creditCard });
                await _mongoCollection.UpdateOneAsync(filter, update);
            }
            catch (MongoException ex)
            {
                throw new MongoException(
                    $"Ocorreu um erro ao tentar cadastrar um novo cartão de crédito para o cliente. " +
                    $"CompleteError: {ex.Message}");
            }
        }
    }
}
