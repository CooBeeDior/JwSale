using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;

namespace MongodbCore
{
    public interface IMongodbClientFactory
    {
        IMongoClient CreateMongodbClient();
    }

    public class MongodbClientFactory : IMongodbClientFactory
    {
        public IMongoClient CreateMongodbClient()
        {
            return null;
        }
    }
}
