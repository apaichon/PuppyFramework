using System;
using System.Collections.Generic;

namespace Puppy.DAL
{
    public interface IDataAdapter: IDisposable
    {
        #region Members

         string ConnectionString
         {
             get;
             set;
         }

         int ConnectionTimeout
         {
             get;
             set;
         }

         int ExecutionTimeout
         {
             get;
             set;
         }

         string DatabaseName
         {
             get;
             set;
         }

         string TableName
         {
             get;
             set;
         }
        
        #endregion

        #region Methods
        IDataAdapter Open();
        IDataAdapter Close();
        void Dispose();
        IDataAdapter Get();
        IDataAdapter Get(out string json);
        IDataAdapter Get(string filter, out string result);
        IDataAdapter Add(object data, out bool isInserted);
        IDataAdapter Edit(string filter, object data, out bool isUpdated);
        IDataAdapter EditOne(Dictionary<string,object> filter, object data, out bool isUpdated);
        IDataAdapter EditMany(string filter, string data, out long totalModified);
        IDataAdapter DeleteMany(string filter,out long totalDeleted);
        /*
        IDAL Get(DataFilter filter);
        IDAL Add();
        IDAL Add(DataFilter filter);
        IDAL Edit();
        IDAL Edit(DataFilter filter);
        IDAL Delete();
        IDAL Delete(DataFilter filter);
        */
        #endregion
    }
}