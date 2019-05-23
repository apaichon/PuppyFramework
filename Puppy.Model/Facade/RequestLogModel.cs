using System;

namespace Puppy.Model.Facade
{
    public class RequestLogModel
    {
        public Guid LogId {get;set;}
        public eLogType  LogType {get {return eLogType.Input;}}
        public string ClassName {get;set;}
        public string MethodName {get;set;}
        public object ExecuteParameter {get;set;}
        public UserAgentModel UserAgent {get;set;}
        
    }

}