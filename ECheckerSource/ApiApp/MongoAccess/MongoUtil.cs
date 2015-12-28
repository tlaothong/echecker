using ApiApp.Models;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Configuration;
namespace ApiApp.MongoAccess
{
    static class MongoUtil
    {
        private static IMongoClient _client;
        private static IMongoDatabase _database;
        private static MongoDbHelper<ReadyStatus> _readyStatus;
    
        static MongoUtil()
        {
            var connectionString = WebConfigurationManager.AppSettings["primaryConnectionString"];
            _client = new MongoClient(connectionString);
            var dbName = WebConfigurationManager.AppSettings["databaseName"];
            _database = _client.GetDatabase(dbName);
            _readyStatus = new MongoDbHelper<ReadyStatus>(_database, "echecker.ReadyStatus");            
        }       


    }
}