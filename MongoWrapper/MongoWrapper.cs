using MongoDB.Bson;
using MongoDB.Driver;
using System.Collections.Generic;

namespace csharp_mongo_wrapper
{
    class MongoWrapper
    {
        /// <summary>
        /// Gets reference to a client connection
        /// </summary>
        /// <remarks>
        /// <para>Typically you only create one MongoClient instance for a given cluster and use it across your application.</para>
        /// <para>Creating multiple MongoClients will, however, still share the same pool of connections if and only if the connection strings are identical.</para>
        /// </remarks>
        public MongoClient client { get; private set; }

        /// <summary>
        /// Gets reference to a database 
        /// </summary>        
        public IMongoDatabase database { get; private set; }

        /// <summary>
        /// Gets reference to a collection 
        /// </summary>      
        public IMongoCollection<BsonDocument> collection { get; private set; }

        /// <summary>
        /// Connects to the server using the connection string received.
        /// </summary>
        /// <remarks>
        /// <para>
        /// To get a database, specify the name of the database to the GetDatabase method on client. 
        /// It’s ok if the database doesn’t yet exist. It will be created upon first use.
        /// </para>
        /// <para>
        /// It’s ok if the collection doesn’t yet exist. It will be created upon first use.
        /// The generic parameter TDocument represents the schema that exists in your collection. 
        /// Used a BsonDocument to indicate that we have no pre-defined schema. 
        /// It is possible to use your plain-old-C#-objects (POCOs) as well
        /// </para>
        /// </remarks>
        public void Connect(string connectionString)
        {
            client = new MongoClient(connectionString);

            database = client.GetDatabase("dbname");
            collection = database.GetCollection<BsonDocument>("collectionName");
        }

        /// <summary>
        /// Gets all documents matching the query received.
        /// </summary>
        /// <returns>
        /// Returns a collection of all matching documents that can be iterated using a loop.
        /// </returns>
        public IEnumerable<BsonDocument> FindDocuments(FilterDefinition<BsonDocument> filter)
        {
            return collection.Find(filter).ToCursor().ToEnumerable();
        }

        /// <summary>
        /// Gets all documents matching the query received with the projected fields
        /// </summary>
        /// <returns>
        /// Returns a collection of all matching documents that can be iterated using a loop.
        /// </returns>
        public IEnumerable<BsonDocument> FindDocuments(FilterDefinition<BsonDocument> filter, ProjectionDefinition<BsonDocument> fieldsToIncludeAndExclude = null)
        {
            return collection.Find(filter).Project(fieldsToIncludeAndExclude).ToCursor().ToEnumerable();
        }

        /// <summary>
        /// Gets all documents matching the query and the sort order received.
        /// </summary>
        /// <returns>
        /// Returns a collection of all matching documents that can be iterated using a loop.
        /// </returns>
        public IEnumerable<BsonDocument> FindDocuments(FilterDefinition<BsonDocument> filter, SortDefinition<BsonDocument> sortBy)
        {
            return collection.Find(filter).Sort(sortBy).ToCursor().ToEnumerable();
        }

        /// <summary>
        /// Gets all documents matching the query and the sort order received with the projected fields
        /// </summary>
        /// <returns>
        /// Returns a collection of all matching documents that can be iterated using a loop.
        /// </returns>
        public IEnumerable<BsonDocument> FindDocuments(FilterDefinition<BsonDocument> filter, SortDefinition<BsonDocument> sortBy, ProjectionDefinition<BsonDocument> fieldsToIncludeAndExclude = null)
        {
            return collection.Find(filter).Project(fieldsToIncludeAndExclude).Sort(sortBy).ToCursor().ToEnumerable();
        }

        /// <summary>
        /// Inserts a document
        /// </summary>
        /// <returns>
        /// Returns void.
        /// </returns>
        public void Insert(BsonDocument document)
        {
            collection.InsertOne(document);
        }

        /// <summary>
        /// Inserts many documents
        /// </summary>
        /// <returns>
        /// Returns void.
        /// </returns>
        public void InsertMany(IEnumerable<BsonDocument> documents)
        {
            collection.InsertMany(documents);
        }

        /// <summary>
        /// Gets count of all documents in a collection
        /// </summary>
        /// <returns>
        /// Returns void.
        /// </returns>
        public long Count()
        {
            // The empty BsonDocument parameter to the method is a filter. 
            // In this case, it is an empty filter indicating to count all the documents.
            return collection.CountDocuments(new BsonDocument());
        }
    }
}