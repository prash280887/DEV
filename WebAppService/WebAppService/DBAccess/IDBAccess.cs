using System.Collections.Generic;
using System.Data;

namespace DBAccess
{
    public interface IDBAccess
    {
        void ExecuteProcedure<T>(ref List<T> outLst, string strSPName); 
        void ExecuteQuery<T>(ref List<T> outLst, string strQry);
        T ExecuteQuery<T>(string strQry);
        int ExecuteNonQuery(string strQry);
        T ExecuteProcedure<T>(string strSPName);
        void AddParameter(string strParamName, object value);
        void AddParameter(string strParamName, object value, SqlDbType sqlDbType);
        void AddParameter(string strParamName, object value, ParameterDirection sqlParamDirection);
        void AddParameter(string strParamName, object value, SqlDbType sqlDbType, ParameterDirection sqlParamDirection);
    }
}

