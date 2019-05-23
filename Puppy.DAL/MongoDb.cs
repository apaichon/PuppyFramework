using System;
using System.Collections.Generic;
using System.Linq;
using MongoDB.Driver;
using MongoDB.Bson;
using Puppy.Model;
using Puppy.Model.Data;

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

        private IClientSessionHandle _session;

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

        public IClientSessionHandle Session
        {
            get { return _session;}
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
            result = _collection.Find(bsonFilter).ToList().ToJson();
            return this;
        }

        public IDataAdapter Count(string filter, out string result)
        {
            var bsonFilter =  BsonDocument.Parse(filter); 
            result = _collection.CountDocuments(bsonFilter, null).ToString();
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

        public IDataAdapter RunCommand(string command, out string result)
        {
            var jsonCommand = new JsonCommand<BsonDocument>(command);
            result = _db.RunCommand(jsonCommand).ToJson();
            return this;
        }

        public IDataAdapter Aggregate(object pipeline, out string result)
        {
           AggregatePipelineModel model = (AggregatePipelineModel)pipeline; 
           BsonDocument Match = BsonDocument.Parse(model.Match);
           BsonDocument Sort = BsonDocument.Parse(model.Sort);
           BsonDocument Group = BsonDocument.Parse(model.Group);
           BsonDocument Project = BsonDocument.Parse(model.Project);
    
            if (model.Lookup == null)
            {
                 result = _collection.Aggregate()
                            .Match(Match)
                            .Group(Group)
                            .Project(Project)
                            .Sort(Sort)
                            .ToList().ToJson();
            }
            else {
                result = _collection.Aggregate()
                            .Match(Match)
                            .Group(Group)
                            .Project(Project)
                            .Lookup(model.Lookup.ForeignCollectionName,
                                model.Lookup.LocalFieldName,
                                model.Lookup.ForeignFieldName,
                                model.Lookup.ResultAs)
                            .Sort(Sort)
                            .ToList().ToJson();
            }

           return this;
        }



        private static BsonDocument ConvertObjectToBson(object data)
        {
            var bson = data.ToBsonDocument();
            bson.Remove("_t");
            return bson;
        }

        public IDataAdapter StartTransaction()
        {
            _session = _client.StartSession();
            return this;
        }

         public IClientSessionHandle CreateTransaction()
        {
            return _client.StartSession();
        }

        public IDataAdapter CommitTransaction()
        {
            _session.CommitTransaction();
            return this;
        }

        public IDataAdapter StopTransaction()
        {
            if (_session != null) 
            {
                if (_session.IsInTransaction)
                {
                    _session.AbortTransaction();
                }
            }
            return this;
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
