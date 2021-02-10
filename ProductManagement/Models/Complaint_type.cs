using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace ProductManagement.Models
{
    public class Complaint_type
    {
        int id;
        string type;

        public int Id { get => id; set => id = value; }
        public string Type { get => type; set => type = value; }

        public Complaint_type()
        {
            this.Id = 0;
            this.Type = String.Empty;
        }

        public IList<Complaint_type> List()
        {

            List<Complaint_type> Complaint_types = new List<Complaint_type>();
            using (SqlConnection con = new SqlConnection(Connectionstrings.Connectionstring()))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("Listtypecomplaint", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    SqlDataReader read = cmd.ExecuteReader();
                    while (read.Read())
                    {
                        Complaint_type Complaint_type = new Complaint_type();
                        Complaint_type.Id = Convert.ToInt32(read["id"]);
                        Complaint_type.Type = read["Type"].ToString();
                        Complaint_types.Add(Complaint_type);

                    }
                    con.Close();
                }
                catch (Exception ex)
                {
                    ex.StackTrace.Replace(Environment.NewLine, ex.ToString());
                }
            }

            return Complaint_types;
        }
    }
}