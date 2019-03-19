using System;
using Puppy.DAL;
using Puppy.Model;
using Puppy.Model.Data;
using Puppy.Model.Business;
using Puppy.Model.Message;
using Puppy.Model.Output;

namespace Puppy.BLL
{
    public class AggregateOperator:IBusinessOperator
    {
        public BusinessOperator Operator => BusinessOperator.Aggregate;
        public Result Execute(DataConfiguration config, object param, IMessage message)
        {
            Result result;
            using(IDataAdapter da = DataManager.Build(config))
            {
                string data = "";
                da.Open()
                  .Aggregate(param, out data)
                  .Close();
                if(!String.IsNullOrEmpty(data))
                {
                  result = Result.GetResult(BusinessStatus.Completed,220,message, data);
                }
                else
                {
                  result = Result.GetResult(BusinessStatus.Warning,420,message, data);
                }
            }
            return result;
        }

    }
}