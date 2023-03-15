using AppPurchases.Shared.Enuns;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Text.Json.Serialization;

namespace AppPurchases.Function.Entities
{
    public class PurchaseMessageModel
    {
        [BsonElement("_id")]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        public string CpfClient { get; set; }

        [BsonRepresentation(BsonType.ObjectId)]
        [JsonPropertyName("AppId")]
        public string AppId { get; set; }

        public string NameApp { get; set; }

        public OrderEnum OrderStatus { get; set; }

        public DateTime PurchaseDate { get; set; }
    }
}
