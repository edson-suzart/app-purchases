using AppPurchases.Shared.Enuns;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Swashbuckle.AspNetCore.Annotations;

namespace AppPurchases.Application.DTOs
{
    [BsonIgnoreExtraElements]
    public class CreditCardDTO
    {
        [BsonElement("_id")]
        [SwaggerSchema(ReadOnly = true)]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        public string? NumberCard { get; set; }

        public string? Validate { get; set; }

        public string? CpfClient { get; set; }

        public string? Flag { get; set; }

        public int SecutiryCode { get; set; }

        public CreditCardEnum CreditCardType { get; set; }

        public decimal CreditLimit { get { return 100m; } }
    }
}
