using AppPurchases.Shared.Enuns;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Swashbuckle.AspNetCore.Annotations;
using System.Text.Json.Serialization;

namespace AppPurchases.Domain.Entities
{
    [BsonIgnoreExtraElements]
    public class PurchaseModel
    {
        [BsonElement("_id")]
        [SwaggerSchema(ReadOnly = true)]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        public string? CpfClient { get; set; }

        [BsonElement("_id")]
        [BsonRepresentation(BsonType.ObjectId)]
        [JsonPropertyName("appId")]
        public string? AppId { get; set; }

        public string? NameApp { get; set; }

        public static DateTime PurchaseDate { get { return DateTime.Now; } }
    }
}
