using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Swashbuckle.AspNetCore.Annotations;

namespace AppPurchases.Domain.Entities
{
    [BsonIgnoreExtraElements]
    public class ClientRegisteredModel
    {
        [BsonElement("_id")]
        [SwaggerSchema(ReadOnly = true)]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        [BsonElement("_id")]
        [BsonId]
        public string? ClientId { get; set; }

        public string? NameClient { get; set; }

        public List<CreditCardModel>? CreditCardModel { get; set; }
    }
}
