using System;
using System.IO;
using System.Text.RegularExpressions;
using Puppy.DAL;
using Puppy.BLL;
using Puppy.Facade;
using Puppy.Model;
using Puppy.Model.Data;
using Puppy.Model.Facade;
using Puppy.Model.Output;
using Puppy.Utils;
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

        //static LogUtil _logger;
        static void Main(string[] args)
        {
            //InitLog();
            //InfoLog("Start to Execute");
            TestFacade();
            //string uaString = "Mozilla/5.0 (iPhone; CPU iPhone OS 5_1_1 like Mac OS X) AppleWebKit/534.46 (KHTML, like Gecko) Version/5.1 Mobile/9B206 Safari/7534.48.3";
            //UserAgent ua = new UserAgent();
            //IModel model = UserAgent.Parse(uaString);
            //Console.WriteLine(JsonConvert.SerializeObject(model));
            Console.WriteLine("Please enter for exit.");
            Console.ReadKey();
        }

        private static void TestFacade()
        {
            LogUtil logger = new LogUtil (GetApplicationRoot() + "\\logs\\myapp.txt");
            RestApiFacade facade = new RestApiFacade(logger);
    
            var aBook = new Book();
            aBook.isbn = "434-5933-343";
            aBook.title = "Java Script999";
            aBook.price = 999;
            Puppy.Model.Message.IMessage message = BusinessMessage.CreateMessage(Puppy.Model.Business.BusinessLocale.th_TH);

            ExecuteModel model = new ExecuteModel {
                FileName ="D:\\Courses\\AspnetCore\\MyConcert\\MyConcert.BLL\\bin\\Debug\\netstandard2.0\\MyConcert.BLL.dll",
                ClassName ="MyConcert.BLL.ConcertBLL",
                MethodName ="Add",
                InitParameter = config,
                ExecuteParameter = aBook,
                Message = message,
                UaString ="Mozilla/5.0 (iPhone; CPU iPhone OS 5_1_1 like Mac OS X) AppleWebKit/534.46 (KHTML, like Gecko) Version/5.1 Mobile/9B206 Safari/7534.48.3"
            };
           Result result = (Result)facade.ExecutionFlow(model);
           //InfoLog(result.Message);

        }

       /*  private static void InitLog()
        {
            _logger =new LogUtil ( GetApplicationRoot() + "\\logs\\myapp.txt");
        }
        */

    /*  private static void InfoLog(string content)
        {
           _logger.Info(content);
        }
        */

    
public static string GetApplicationRoot()
{
 var exePath =   Path.GetDirectoryName(System.Reflection
                   .Assembly.GetExecutingAssembly().CodeBase);
 Regex appPathMatcher=new Regex(@"(?<!fil)[A-Za-z]:\\+[\S\s]*?(?=\\+bin)");
 var appRoot = appPathMatcher.Match(exePath).Value;
 return appRoot;
}

        
    }
}
