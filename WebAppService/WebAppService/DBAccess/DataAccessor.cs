using System.Data.SqlClient;
using System.Data;

namespace DBAccess
{
    internal interface IDBAccessor<T>
    {
      T Execute(SqlCommand _cmd);
    }

    internal class DBAccessor : IDBAccessor<IDataReader>, IDBAccessor<DataSet>
    {
        DataSet IDBAccessor<DataSet>.Execute(SqlCommand _cmd)
        {
            SqlDataAdapter _da;
            DataSet _ds = new DataSet();
                _da = new SqlDataAdapter(_cmd);
                _da.Fill(_ds);
            return _ds;
        }
        IDataReader IDBAccessor<IDataReader>.Execute(SqlCommand _cmd)
        {
            SqlDataReader _dr;
            _dr = _cmd.ExecuteReader();                      
            return _dr;
        }
       
    }
    
}

