using MongoDB.Driver;
using System.Configuration;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace MongoDB_Application
{
    public class ProductInfo
    {
        private string connectionString;
        private string databaseName;
        private string collectionName;
        public ProductInfo()
        {
            connectionString = ConfigurationManager.AppSettings["ConnectionString"];
            databaseName = ConfigurationManager.AppSettings["DatabaseName"];
            collectionName = ConfigurationManager.AppSettings["CollectionName"];
        }

        public ProductModel GetProductByID(int productID)
        {
            MongoClient client = new MongoClient(connectionString);
            IMongoDatabase database = client.GetDatabase(databaseName);
            IMongoCollection<ProductModel> productCollection
                = database.GetCollection<ProductModel>(collectionName);
            var objResult = productCollection.Find<ProductModel>(x => x.ProductID == productID).FirstOrDefault();
            return objResult;
            //return objResult.First<ProductModel>();
        }

        public bool UpdateProduct(ProductModel objModel)
        {
            MongoClient client = new MongoClient(connectionString);
            IMongoDatabase database = client.GetDatabase(databaseName);
            IMongoCollection<ProductModel> productCollection
                = database.GetCollection<ProductModel>(collectionName);
            var filter = Builders<ProductModel>.Filter.Eq("ProductID", objModel.ProductID);
            var update = Builders<ProductModel>.Update
                .Set("ProductName", objModel.ProductName)
                .Set("ProductID", objModel.ProductID)
                .Set("ProductDescription", objModel.ProductDescription)
                .Set("ProductPrice", objModel.ProductPrice);
            productCollection.UpdateOne(filter, update);
            return true;
        }

        public List<ProductModel> GetAllProducts()
        {
            MongoClient client = new MongoClient(connectionString);
            IMongoDatabase database = client.GetDatabase(databaseName);
            IMongoCollection<ProductModel> productCollection
                = database.GetCollection<ProductModel>(collectionName);
            var objResult = productCollection.Find(x => x.ProductPrice > 0).ToList();
            return objResult;
        }

        public bool Add(ProductModel obj)
        {
            MongoClient client = new MongoClient(connectionString);
            IMongoDatabase database = client.GetDatabase(databaseName);
            IMongoCollection<ProductModel> productCollection
                = database.GetCollection<ProductModel>(collectionName);

            productCollection.InsertOne(obj);
            return true;
        }
    }
}