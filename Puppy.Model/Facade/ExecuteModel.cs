using System;
using Puppy.Model.Message;

namespace Puppy.Model.Facade
{
    public class ExecuteModel : IModel
    {
        public string FileName { get; set; }
        public string ClassName { get; set; }
        public string MethodName { get; set; }
        public object InitParameter { get; set; }
        public object ExecuteParameter {get; set;}
        public string UaString {get;set;}
        public IMessage Message {get; set;}

    }
}