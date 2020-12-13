using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace ProductManagement.Models
{
    public class Type : Crud
    {

        private int id;
        private string typep;
        private string description;

        public int Id { get => id; set => id = value; }
        public string Typep { get => typep; set => typep = value; }
        public string Description { get => description; set => description = value; }


        public Type()
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
                    string sqlquery = "select * from Typeproduct";
                    SqlCommand cmd = new SqlCommand(sqlquery, con);
                    con.Open();
                    SqlDataReader read = cmd.ExecuteReader();
                    while (read.Read())
                    {
                        Type type = new Type();
                        type.Id = Convert.ToInt32(read["id"]);
                        type.Typep = read["type"].ToString();
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
        public int counttype()
        {
            int nb = 0;
            using (SqlConnection con = new SqlConnection(Connectionstrings.Connectionstring()))
            {
                string sqlquery = "select count(*) as nb from typeproduct ";
                SqlCommand cmd = new SqlCommand(sqlquery, con);
                con.Open();
                SqlDataReader read = cmd.ExecuteReader();
                while (read.Read())
                {
                    nb = Convert.ToInt32(read["nb"]);
                }
                con.Close();
            }

            return nb;
        }
        public override void Add()
        {
            try
            {
                SqlConnection connect = new SqlConnection(Connectionstrings.Connectionstring());
                SqlCommand cmd = connect.CreateCommand();
                cmd.CommandText = "Execute addtype @type, @description";
                cmd.Parameters.Add("@type", SqlDbType.NVarChar, 50).Value = this.Typep;
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
                    string sqlquery = "select * from typeproduct where id=" + id;
                    SqlCommand cmd = new SqlCommand(sqlquery, con);
                    con.Open();
                    SqlDataReader read = cmd.ExecuteReader();
                    while (read.Read())
                    {
                        this.Id = Convert.ToInt32(read["id"]);
                        this.Typep = read["type"].ToString();
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
                    string sqlquery = "Delete from typeproduct where id =" + id;
                    SqlCommand cmd = new SqlCommand(sqlquery, con);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                    msg = "Type de produit supprimé";
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
                    string sqlquery = "update typeproduct set Type='" + this.Typep +
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