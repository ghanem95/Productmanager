using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace ProductManagement.Models
{
    public class Complaint_state
    {
        int id;
        string state;

        public int Id { get => id; set => id = value; }
        public string State { get => state; set => state = value; }

        public Complaint_state()
        {
            this.Id = 0;
            this.State = String.Empty;
        }
        public IList<Complaint_state> List()
        {

            List<Complaint_state> Complaint_states = new List<Complaint_state>();
            using (SqlConnection con = new SqlConnection(Connectionstrings.Connectionstring()))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("Liststatecomplaint", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    SqlDataReader read = cmd.ExecuteReader();
                    while (read.Read())
                    {
                        Complaint_state Complaint_state = new Complaint_state();
                        Complaint_state.Id = Convert.ToInt32(read["id"]);
                        Complaint_state.State = read["State"].ToString();
                        Complaint_states.Add(Complaint_state);

                    }
                    con.Close();
                }
                catch (Exception ex)
                {
                    ex.StackTrace.Replace(Environment.NewLine, ex.ToString());
                }
            }

            return Complaint_states;
        }
    }
}