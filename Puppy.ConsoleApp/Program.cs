using System;
//using Puppy.DAL;
using Puppy.BLL;
using MyConcert.BLL;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Puppy.ConsoleApp
{
    class Program
    {
        class Book
        {
            public string isbn {get;set;}
            public string title {get;set;}
            public decimal price {get;set;}
        }
        static void Main(string[] args)
        {
            /*MongoDb db = new MongoDb (
                "mongodb://localhost:27017",
                "erp",
                "books"
            );

            var aBook = new Book();
            aBook.isbn = "434-5933-343";
            aBook.title = "Java Script21";
            aBook.price = 19999;
            bool isInserted = false;*/
           // object result ;
            /*db.Open()
            .Add(aBook,out isInserted )
            .Close();
            Console.WriteLine(isInserted);*/

            /*db.Open()
            .Edit("5c639e39c8aee936ab8ea416",aBook, out isInserted,result)
            .Close();
            Console.WriteLine(isInserted + result);
            */
            /* db.Open()
            .Edit("5c639e39c8aee936ab8ea416",aBook, out isInserted)
            .Close();
            Console.WriteLine(isInserted);*/
            //string data = "434-5933-343";
            //Dictionary<string,object> filter = new Dictionary<string,object>();
            //filter.Add("isbn","434-5933-343");
            //Console.WriteLine(JsonConvert.SerializeObject(filter));

            /*db.Open()
            .EditOne(filter,aBook, out isInserted)
            .Close();
            Console.WriteLine(isInserted);
            */

            /*long totalModified = 0;
            db.Open()
            .EditMany("{isbn:'434-5933'}","{'$set':{title:'JavaScript 100'}}", out totalModified)
            .Close();
            Console.WriteLine(totalModified);*/

            /*long totalModified = 0;
            db.Open()
            .DeleteMany("{isbn:'434-5933-343'}", out totalModified)
            .Close();
            Console.WriteLine(totalModified);*/
            
            
            /*string result2 = "";
            db
            .Open()
            //.Get()
            .Get("{price:{$gt:1500}}",out result2)
            .Close();
            Console.WriteLine("Hello World!");
            Console.WriteLine(db.Data);
            Console.WriteLine("2\n" + result2);
            */
            /*BusinessStrategy biz = new BusinessStrategy(
                new IBusinessOperator[] {
                    new AddOperator(),
                    new GetOperator()
                }
            );*/
            //DataConfiguration config, object param, IMessage message
            var aBook = new Book();
            aBook.isbn = "xxx-3434.534";
            aBook.title = "Dotnet Core Book";
            aBook.price = 2500;
            //bool isInserted = false;

            Environment.SetEnvironmentVariable("ENV_MODE", "DEV",EnvironmentVariableTarget.Process);
            Environment.SetEnvironmentVariable("DATABASE_PROVIDER", "mongodb",EnvironmentVariableTarget.Process);
            Environment.SetEnvironmentVariable("CONNECTION_DEV", "mongodb://localhost:27017",EnvironmentVariableTarget.Process);
            Console.WriteLine(Environment.GetEnvironmentVariable("ENV_MODE"));
            Console.WriteLine(Environment.GetEnvironmentVariable("DATABASE_PROVIDER"));
            Console.WriteLine(Environment.GetEnvironmentVariable("CONNECTION_DEV"));
            //DataConfiguration cfg = new DataConfiguration();
             /*Dictionary<string,string> ConnectionStringList = new 
      Dictionary<string,string>
      {
        {"DEV","ConnectionStringDev" }
      };
             Console.WriteLine(ConnectionStringList[Environment.GetEnvironmentVariable("ENV_MODE")]);
             */
            IMessage message = BusinessMessage.CreateMessage(BusinessLocale.th_TH);
            ConcertBLL concert = new ConcertBLL();
            Result result = concert.Add(aBook, message);

             Console.WriteLine(result.Message);
            /*cfg.DataProvider = eDataProvider.MongoDb;
            cfg.ConnectionString = "mongodb://localhost:27017";
            cfg.DatabaseName = "erp";
            cfg.TableName = "books";
            Result result = biz.Execute(
                cfg,
                BusinessOperator.Add,
                aBook,
                message
            );*/

           // Console.WriteLine(result.Message);
            result = concert.Get("{title:'Dotnet Core Book'}", message);
            /*result = biz.Execute(
                cfg,
                BusinessOperator.Get,
                "{title:'Dotnet Core Book'}",
                message
            );*/



            Console.WriteLine(result.Data);

          //IEnumerable<string> m_oEnum = new string[]{};

        }
    }
}
