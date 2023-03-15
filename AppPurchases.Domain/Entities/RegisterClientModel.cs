using AppPurchases.Shared.Enuns;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Swashbuckle.AspNetCore.Annotations;

namespace AppPurchases.Domain.Entities
{
    [BsonIgnoreExtraElements]
    public class RegisterClientModel
    {
        [BsonElement("_id")]
        [SwaggerSchema(ReadOnly = true)]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        public string? NameClient { get; set; }

        public string? CpfClient { get; set; }

        public string UserName { get { return CpfClient!; } }

        public string? Password { get; set; }

        public string? DateBirthClient { get; set; }

        public GenderEnum Gender { get; set; }

        public string? AddressClient { get; set; }

        public static DateTime RegisterDate { get { return DateTime.Now; } }

        public List<CreditCardModel>? CreditCard { get; set; }
    }
}
