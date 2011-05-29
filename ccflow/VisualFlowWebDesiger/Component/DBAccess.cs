﻿using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using WF.Controls;
using WF.DataServiceReference;
using Silverlight;
using Ccflow.Web.UI.Control.Workflow.Designer;

namespace BP.DA
{
    public class DBAccess
    {
        #region run sql
        public static int RunSQL(string sql)
        {
            WebServiceSoapClient client = new WebServiceSoapClient();
            client.RunSQLAsync(sql, true);
            client.RunSQLCompleted += new EventHandler<RunSQLCompletedEventArgs>(client_RunSQLCompleted);
            return  myResultInt;
        }

        private static int myResultInt = 0;
        static void client_RunSQLCompleted(object sender, RunSQLCompletedEventArgs e)
        {
            myResultInt = e.Result;
        }
        #endregion run sql

        #region run sql return table.
        private static DataTable myResultDT = null;
        public static DataTable RunSQLReturnTable(string sql)
        {
            WebServiceSoapClient client = new WebServiceSoapClient();
            client.RunSQLReturnTableAsync(sql, true);
            client.RunSQLReturnTableCompleted+=new EventHandler<RunSQLReturnTableCompletedEventArgs>(client_RunSQLReturnTableCompleted);
            return myResultDT;
        }
        static void client_RunSQLReturnTableCompleted(object sender, RunSQLReturnTableCompletedEventArgs e)
        {
            DataSet ds = new DataSet();
            ds.FromXml(e.Result);
            myResultDT = ds.Tables[0];
        }
        #endregion run sql return table.
    }
}
