using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Swashbuckle.AspNetCore.Annotations;

namespace AppPurchases.Application.DTOs
{
    [BsonIgnoreExtraElements]
    public class ClientRegisteredDTO
    {
        [BsonElement("_id")]
        [SwaggerSchema(ReadOnly = true)]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        public Guid ClientId { get; set; }

        public string? NameClient { get; set; }

        public List<CreditCardDTO>? CreditCard { get; set; }
    }
}
