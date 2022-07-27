using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace UserModel.UserModel
{
    [BsonCollection("User")]
    [BsonIgnoreExtraElements]                       //If we receive extra fields from MongoDB instruct serializer to ignore them
    public class User : IDocument
    {
        
        public ObjectId Id { get; set; }

        [BsonElement("name")]                       //Map this property to name field in MongoDB document
        public string Name { get; set; } = string.Empty;

        [BsonElement("age")]
        public int Age { get; set; }
    }
}
