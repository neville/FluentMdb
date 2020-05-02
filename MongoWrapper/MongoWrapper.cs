using MongoDB.Bson;
using MongoDB.Driver;
using System.Collections.Generic;

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

        /// <summary>
        /// Gets all documents matching the query received in the filter parameter.
        /// </summary>
        /// <returns>
        /// Returns a collection of all matching documents that can be iterated using a loop.
        /// </returns>
        public IEnumerable<BsonDocument> FindDocuments(FilterDefinition<BsonDocument> filter) {
            return collection.Find(filter).ToCursor().ToEnumerable();
        }

        public void Insert(BsonDocument document)
        {
            collection.InsertOne(document);
        }

        public void InsertMany(IEnumerable<BsonDocument> documents)
        {
            collection.InsertMany(documents);
        }

        public long Count()
        {
            // The empty BsonDocument parameter to the method is a filter. 
            // In this case, it is an empty filter indicating to count all the documents.
            return collection.CountDocuments(new BsonDocument());
        }
    }
}