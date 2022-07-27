using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserModel.UserModel
{
    public interface IDocument
    {
        [BsonId]                                    //Map this field to Id field on MongoDB
        [BsonRepresentation(BsonType.String)]     //Automatically converts Mongo datatype into dotnet datatype and viceversa.
        ObjectId Id { get; set; }
    }
}
