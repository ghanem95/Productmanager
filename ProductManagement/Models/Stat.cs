using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace ProductManagement.Models
{
    public class Stat
    {
        private int id;
        private string day;
        private int value;

        public int Id { get => id; set => id = value; }
        public string Day { get => day; set => day = value; }
        public int Value { get => value; set => this.value = value; }

        public IList<Stat> List()
        {

            List<Stat> ListStat = new List<Stat>();
            using (SqlConnection con = new SqlConnection(Connectionstrings.Connectionstring()))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("selectstat", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    SqlDataReader read = cmd.ExecuteReader();
                    while (read.Read())
                    {
                        Stat Stat = new Stat();
                        Stat.Day = read["Day"].ToString();
                        Stat.Value = Convert.ToInt32(read["Val"]);
                        ListStat.Add(Stat);

                    }
                    con.Close();
                }
                catch (Exception ex)
                {
                    ex.StackTrace.Replace(Environment.NewLine, ex.ToString());
                }
            }

            return ListStat;
        }
    }
}