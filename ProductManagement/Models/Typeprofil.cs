using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace ProductManagement.Models
{
    public class Typeprofil : Crud
    {
        private int id;
        private string typep;
        private string description;

        public int Id { get => id; set => id = value; }
        public string Typep { get => typep; set => typep = value; }
        public string Description { get => description; set => description = value; }


        public Typeprofil()
        {
            this.Id = 0;
            this.Typep = string.Empty;
            this.Description = string.Empty;
        }
        public IList<Type> List()
        {

            List<Type> Listtype = new List<Type>();
            using (SqlConnection con = new SqlConnection(Connectionstrings.Connectionstring()))
            {
                try
                {
                    string sqlquery = "select * from Typeprofil";
                    SqlCommand cmd = new SqlCommand(sqlquery, con);
                    con.Open();
                    SqlDataReader read = cmd.ExecuteReader();
                    while (read.Read())
                    {
                        Type type = new Type();
                        type.Id = Convert.ToInt32(read["id"]);
                        type.Typep = read["name"].ToString();
                        type.Description = read["description"].ToString();
                        Listtype.Add(type);

                    }
                    con.Close();
                }
                catch (Exception ex)
                {
                    ex.StackTrace.Replace(Environment.NewLine, ex.ToString());
                }
            }

            return Listtype;
        }
        public override void Add()
        {
            try
            {
                SqlConnection connect = new SqlConnection(Connectionstrings.Connectionstring());
                SqlCommand cmd = connect.CreateCommand();
                cmd.CommandText = "Execute addtypeprofil @name, @description";
                cmd.Parameters.Add("@name", SqlDbType.NVarChar, 50).Value = this.Typep;
                cmd.Parameters.Add("@description", SqlDbType.NVarChar, 50).Value = this.Description.isNull(string.Empty);
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
                    string sqlquery = "select * from typeprofil where id=" + id;
                    SqlCommand cmd = new SqlCommand(sqlquery, con);
                    con.Open();
                    SqlDataReader read = cmd.ExecuteReader();
                    while (read.Read())
                    {
                        this.Id = Convert.ToInt32(read["id"]);
                        this.Typep = read["name"].ToString();
                        this.Description = read["description"].ToString();
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
            string msg = "";
            try
            {
                using (SqlConnection con = new SqlConnection(Connectionstrings.Connectionstring()))
                {
                    string sqlquery = "Delete from typeprofil where id =" + id;
                    SqlCommand cmd = new SqlCommand(sqlquery, con);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                    msg = "Type de profil supprimé";
                }
            }
            catch (Exception ex)
            {
                msg = "Type ne peut pas être supprimé";
            }
            return msg;
        }

        public override void Update()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(Connectionstrings.Connectionstring()))
                {
                    string sqlquery = "update typeprofil set Name='" + this.Typep +
                        "', Description = '" + this.Description + "'" +
                        " where id =" + this.Id;
                    SqlCommand cmd = new SqlCommand(sqlquery, con);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
            }
            catch (Exception ex)
            {
                ex.StackTrace.Replace(Environment.NewLine, ex.ToString());
            }

        }
    }
}