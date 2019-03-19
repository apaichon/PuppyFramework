using System;
using System.Linq;

namespace Puppy.BLL
{
    public class BusinessStrategyFactory: IBusinessStrategyFactory
    {
      private readonly AddOperator _addOperator;
      private readonly GetOperator _getOperator;

      private readonly AggregateOperator _aggregateOperator;
      private readonly RunCommandOperator _runCommandOperator;

      public BusinessStrategyFactory(AddOperator addOperator, GetOperator getOperator,AggregateOperator aggregateOperator, RunCommandOperator runCommandOperator)
      {
        this._addOperator = addOperator;
        this._getOperator = getOperator;
        this._aggregateOperator = aggregateOperator;
        this._runCommandOperator = runCommandOperator;
      }

      public IBusinessOperator[] Create() => new IBusinessOperator[]
      {
        _addOperator,
        _getOperator,
        _addOperator,
        _runCommandOperator
      };
    }
}