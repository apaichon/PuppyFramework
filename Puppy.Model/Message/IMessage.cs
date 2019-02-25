using System;
using System.Collections.Generic;

namespace Puppy.Model.Message
{
    public interface IMessage
    {
        string GetMessage(int key);
    }
}