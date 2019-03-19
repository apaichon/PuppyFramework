using System;
using Puppy.DAL;
using Puppy.Model;
using Puppy.Model.Data;
using Puppy.Model.Business;
using Puppy.Model.Message;
using Puppy.Model.Output;

namespace Puppy.BLL
{
    public class AddOperator:IBusinessOperator
    {
        public BusinessOperator Operator => BusinessOperator.Add;
        public Result Execute(DataConfiguration config, object param, IMessage message)
        {
            Result result;
            using(IDataAdapter da = DataManager.Build(config))
            {
                bool isAdded = false;
                da.Open()
                  .Add(param, out isAdded)
                  .Close();
                if(isAdded)
                {
                  result = Result.GetResult(BusinessStatus.Completed,210,message, param.ToString());
                }
                else
                {
                  result = Result.GetResult(BusinessStatus.Warning,410,message, param.ToString());
                }
            }
            return result;
        }

    
    }
}