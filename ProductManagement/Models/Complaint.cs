using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace ProductManagement.Models
{
    public class Complaint : Crud
    {

        private int id;
        private string title;
        private string description;
        private string desctype;
        private string descstate;
        private int type;
        private DateTime creation_date;
        private int state;
        private string product;

        public int Id { get => id; set => id = value; }
        public string Title { get => title; set => title = value; }
        public string Description { get => description; set => description = value; }
        public int Type { get => type; set => type = value; }
        public DateTime Creation_date { get => creation_date; set => creation_date = value; }
        public int State { get => state; set => state = value; }
        public string Product { get => product; set => product = value; }
        public string Desctype { get => desctype; set => desctype = value; }
        public string Descstate { get => descstate; set => descstate = value; }

        public Complaint()
        {
            this.Id = 0;
            this.Title = String.Empty;
            this.Description = String.Empty;
            this.Type = 0;
            this.Creation_date = DateTime.Now;
            this.Descstate = String.Empty;
            this.Desctype = String.Empty;
        }
        public int CountListDatatable(int length, int start, string searchVal, string tri, string column)
        {
            int nbr = 0;
            using (SqlConnection conn = new SqlConnection(Connectionstrings.Connectionstring()))
            {
                try
                {
                    column = column.Replace("0", "id").Replace("1", "title").Replace("2", "description").Replace("3", "type")
                        .Replace("4", "product").Replace("5", "Creation_date").Replace("6", "state");
                    SqlCommand cmd = new SqlCommand("countdatatablecomplaints", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@number", length);
                    cmd.Parameters.AddWithValue("@start", start);
                    cmd.Parameters.AddWithValue("@sortcolumn", column);
                    cmd.Parameters.AddWithValue("@tri", tri);
                    cmd.Parameters.AddWithValue("@searchval", searchVal);
                    conn.Open();
                    cmd.Connection = conn;
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        nbr = Convert.ToInt32(reader["nbr"]);
                    }
                    reader.Close();
                    conn.Close();
                }
                catch (Exception ex)
                {
                    ex.StackTrace.Replace(Environment.NewLine, ex.ToString());
                }
            }

            return nbr;

        }
        public IList<Complaint> ListDatatable(int length, int start, string searchVal, string tri, string column)
        {
            column = column.Replace("0", "id").Replace("1", "title").Replace("2", "description").Replace("3", "type")
                        .Replace("4", "product").Replace("5", "Creation_date").Replace("6", "state");
            List<Complaint> Complaints = new List<Complaint>();
            using (SqlConnection conn = new SqlConnection(Connectionstrings.Connectionstring()))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("selectcomplaints", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@number", length);
                    cmd.Parameters.AddWithValue("@start", start);
                    cmd.Parameters.AddWithValue("@sortcolumn", column);
                    cmd.Parameters.AddWithValue("@tri", tri);
                    cmd.Parameters.AddWithValue("@searchval", searchVal);
                    conn.Open();
                    cmd.Connection = conn;
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        Complaints.Add(new Complaint()
                        {
                            Id = Convert.ToInt32(reader["Id"]),
                            Title = reader["Title"].ToString(),
                            Description = reader["Description"].ToString(),
                            Creation_date = Convert.ToDateTime(reader["Creation_date"]),
                            Product = reader["ref"].ToString(),
                            State = Convert.ToInt32(reader["State"]),
                            Type = Convert.ToInt32(reader["Type"]),
                            Desctype = reader["Desctype"].ToString(),
                            Descstate = reader["descstate"].ToString(),
                        }); ;
                    }
                    reader.Close();
                    conn.Close();
                }
                catch (Exception ex)
                {
                    ex.StackTrace.Replace(Environment.NewLine, ex.ToString());
                }
            }


            return Complaints;
        }

        public override void Add()
        {
            try
            {
                SqlConnection connect = new SqlConnection(Connectionstrings.Connectionstring());
                SqlCommand cmd = new SqlCommand("addcomplaint", connect);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@title", this.Title.isNull(string.Empty));
                cmd.Parameters.AddWithValue("@description", this.Description.isNull(string.Empty));
                cmd.Parameters.AddWithValue("@creation_date", this.Creation_date);
                cmd.Parameters.AddWithValue("@type", this.Type);
                cmd.Parameters.AddWithValue("@state", this.State);
                cmd.Parameters.AddWithValue("@product", this.Product.isNull(string.Empty));
                connect.Open();
                cmd.ExecuteNonQuery();
                connect.Close();
            }
            catch (Exception ex)
            {
                ex.StackTrace.Replace(Environment.NewLine, ex.ToString());
            }

        }

        public override void Affiche(int id)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(Connectionstrings.Connectionstring()))
                {
                    SqlCommand cmd = new SqlCommand("selectcomplaintbyid", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id", id);
                    con.Open();
                    SqlDataReader read = cmd.ExecuteReader();
                    while (read.Read())
                    {
                        this.Id = Convert.ToInt32(read["id"]);
                        this.Title = read["Title"].ToString();
                        this.Description = read["Description"].ToString();
                        this.Creation_date = Convert.ToDateTime(read["Creation_date"]);
                        this.Product = read["Product"].ToString();
                        this.Type = Convert.ToInt32(read["Type"]);
                        this.State = Convert.ToInt32(read["State"]);
                    }
                    con.Close();
                }
            }
            catch (Exception ex)
            {
                ex.StackTrace.Replace(Environment.NewLine, ex.ToString());
            }

        }
        public override string Delete(int id)
        {
            string msg;
            try
            {
                using (SqlConnection con = new SqlConnection(Connectionstrings.Connectionstring()))
                {
                    SqlCommand cmd = new SqlCommand("Deletecomplaint", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id", id);
                    con.Open();
                    int rowAffected = cmd.ExecuteNonQuery();
                    con.Close();
                    msg = "Réclamation supprimé";
                }
            }
            catch
            {
                msg = "Réclamation ne peut pas être supprimé";
            }
            return msg;


        }

        public override void Update()
        {
            try
            {
                using (SqlConnection connect = new SqlConnection(Connectionstrings.Connectionstring()))
                {
                    SqlCommand cmd = new SqlCommand("updatecomplaint", connect);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id", this.Id);
                    cmd.Parameters.AddWithValue("@title", this.Title.isNull(string.Empty));
                    cmd.Parameters.AddWithValue("@description", this.Description.isNull(string.Empty));
                    cmd.Parameters.AddWithValue("@creation_date", this.Creation_date);
                    cmd.Parameters.AddWithValue("@type", this.Type);
                    cmd.Parameters.AddWithValue("@state", this.State);
                    cmd.Parameters.AddWithValue("@product", this.Product.isNull(string.Empty)); ;
                    connect.Open();
                    int rowAffected = cmd.ExecuteNonQuery();
                    connect.Close();
                }
            }
            catch (Exception ex)
            {
                ex.StackTrace.Replace(Environment.NewLine, ex.ToString());
            }

        }
    }
}