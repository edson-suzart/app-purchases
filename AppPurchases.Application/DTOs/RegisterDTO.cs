using AppPurchases.Shared.Enuns;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Swashbuckle.AspNetCore.Annotations;

namespace AppPurchases.Application.DTOs
{
    [BsonIgnoreExtraElements]
    public class RegisterDTO
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

        public DateTime RegisterDate { get; set; }

        public List<CreditCardDTO>? CreditCard { get; set; }
    }
}
