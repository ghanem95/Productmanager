using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace ProductManagement.Models
{
    public class Client : Crud
    {
        private int id;
        private string firstname;
        private string lastname;
        private string adresse;
        private DateTime birthdate;
        private string cite;
        private string countrie;
        private int codep;
        private string email;
        private int tel;
        private string prof;

        public int Id { get => id; set => id = value; }
        public string Adresse { get => adresse; set => adresse = value; }
        public DateTime Birthdate { get => birthdate; set => birthdate = value; }
        public string Cite { get => cite; set => cite = value; }
        public string Countrie { get => countrie; set => countrie = value; }
        public int Codep { get => codep; set => codep = value; }
        public string Email { get => email; set => email = value; }
        public int Tel { get => tel; set => tel = value; }
        public string Prof { get => prof; set => prof = value; }
        public string Firstname { get => firstname; set => firstname = value; }
        public string Lastname { get => lastname; set => lastname = value; }

        public Client()
        {
            this.Id = 0;
            this.Firstname = string.Empty;
            this.Lastname = string.Empty;
            this.Birthdate = DateTime.Now;
            this.Adresse = string.Empty;
            this.Cite = string.Empty;
            this.Countrie = string.Empty;
            this.Codep = 0;
            this.Email = string.Empty;
            this.Tel = 0;
            this.Prof = string.Empty;

        }
        public override void Add()
        {
            SqlConnection connect = new SqlConnection(Connectionstrings.Connectionstring());
            SqlCommand cmd = connect.CreateCommand();
            cmd.CommandText = "Execute addcustomer @firstname,@lastname,@birthdate,@adresse,@cite,@countrie,@codep,@email,@tel,@prof";
            cmd.Parameters.Add("@firstname", SqlDbType.NVarChar, 50).Value = this.Firstname;
            cmd.Parameters.Add("@lastname", SqlDbType.NVarChar, 50).Value = this.Lastname;
            cmd.Parameters.Add("@birthdate", SqlDbType.Date).Value = this.Birthdate;
            cmd.Parameters.Add("@adresse", SqlDbType.NVarChar, 50).Value = this.Adresse;
            cmd.Parameters.Add("@cite", SqlDbType.NVarChar, 50).Value = this.Cite;
            cmd.Parameters.Add("@countrie", SqlDbType.NVarChar, 50).Value = this.Countrie;
            cmd.Parameters.Add("@codep", SqlDbType.Int, 50).Value = this.Codep;
            cmd.Parameters.Add("@email", SqlDbType.NVarChar, 50).Value = this.Email;
            cmd.Parameters.Add("@tel", SqlDbType.Int, 50).Value = this.Tel;
            cmd.Parameters.Add("@prof", SqlDbType.NVarChar, 50).Value = this.Prof;


            connect.Open();
            cmd.ExecuteNonQuery();
            connect.Close();
        }

        public IList<Client> List()
        {

            List<Client> Listclient = new List<Client>();
            using (SqlConnection con = new SqlConnection(Connectionstrings.Connectionstring()))
            {
                try
                {
                    string sqlquery = "select * from Customer";
                    SqlCommand cmd = new SqlCommand(sqlquery, con);
                    con.Open();
                    SqlDataReader read = cmd.ExecuteReader();
                    while (read.Read())
                    {
                        Client client = new Client();
                        client.Id = Convert.ToInt32(read["id"]);
                        client.Firstname = read["firstname"].ToString();
                        client.Lastname = read["lastname"].ToString();
                        client.Birthdate = Convert.ToDateTime(read["birthdate"]);
                        client.Adresse = read["adresse"].ToString();
                        client.Cite = read["cite"].ToString();
                        client.Countrie = read["countrie"].ToString();
                        client.Codep = Convert.ToInt32(read["codep"]);
                        client.Email = read["email"].ToString();
                        client.Tel = Convert.ToInt32(read["tel"]);
                        client.Prof = read["prof"].ToString();
                        Listclient.Add(client);

                    }
                    con.Close();
                }
                catch (Exception ex)
                {

                }
            }

            return Listclient;
        }
        public override void Affiche(int id)
        {
            using (SqlConnection con = new SqlConnection(Connectionstrings.Connectionstring()))
            {
                string sqlquery = "select * from Customer where id=" + id;
                SqlCommand cmd = new SqlCommand(sqlquery, con);
                con.Open();
                SqlDataReader read = cmd.ExecuteReader();
                while (read.Read())
                {
                    this.Id = Convert.ToInt32(read["id"]);
                    this.Firstname = read["firstname"].ToString();
                    this.Lastname = read["lastname"].ToString();
                    this.Birthdate = Convert.ToDateTime(read["birthdate"]);
                    this.Adresse = read["adresse"].ToString();
                    this.Codep = Convert.ToInt32(read["codep"]);
                    this.Cite = read["cite"].ToString();
                    this.Countrie = read["countrie"].ToString();
                    this.Email = read["email"].ToString();
                    this.Prof = read["prof"].ToString();
                    this.Tel = Convert.ToInt32(read["tel"]);
                }
                con.Close();
            }
        }

        public override string Delete(int idclt)
        {
            string msg;
            try
            {
                using (SqlConnection con = new SqlConnection(Connectionstrings.Connectionstring()))
                {
                    string sqlquery = "Delete from Customer where id =" + idclt;
                    SqlCommand cmd = new SqlCommand(sqlquery, con);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                    msg = "Client supprimé";
                }
            }
            catch
            {
                msg = "Client ne peut pas être supprimé";
            }
            return msg;


        }
        public int countclt()
        {
            int nb = 0;
            using (SqlConnection con = new SqlConnection(Connectionstrings.Connectionstring()))
            {
                string sqlquery = "select count(*) as nb from Customer ";
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
        public override void Update()
        {
            using (SqlConnection con = new SqlConnection(Connectionstrings.Connectionstring()))
            {
                string sqlquery = "update Customer set Firstname='" + this.Firstname +
                    "', Lastname = '" + this.Lastname + "'" +
                    ", Birthdate ='" + this.Birthdate + "'" +
                    ", adresse ='" + this.Adresse + "'" +
                    ", cite ='" + this.Cite + "'" +
                    ", countrie ='" + this.Countrie + "'" +
                    ", Codep ='" + this.Codep + "'" +
                    ", email ='" + this.Email + "'" +
                    ", tel ='" + this.Tel + "'" +
                    ", prof ='" + this.Prof + "'" +
                    " where id =" + this.Id;
                SqlCommand cmd = new SqlCommand(sqlquery, con);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }
    }
}