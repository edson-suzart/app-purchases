﻿using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Swashbuckle.AspNetCore.Annotations;

namespace AppPurchases.Domain.Entities
{
    [BsonIgnoreExtraElements]
    public class AppModel
    {
        [BsonElement("_id")]
        [SwaggerSchema(ReadOnly = true)]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        public string? NameApp { get; set; }

        public decimal PriceApp { get; set; }

        public string? DescriptionApp { get; set; }
    }
}