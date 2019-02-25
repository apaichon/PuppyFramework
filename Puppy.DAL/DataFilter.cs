using System;
using System.Collections.Generic;

namespace Puppy.DAL
{
    public class DataFilter
    {
        [System.ComponentModel.DefaultValue("")]
        public string MethodName
        {
            get;
            set;
        }

        public Dictionary<string,object> ParamList
        {
            get;
            set;
        }

        [System.ComponentModel.DefaultValue(true)]
        public  bool IsOne
        {
            get;
            set;
        }

    }
}