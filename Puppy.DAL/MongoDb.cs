using System;
using System.Collections.Generic;
using System.Linq;
using MongoDB.Driver;
using MongoDB.Bson;

namespace Puppy.DAL
{
    public class MongoDb: IDataAdapter
    {
        #region Members
        MongoClient _client;
        IMongoDatabase _db; 
        IMongoCollection<BsonDocument> _collection;
        // Track whether Dispose has been called.
        private bool disposed = false;

        [System.ComponentModel.DefaultValue(false)]
        public bool IsOpened
        {
            get;set;
        }

        [System.ComponentModel.DefaultValue("")]
        public string ConnectionString
        {
            get;set;
        }

        [System.ComponentModel.DefaultValue(15)]
        public int ConnectionTimeout
        {
            get;set;
        }

        [System.ComponentModel.DefaultValue(15)]
        public int ExecutionTimeout
        {
            get;set;
        }

        [System.ComponentModel.DefaultValue("")]
        public string DatabaseName
        {
            get;set;
        }

        [System.ComponentModel.DefaultValue("")]
        public string TableName
        {
            get;set;
        }

        public string Data
        {
            get;set;
        }

        #endregion

        public MongoDb()
        {

        }

        public MongoDb(string connectionString, string databaseName, string tableName)
        {
            this.ConnectionString = connectionString;
            this.DatabaseName = databaseName;
            this.TableName = tableName;
            _client = new MongoClient(this.ConnectionString);

        }

        public IDataAdapter Open()
        {
            _db = _client.GetDatabase(this.DatabaseName);
            _collection = _db.GetCollection<BsonDocument>(this.TableName);
            this.IsOpened = true;
            return this;
        }

        public IDataAdapter Close()
        {
            _collection = null;
            _db = null;
            _client = null;
            IsOpened = false;
            return this;
        }

        public IDataAdapter Get()
        {
            this.Data = _collection.Find(data => true).ToList().ToJson();
            return this;
        }

        public IDataAdapter Get(out string json)
        {
            json = _collection.Find(data => true).ToList().ToJson();
            return this;
        }

        public IDataAdapter Get(string filter, out string result)
        {
            var bsonFilter =  BsonDocument.Parse(filter); 
            result = _collection.Find(filter).ToList().ToJson();
            return this;
        }

        public IDataAdapter Add(object data, out bool isInserted)
        {
            var bson = ConvertObjectToBson(data);
            _collection.InsertOne(bson);
            isInserted = true;
            return this;
        }

        public IDataAdapter Edit(string filter, object data, out bool isUpdated)
        {
            var doc =  BsonDocument.Parse(filter); 
            var bson = ConvertObjectToBson(data);
            isUpdated = (_collection.FindOneAndUpdate(doc, bson ) != null? true:false);
            return this;
        }

        public IDataAdapter EditOne(Dictionary<string,object> filter, object data, out bool isUpdated)
        {
            var bsonFilter  = filter.ToBsonDocument();
            var bson = ConvertObjectToBson(data);
            isUpdated = (_collection.FindOneAndUpdate(bsonFilter, bson ) != null? true:false);
            return this;
        }

        public IDataAdapter EditMany(string filter, string data, out long totalModified)
        {
            totalModified = 0;
            var bsonFilter  = BsonDocument.Parse(filter);
            var bson = BsonDocument.Parse(data);
            
            UpdateResult result = _collection.UpdateMany(bsonFilter, bson ) ;
            if(result.IsAcknowledged)
            {
                totalModified = result.ModifiedCount;
            }
            return this;
        }

        public IDataAdapter DeleteMany(string filter, out long totalDeleted)
        {
            totalDeleted = 0;
            var bsonFilter  = BsonDocument.Parse(filter);
            DeleteResult result = _collection.DeleteMany(bsonFilter);
            if(result.IsAcknowledged)
            {
                totalDeleted = result.DeletedCount;
            }
            return this;
        }

        private BsonDocument ConvertObjectToBson(object data)
        {
            var bson = data.ToBsonDocument();
            bson.Remove("_t");
            return bson;
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            // Check to see if Dispose has already been called.
            if(!this.disposed)
            {
                // If disposing equals true, dispose all managed
                // and unmanaged resources.
                if(disposing)
                {
                    // Dispose managed resources.
                    _client = null;
                    _db = null; 
                    _collection = null;
                }

                // Note disposing has been done.
                disposed = true;

            }
        }

         ~MongoDb()
        {
            Dispose(false);
        }

    }
}
