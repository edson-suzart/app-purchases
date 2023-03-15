using AppPurchases.Shared.Enuns;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using Swashbuckle.AspNetCore.Annotations;
using System.Text.Json.Serialization;
using AppPurchases.Application.DTOs;

namespace AppPurchases.Application.EventEntities
{
    public class PurchaseEvent : EventArgs
    {
        public PurchaseEvent(PurchaseDTO purchaseDTO)
        {
            CpfClient = purchaseDTO.CpfClient!;
            AppId = purchaseDTO.AppId;
            NameApp = purchaseDTO.NameApp;
            PurchaseDate = purchaseDTO.PurchaseDate;
        }

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
