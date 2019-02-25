using System;
using Puppy.Model.Business;
using Puppy.Model.Message;

namespace Puppy.Model.Output
{
    public class Result : IModel
    {
        public BusinessStatus Status
        {get;set;}
        public int StatusCode
        {get;set;}
        public string Message
        {get;set;}
        public string Data
        {get;set;}

        public static Result GetResult(BusinessStatus status, int msgCode, IMessage message, string data)
        {
            Result result = new Result();
            result.Status = status;
            result.StatusCode = msgCode;
            result.Data = data;
            result.Message = Result.GetMessage(msgCode, message);

            return result;
        }

        public static string GetMessage(int msgCode, IMessage message)
        {
            return message.GetMessage(msgCode);
        }
        
    }
}