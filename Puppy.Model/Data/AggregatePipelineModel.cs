using System;
using System.Collections.Generic;

namespace Puppy.Model.Data
{
    public class AggregatePipelineModel:IModel
    {
        public string Match {get;set;}
        public string Sort {get;set;}
        public string Group {get;set;}
        public string Project {get;set;}

        public LookupModel Lookup {get;set;}
    }

    public class LookupModel:IModel
    {
        public string ForeignCollectionName {get;set;}
        public string LocalFieldName {get;set;}
        public string ForeignFieldName {get;set;}
        public string ResultAs {get;set;}
    }
}