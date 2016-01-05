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
        public static IMongoDatabase _database;
        //public static IMongoCollection<ReadyStatus> _readyStatus;
        //public static IMongoCollection<Vehicle> _vehicles;
        //public static IMongoCollection<Amissed> _amissed;
        //public static IMongoCollection<Checked> _checked;
        //public static IMongoCollection<Topic> _topic;
        //public static IMongoCollection<Users> _users;

        static MongoUtil()
        {
            var connectionString = WebConfigurationManager.AppSettings["primaryConnectionString"];
            _client = new MongoClient(connectionString);
            var dbName = WebConfigurationManager.AppSettings["databaseName"];
            _database = _client.GetDatabase(dbName);

            //_vehicles =_database.GetCollection<Vehicles>("echecker.Vehicles");
            //_readyStatus = _database.GetCollection<ReadyStatus>("echecker.ReadyStatus");
            //_amissed = _database.GetCollection<Amissed>("echecker.Amissed");
            //_checked = _database.GetCollection<Checked>("echecker.Checked");
            //_topic = _database.GetCollection<Topic>("echecker.Topic");
            //_users = _database.GetCollection<Users>("echecker.Users");

        }

        public static IMongoCollection<T> GetCollection<T>(string tableName)
        {

            return _database.GetCollection<T>(tableName);
        }





    }
}