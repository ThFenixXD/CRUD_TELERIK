using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace WebApplication
{
    public class Framework
    {
        public static DataTable GetDataTable(string query)
        {
            String ConnString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            SqlConnection conn = new SqlConnection(ConnString);
            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = new SqlCommand(query, conn);
            DataTable myDataTable = new DataTable();
            conn.Open();
            try
            {
                adapter.Fill(myDataTable);
            }
            finally
            {
                conn.Close();
            }
            return myDataTable;

        }
    }
}