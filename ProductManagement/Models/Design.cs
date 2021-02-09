using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace ProductManagement.Models
{
    public class Design: Crud
    {
        int id_user;
        string designs;
        string datatable;
        string btn;
        public int Id_user { get => id_user; set => id_user = value; }
        public string Designs { get => designs; set => designs = value; }
        public string Datatable { get => datatable; set => datatable = value; }
        public string Btn { get => btn; set => btn = value; }

        public Design()
        {
            id_user = 0;
            designs = string.Empty;
            datatable = string.Empty;
            btn = string.Empty;
        }

        public override void Add()
        {
            throw new NotImplementedException();
        }

        public override void Affiche(int id)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(Connectionstrings.Connectionstring()))
                {
                    SqlCommand cmd = new SqlCommand("getdesign", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id", this.Id_user);
                    con.Open();
                    SqlDataReader read = cmd.ExecuteReader();
                    while (read.Read())
                    {
                        this.Id_user = Convert.ToInt32(read["id_user"]);
                        this.Designs = read["Design"].ToString();
                        this.Datatable = read["Datatable"].ToString();
                        this.Btn = read["Btn"].ToString();
                    }
                    con.Close();
                }
            }
            catch (Exception ex)
            {
                ex.StackTrace.Replace(Environment.NewLine, ex.ToString());
            }
        }

        public override void Update()
        {
            try
            {
                SqlConnection connect = new SqlConnection(Connectionstrings.Connectionstring());
                SqlCommand cmd = new SqlCommand("updatedesign", connect);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id", this.Id_user);
                cmd.Parameters.AddWithValue("@design", this.Designs.isNull(string.Empty));
                cmd.Parameters.AddWithValue("@datatable", this.Datatable.isNull(string.Empty));
                cmd.Parameters.AddWithValue("@btn", this.Btn.isNull(string.Empty));
                connect.Open();
                int rowAffected = cmd.ExecuteNonQuery();
                connect.Close();
            }
            catch (Exception ex)
            {
                ex.StackTrace.Replace(Environment.NewLine, ex.ToString());
            }
        }

        public override string Delete(int id)
        {
            throw new NotImplementedException();
        }
    }
}