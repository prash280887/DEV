using System;
using System.Collections.Generic;
using System.Data;

namespace DBAccess
{
    public static class DataUtility
    {
        public static List<T> ConvertDataToList<T>(DataSet ds)
        {
            try
            {
                object val = null;
                string colName = "";
                Type type = null;
                List <T> outLst = new List<T>();
                T tObj = default(T);
                foreach (DataRow drc in ds.Tables[0].Rows)
                {
                    tObj = Activator.CreateInstance<T>();
                    for (int i = 0; i < ds.Tables[0].Columns.Count; i++)
                    {
                       val = drc[i];
                        colName = ds.Tables[0].Columns[i].ColumnName;
                        type = drc[i].GetType();
                        DataUtility.SetPropertyValue(tObj, colName, val);                       
                    }
                    outLst.Add(tObj);
                }

                return outLst;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static List<T> ConvertDataToList<T>(IDataReader dr)
        {
            try
            {
                List<T> outLst = new List<T>();
                T tObj = default(T); 
                object val = null;
                string colName = "";
                Type type = null;
                if (dr != null)
                {
                    while (dr.Read())
                    {
                        tObj = Activator.CreateInstance<T>();
                        for (int i = 0; i < dr.FieldCount; i++)
                        {
                            val = dr[i];
                            colName = dr.GetName(i);
                            type = dr[i].GetType();
                            DataUtility.SetPropertyValue(tObj, colName, val);
                        }
                        outLst.Add(tObj);
                    }
                }

                return outLst;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void SetPropertyValue(object theObject, string theProperty, object theValue)
        {
            try
            {
                var msgInfo = theObject.GetType().GetProperty(theProperty);
                if(msgInfo !=null)
                msgInfo.SetValue(theObject, theValue);
            }
            catch(Exception e) 
            {
                throw e;
            }
        }
    }
}

