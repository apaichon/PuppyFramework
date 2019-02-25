using System;
using System.Linq;

namespace Puppy.BLL
{
    public class BusinessStrategyFactory: IBusinessStrategyFactory
    {
      private readonly AddOperator _addOperator;
      private readonly GetOperator _getOperator;

      public BusinessStrategyFactory(AddOperator addOperator, GetOperator getOperator)
      {
        this._addOperator = addOperator;
        this._getOperator = getOperator;
      }

      public IBusinessOperator[] Create() => new IBusinessOperator[]
      {
        _addOperator,
        _getOperator
      };
    }
}