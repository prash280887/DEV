using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System;

namespace DBAccess
{
    public class DBAccess : IDBAccess
    {
        private string _constr;
        private SqlConnection _dbConObj;
        private SqlCommand _cmd;
        private SqlParameter _sqlParam;
        private DBAccess()
        {
        }
        public DBAccess(string ConStr)
        {
            _constr = ConStr;
            _cmd = new SqlCommand();
        }
        public void ExecuteProcedure<T>(ref List<T> outLst, string strSPName)
        {
            outLst = new List<T>();
            DataSet ds = ExecuteProcedure<DataSet>(strSPName);
            outLst = DataUtility.ConvertDataToList<T>(ds);
        }
        public void ExecuteQuery<T>(ref List<T> outLst, string strQry)
        {
            outLst = new List<T>();
            DataSet ds = ExecuteQuery<DataSet>(strQry);
            outLst = DataUtility.ConvertDataToList<T>(ds);
        }
        public T ExecuteProcedure<T>(string strSPName)
        {
            using (_dbConObj = new SqlConnection(_constr))
            {
                _dbConObj.Open();
                _cmd.Connection = _dbConObj;
                _cmd.CommandText = strSPName;
                _cmd.CommandType = CommandType.StoredProcedure;
                IDBAccessor<T> dbo = (IDBAccessor<T>)new DBAccessor();
                T dataObj = dbo.Execute(_cmd);
                return dataObj;
            }
        }
        public T ExecuteQuery<T>(string strQry)
        {
            T dataObj = default(T);
            using (_dbConObj = new SqlConnection(_constr))
            {
                _dbConObj.Open();
                _cmd.Connection = _dbConObj;
                _cmd.CommandText = strQry;
                _cmd.CommandType = CommandType.Text;
                IDBAccessor<T> dbo = (IDBAccessor<T>)new DBAccessor();
                dataObj = dbo.Execute(_cmd);
            }
            return dataObj;
        }
        public object ExecuteScalar(string strQry)
        {
            object rtnObj = null;
            using (_dbConObj = new SqlConnection(_constr))
            {
                _dbConObj.Open();
                _cmd.Connection = _dbConObj;
                _cmd.CommandText = strQry;
                _cmd.CommandType = CommandType.Text;
                rtnObj = _cmd.ExecuteScalar();
            }

            return rtnObj;
        }
        public int ExecuteNonQuery(string strQry)
        {
            int rtnCode = -1;
            using (_dbConObj = new SqlConnection(_constr))
            {
                _dbConObj.Open();
                _cmd.Connection = _dbConObj;
                _cmd.CommandText = strQry;
                _cmd.CommandType = CommandType.Text;
                rtnCode = _cmd.ExecuteNonQuery();
            }

            return rtnCode;
        }
        public void AddParameter(string strParamName, object value)
        {
            _sqlParam = new SqlParameter();
            _sqlParam.ParameterName = strParamName;
            _sqlParam.Direction = ParameterDirection.Input;
            _sqlParam.SqlValue = value == null? DBNull.Value : value;
            _cmd.Parameters.Add(_sqlParam);
        }
        public void AddParameter(string strParamName, object value, SqlDbType sqlDbType)
        {
            _sqlParam = new SqlParameter();
            _sqlParam.ParameterName = strParamName;
            _sqlParam.Direction = ParameterDirection.Input;
            _sqlParam.SqlDbType = sqlDbType;
            _sqlParam.SqlValue = value == null ? DBNull.Value : value; 
            _cmd.Parameters.Add(_sqlParam);
        }
        public void AddParameter(string strParamName, object value, ParameterDirection sqlParamDirection)
        {
            _sqlParam = new SqlParameter();
            _sqlParam.ParameterName = strParamName;
            _sqlParam.Value = value == null ? DBNull.Value : value;
            _sqlParam.Direction = sqlParamDirection;
            _cmd.Parameters.Add(_sqlParam);
        }
        public void AddParameter(string strParamName, object value, SqlDbType sqlDbType, ParameterDirection sqlParamDirection)
        {
            _sqlParam = new SqlParameter();
            _sqlParam.ParameterName = strParamName;
            _sqlParam.SqlDbType = sqlDbType;
            _sqlParam.Direction = sqlParamDirection;
            _sqlParam.Value = value == null ? DBNull.Value : value;
            _cmd.Parameters.Add(_sqlParam);
        }

    }


    public interface IDBCommand
    {
        void Open();
        void CreateCommand(string cmdText, CommandType cmdType);
        void AddParameters(string paramName, object paramValue);
        IDataReader ExecuteReader();
        object ExecuteScalar();
        int ExecuteNonQuery();
        void Close();
    }

    public class DBCommand : IDBCommand
    {
        private string _constr;
        private SqlConnection _dbConObj;
        private SqlCommand _cmd;

        private DBCommand()
        {
            
        }
        public DBCommand(string ConStr)
        {
            _constr = ConStr;
            _dbConObj = new SqlConnection(ConStr);
            _cmd = new SqlCommand();
        }

        public void Open()
        {
            _dbConObj.Open();
        }


        public void CreateCommand(string cmdText, CommandType cmdType)
        {
            _cmd = new SqlCommand();
            _cmd.Connection = _dbConObj;
            _cmd.CommandText = cmdText;
            _cmd.CommandType = cmdType;
        }

        public void AddParameters(string paramName, object paramValue)
        {
            _cmd = new SqlCommand();
            if(paramValue !=null)
            _cmd.Parameters.AddWithValue(paramName, paramValue);
            else
                _cmd.Parameters.AddWithValue(paramName, DBNull.Value);
        }

        public IDataReader ExecuteReader()
        {
           return _cmd.ExecuteReader();
        }
        public object ExecuteScalar()
        {
          return  _cmd.ExecuteScalar();
        }
        public int ExecuteNonQuery()
        {
            return _cmd.ExecuteNonQuery();
        }

        public void Close()
        {
            _dbConObj.Close();
        }

    }

}

