using System;
using System.Collections.Generic;
using Puppy.Model.Message;
using Puppy.Model.Business;

namespace Puppy.BLL
{
    public class BusinessMessage
    {
      public BusinessMessage()
      {

      }

      public static IMessage CreateMessage(BusinessLocale locale)
        {
            IMessage message;
            switch (locale)
            {
                default:
                  message = new EN_US();
                break;
                case BusinessLocale.en_US:
                  message = new EN_US();
                break;
                case BusinessLocale.th_TH:
                  message = new TH_TH();
                break;
            }
            return message;
        }
    }
}