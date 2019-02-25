using System;
using System.Collections.Generic;
using System.Linq;
using Puppy.Model.Data;
using Puppy.Model.Message;
using Puppy.Model.Business;
using Puppy.Model.Output;

namespace Puppy.BLL
{
    public class BusinessStrategy:IBusinessStrategy
    {
        private readonly IEnumerable<IBusinessOperator> _operators;
        public BusinessStrategy(IEnumerable<IBusinessOperator> operators)
        {
            _operators = operators;
        }

        public Result Execute(DataConfiguration config,BusinessOperator op, object param, IMessage message)
        {
            return _operators.FirstOrDefault(x => x.Operator == op)?.Execute(config, param, message) ?? throw new Exception(nameof(op));
        }

    }
}