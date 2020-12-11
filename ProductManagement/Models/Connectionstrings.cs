using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace ProductManagement.Models
{
    public static class Connectionstrings
    {


        static string connectionString = ConfigurationManager.ConnectionStrings["YourConnectionStringKey"].ConnectionString;
        public static string Connectionstring()
        {
            return connectionString;
        }

    }
}