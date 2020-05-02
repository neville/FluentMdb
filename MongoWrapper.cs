using MongoDB.Bson;
using MongoDB.Driver;

namespace csharp_mongo_wrapper
{
    class MongoWrapper
    {
        // Typically you only create one MongoClient instance for a given cluster and use it across your application. 
        // Creating multiple MongoClients will, however, still share the same pool of connections if and only if the connection strings are identical.
        MongoClient client;
        IMongoDatabase database;
        IMongoCollection<BsonDocument> collection;

        public void Connect(string connectionString)
        {
            client = new MongoClient(connectionString);

            // To get a database, specify the name of the database to the GetDatabase method on client. 
            // It’s ok if the database doesn’t yet exist. It will be created upon first use.
            database = client.GetDatabase("dbname");

            // It’s ok if the collection doesn’t yet exist. It will be created upon first use.
            // The generic parameter TDocument represents the schema that exists in your collection. 
            // Used a BsonDocument to indicate that we have no pre-defined schema. 
            // It is possible to use your plain-old-C#-objects (POCOs) as well
            collection = database.GetCollection<BsonDocument>("collectionName");
        }

        public void Insert(BsonDocument document)
        {
            collection.InsertOne(document);
        }
    }
}