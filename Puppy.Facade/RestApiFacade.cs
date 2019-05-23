using System;
using System.Reflection;
using Puppy.Model.Facade;
using Puppy.Model.Output;
using Puppy.Model.Business;
using Puppy.Utils;
using Newtonsoft.Json;

namespace Puppy.Facade
{
    public class RestApiFacade
    {
        Guid _id;
        LogUtil _logger;

        ExecuteModel _executeModel;
        RequestLogModel _reqLog;
        ResponseLogModel _resLog;
        DateTime _start;
        DateTime _end;
        Result _result;

        public RestApiFacade(LogUtil logger)
        {
            _logger = logger;
            _id = Guid.NewGuid();
        }

        public RestApiFacade WriteReqLog()
        {
            this._reqLog = new RequestLogModel 
            {
                LogId = _id,
                ClassName = this._executeModel.ClassName,
                MethodName = this._executeModel.MethodName,
                ExecuteParameter = this._executeModel.ExecuteParameter,

            };

            if (!String.IsNullOrEmpty(this._executeModel.UaString))
            {
                this._reqLog.UserAgent = (UserAgentModel)UserAgent.Parse(this._executeModel.UaString);   
            }

            this._logger.Info(JsonConvert.SerializeObject(this._reqLog));
            
            return this;
        }

        public RestApiFacade WriteResLog()
        {
            this._resLog = new ResponseLogModel 
            {
                LogId = _id,
                Duration =  this._end.Subtract(this._start).TotalMilliseconds,
                Output = this._result
            };

            switch(this._result.Status)
            {
                case BusinessStatus.Completed:
                    this._logger.Info(JsonConvert.SerializeObject(this._resLog));
                break;
                case BusinessStatus.Info:
                    this._logger.Info(JsonConvert.SerializeObject(this._resLog));
                break;
                case BusinessStatus.Warning:
                    this._logger.Warning(JsonConvert.SerializeObject(this._resLog));
                break;
                 case BusinessStatus.Error:
                    this._logger.Error(JsonConvert.SerializeObject(this._resLog));
                break;
            }

            return this;
        }
        public RestApiFacade Execute()
        {
            Assembly assem = Assembly.LoadFrom(this._executeModel.FileName);
            MethodInfo m = assem.GetType(this._executeModel.ClassName).GetMethod(this._executeModel.MethodName);
            Object o = assem.CreateInstance(this._executeModel.ClassName, false, BindingFlags.ExactBinding, null, null,null,null);
            _result = (Result)m.Invoke(o, new Object[]{this._executeModel.ExecuteParameter, this._executeModel.Message});
            return this;    
        }

        public RestApiFacade Start()
        {
            this._start = DateTime.Now;
            return this;
        }

        public RestApiFacade End()
        {
            this._end = DateTime.Now;
            return this;
        }

        public Result ExecutionFlow(ExecuteModel model)
        {
            this._executeModel = model;
            this.WriteReqLog()
            .Start()
            .Execute()
            .End()
            .WriteResLog();
            return this._result;
        }
    }
}
