using System;
using System.Collections.Generic;

namespace Puppy.Model.Data
{
    public class DataConfiguration
    {
        public eDataProvider DataProvider
        {get;set;}
        public string ConnectionString
        {get;set;}
        public string DatabaseName
        {get;set;}
        public string TableName
        {get;set;}

        public static eDataProvider GetDataProvider(string envName)
        {
            string envVal = Environment.GetEnvironmentVariable(envName);
            switch (envVal.ToLower())
            { 
                default:
                return eDataProvider.MongoDb;
                case "mongodb":
                return eDataProvider.MongoDb;
            }
        }

        public static string GetConnectionString(string key, Dictionary<string,string> consList)
        {
            return Environment.GetEnvironmentVariable(consList[key]);
        }
    }
}