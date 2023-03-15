using AppPurchases.Shared.Enuns;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Swashbuckle.AspNetCore.Annotations;
using System.Text.Json.Serialization;

namespace AppPurchases.Application.DTOs
{
    [BsonIgnoreExtraElements]
    public class PurchaseDTO
    {
        [BsonElement("_id")]
        [SwaggerSchema(ReadOnly = true)]
        [JsonIgnore]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        public string? CpfClient { get; set; }

        [BsonRepresentation(BsonType.ObjectId)]
        [JsonPropertyName("AppId")]
        public string? AppId { get; set; }

        public string? NameApp { get; set; }

        public DateTime PurchaseDate { get; set; }
    }
}