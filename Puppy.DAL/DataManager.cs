using System;
using Puppy.Model.Data;

namespace Puppy.DAL
{
    public class DataManager
    {
        public static IDataAdapter Build( eDataProvider dataProvider,
                                          string connectionString,
                                          string databaseName,
                                          string tableName
                                        )
        {
            switch (dataProvider)
            {
                default:
                    return new MongoDb(connectionString,
                                        databaseName,
                                        tableName
                                      );
                case eDataProvider.MongoDb:
                    return new MongoDb(connectionString,
                                        databaseName,
                                        tableName
                                      );
            }
        }

        public static IDataAdapter Build( DataConfiguration config)
        {
            switch (config.DataProvider)
            {
                default:
                    return new MongoDb(config.ConnectionString,
                                        config.DatabaseName,
                                        config.TableName
                                      );
                case eDataProvider.MongoDb:
                    return new MongoDb(config.ConnectionString,
                                        config.DatabaseName,
                                        config.TableName
                                      );
            }
        }
    }
}