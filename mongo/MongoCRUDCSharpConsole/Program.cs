using System;
using MongoDB.Driver;
using MongoDB.Bson;
using MongoCRUDCSharpConsole.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MongoCRUDCSharpConsole
{
    class Program
    {
        static void Main(string[] args)
        {

            var MongoUri = "mongodb://chandra:d56_G_paEEjEv_6@ac-9jm9qpr-shard-00-00.aopnkdq.mongodb.net:27017,ac-9jm9qpr-shard-00-01.aopnkdq.mongodb.net:27017,ac-9jm9qpr-shard-00-02.aopnkdq.mongodb.net:27017/?ssl=true&replicaSet=atlas-bav118-shard-0&authSource=admin&retryWrites=true&w=majority";
            var client = new MongoClient(MongoUri);


            MongoCRUD mc = new MongoCRUD(client);
            //mc.Querying();
            //mc.Update();
            //mc.UpdateMany();
            mc.DeleteOne();

            //LINQTest lt = new LINQTest();


            Console.ReadKey();



        }
    }
}

