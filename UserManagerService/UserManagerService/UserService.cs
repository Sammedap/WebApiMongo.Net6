using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserModel.UserModel;

namespace UserManagerService.UserManagerService
{
    public class UserService<TDocument> : IUserService<TDocument> where TDocument : IDocument
    {
        private readonly IMongoCollection<TDocument> _collection;

        public UserService(IUserStoreDatabaseSettings settings, IMongoClient mongoClient)
        {
            var database = mongoClient.GetDatabase(settings.DatabaseName);
            _collection = database.GetCollection<TDocument>(settings.UserCollectionName);
           // var database = new MongoClient(settings.ConnectionString).GetDatabase(settings.DatabaseName);
            //_users = database.GetCollection<TDocument>(GetCollectionName(typeof(TDocument)));
        }

        public virtual TDocument Create(TDocument document)
        {
            _collection.InsertOne(document);
            return document;
        }
        public IQueryable<TDocument> Get()
        {
            //List<User> lstUser = _users.Find(user => true).ToList();
            return _collection.AsQueryable();
        }

        public virtual TDocument Get(string Id)
        {
            //User objUser = _users.Find(user => user.Id == Id).FirstOrDefault();

            var objectId = new ObjectId(Id);
            var filter = Builders<TDocument>.Filter.Eq(x => x.Id, objectId);

            return _collection.Find(filter).SingleOrDefault();
        }

        public void Remove(string Id)
        {
            //_users.DeleteOne(users => users.Id == Id);
            var objectId = new ObjectId(Id);
            var filter = Builders<TDocument>.Filter.Eq(doc => doc.Id, objectId);
            _collection.FindOneAndDelete(filter);
        }

        public void Update(string Id, TDocument document)
        {
            var objectId = new ObjectId(Id);
            //_collection.ReplaceOne(user => user.Id == objectId, document);
            document.Id = objectId;
            var filter = Builders<TDocument>.Filter.Eq(doc => doc.Id, document.Id);
            _collection.FindOneAndReplace(filter, document);
        }
    }
}
