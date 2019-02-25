using System;
using System.Collections.Generic;
using System.Linq;
using Puppy.Model.Data;
using Puppy.Model.Message;
using Puppy.Model.Business;
using Puppy.Model.Output;

namespace Puppy.BLL
{
    public interface IBusinessStrategy
    {
        Result Execute(DataConfiguration config, BusinessOperator op, object param, IMessage message);
    }
}
   