using System;
using Puppy.Model.Output;

namespace Puppy.Model.Facade
{
    public class ResponseLogModel
    {
        public Guid LogId {get;set;}
        public eLogType  LogType {get {return eLogType.Output;}}
        public Result Output {get;set;}
        public double Duration {get;set;}
        
    }

}