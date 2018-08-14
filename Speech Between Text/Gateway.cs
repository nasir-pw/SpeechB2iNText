using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;

namespace Speech_Between_Text
{
    public class Gateway
    {
        string info;
        int i=0;
        DBConnector connector = null;
        SqlConnection connection = null;
        Property ob = null;

        public Gateway()
        {
            connector = new DBConnector();
            connection = connector.Connection;
        }
        public Property RetrieveInfoFrmoTextTable()
        {
            //counter++;
            connection.Open();
            string queryString = "SELECT * FROM TextTable"; // WHERE User_ID=('" + counter + "')"
            SqlCommand command = new SqlCommand(queryString, connection);
            SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
            //reader.Read();
                
                ob.TextInfo= reader["Word"].ToString();
               // textArr=info;
                //return info;

           }
            connection.Close();
            return ob;

        }
    }
}
