using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;

namespace Speech_Between_Text
{
    public class DBConnector
    {
        private string connectionString = @"server = (local)\SQLEXPRESS; Integrated Security = SSPI; Initial Catalog = TextDB";

        SqlConnection connection = null;

        public DBConnector()
        {
            connection = new SqlConnection(connectionString);
        }

        public SqlConnection Connection
        {
            get { return connection; }
        }
       
    }
}
