using System;
using Puppy.Model.Data;
using Puppy.Model.Message;
using Puppy.Model.Business;
using Puppy.Model.Output;

namespace Puppy.BLL
{
    public interface IBusinessOperator
    {
        BusinessOperator Operator {get;}
        Result Execute(DataConfiguration config, object param, IMessage message);
    }
}