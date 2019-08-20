using System;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ksp_portal.Services
{
    public class NoSqlDataAccess : INoSqlDataAccess
    {
        public string ConnectionString { get; set; }
        private IConfiguration _configuration { get; set; }
        private string _connectionString { get; set; }
        private string _collectionName { get; set; }
        private string _databaseName { get; set; }
        public NoSqlDataAccess(IConfiguration config)
        {
            _configuration = config;
            BuildConnectionString();
        }
        public string BuildConnectionString()
        {
            _connectionString = _configuration["ConnectionString"];
            _connectionString = _connectionString.Replace("<<db_username>>", _configuration["db_username"]);
            _connectionString = _connectionString.Replace("<<password>>", _configuration["password"]);
            _connectionString = _connectionString.Replace("<<hostname>>", _configuration["hostname"]);
            return _connectionString;
        }

        public bool Connect()
        {
            throw new NotImplementedException();
        }

        public bool Disconnect()
        {
            throw new NotImplementedException();
        }
    }
    public interface INoSqlDataAccess
    {
        string ConnectionString { get; set; }
        string BuildConnectionString();
        bool Connect();
        bool Disconnect();
    }
}