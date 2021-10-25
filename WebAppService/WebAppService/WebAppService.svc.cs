using DBAccess;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using WebAppService.Entities;
using WebAppService.EntityModel;

namespace WebAppService
{
    public class WebAppService : IWebAppService
    {
        private string _CONSTR;

        private WebAppService()
        {
            _CONSTR = ConfigurationManager.AppSettings["DBConn"].ToString();
        }       

        public UserInfoModel GetUserDetails(string userId)
        {
            UserInfoModel uiInf = new UserInfoModel();
            try
            {
                IDBAccess dba = new DBAccess.DBAccess(_CONSTR);
                List<UserInfo> ui = new List<UserInfo>();                
                dba.AddParameter("@P_USERID", userId);
                dba.ExecuteProcedure<UserInfo>(ref ui, "SP_GETUSER");
                uiInf.dataList = ui;
            }
            catch(Exception ex)
            {
                //LogExceptionTo server
                uiInf.exception = ex;
            }

            return uiInf;
        }

        public  void TestService_SPCall(string userId)
        {
            UserInfoModel uiInf = new UserInfoModel();
            try
            {
                IDBAccess dba = new DBAccess.DBAccess(_CONSTR);

                List<UserInfo> ui;
                dba.AddParameter("@P_USERID", userId);

                //A)SP to DATASET
                DataSet ds = dba.ExecuteProcedure<DataSet>("SP_GETUSER");
                ui = new List<UserInfo>();
                dba.ExecuteProcedure<UserInfo>(ref ui, "SP_GETUSER");

                //B) SP to list
                ui = new List<UserInfo>();
                dba.ExecuteProcedure<UserInfo>(ref ui, "SP_GETUSER");
                uiInf.dataList = ui;
            }
            catch (Exception ex)
            {
                //LogExceptionTo server
                uiInf.exception = ex;
            }

           // return uiInf;
        }

        public void TestService_QueryCall(string query)
        {
            UserInfoModel uiInf = new UserInfoModel();
            try
            {
                IDBAccess dba = new DBAccess.DBAccess(_CONSTR);

                //A)query to DATASET
                DataSet ds = dba.ExecuteQuery<DataSet>(query);

                //B) query to ENITYLIST
                List<UserInfo> ui = new List<UserInfo>();
                dba.ExecuteQuery<UserInfo>(ref ui, query);
                uiInf.dataList = ui;

                //C) query to DATAREADER
                IDBCommand dbc = new DBCommand(_CONSTR);
                dbc.Open();
                dbc.CreateCommand(query, CommandType.Text);
                IDataReader dr = dbc.ExecuteReader();

                //fill data using below 2 methods to enitity
                //1. use while(dr.Read())
                //   { }
                //
                //2. Utiltity

                uiInf.dataList = DataUtility.ConvertDataToList<UserInfo>(dr);

                dbc.Close();
            }
            catch (Exception ex)
            {
                //LogExceptionTo server
                uiInf.exception = ex;
            }

           // return uiInf;         
        }

    }
}
