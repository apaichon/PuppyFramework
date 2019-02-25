using System;
using Puppy.Model.Business;

namespace Puppy.BLL
{
    public interface IBusinessStrategyFactory
    {
        IBusinessOperator[] Create(); 
    }
}
   