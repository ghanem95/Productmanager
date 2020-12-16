using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
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
        public int CountListDatatable(int length, int start, string searchVal, string tri, string column)
        {
            int nbr = 0;
            using (SqlConnection conn = new SqlConnection(Connectionstrings.Connectionstring()))
            {
                try
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand();
                    StringBuilder sbSQL = new StringBuilder();
                    column = (Convert.ToInt32(column) + 1).ToString();
                    column = column.Replace("1", "id").Replace("2", "firstname").Replace("3", "lastname").Replace("4", "birthdate")
                        .Replace("5", "Adresse").Replace("6", "cite").Replace("7", "countrie").Replace("8", "codep").Replace("9", "email")
                        .Replace("10", "tel").Replace("11", "prof");
                    sbSQL.AppendFormat("select count(*) nbr from", length);
                    sbSQL.AppendFormat(" (select clt.*, row_number() over(order by {0}", column);
                    sbSQL.AppendFormat(" {0}) as [row_number] from Customer clt) clt", tri);
                    sbSQL.AppendFormat(" where row_number >{0}", start);

                    if (!string.IsNullOrEmpty(searchVal))
                    {
                        sbSQL.AppendFormat(" and Firstname like '%{0}%' or Lastname like '%{0}%' or FORMAT(birthdate, 'dd/MM/yyyy ') " +
                            "like '%{0}%' " +
                            "or Adresse like '%{0}%' or Cite like '%{0}%' or Countrie like '%{0}%' or Codep like '%{0}%'" +
                            " or Email like '%{0}%' or Tel like '%{0}%' or Prof like '%{0}%'", searchVal);
                    }

                    cmd.CommandText = sbSQL.ToString();
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

                }
            }

            return nbr;

        }
        public IList<Client> ListDatatable(int length, int start, string searchVal,string tri,string column)
        {
            column = column.Replace("0", "id").Replace("1", "firstname").Replace("2", "lastname").Replace("3", "birthdate")
                .Replace("4", "Adresse").Replace("5", "cite").Replace("6", "countrie").Replace("7", "codep").Replace("8", "email")
                .Replace("9", "tel").Replace("10", "prof");
            List<Client> clients = new List<Client>();
            using (SqlConnection conn = new SqlConnection(Connectionstrings.Connectionstring()))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("selectcustomers", conn);
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
                        clients.Add(new Client()
                        {
                            Id = Convert.ToInt32(reader["Id"]),
                            Firstname = reader["Firstname"].ToString(),
                            Lastname = reader["Lastname"].ToString(),
                            Birthdate =Convert.ToDateTime(reader["Birthdate"]),
                            Adresse = reader["Adresse"].ToString(),
                            Cite = reader["Cite"].ToString(),
                            Countrie = reader["countrie"].ToString(),
                            Codep = Convert.ToInt32(reader["Codep"]),
                            Email = reader["Email"].ToString(),
                            Tel = Convert.ToInt32(reader["Tel"]),
                            Prof = reader["Prof"].ToString(),
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


            return clients;
        }
        public override void Add()
        {
            try
            {
                SqlConnection connect = new SqlConnection(Connectionstrings.Connectionstring());
                SqlCommand cmd = new SqlCommand("addcustomer", connect);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@firstname", this.Firstname.isNull(string.Empty));
                cmd.Parameters.AddWithValue("@lastname", this.Lastname.isNull(string.Empty));
                cmd.Parameters.AddWithValue("@birthdate", this.Birthdate);
                cmd.Parameters.AddWithValue("@adresse", this.Adresse.isNull(string.Empty));
                cmd.Parameters.AddWithValue("@cite", this.Cite.isNull(string.Empty));
                cmd.Parameters.AddWithValue("@countrie", this.Countrie.isNull(string.Empty));
                cmd.Parameters.AddWithValue("@codep", this.Codep);
                cmd.Parameters.AddWithValue("@email", this.Email.isNull(string.Empty));
                cmd.Parameters.AddWithValue("@tel", this.Tel);
                cmd.Parameters.AddWithValue("@prof", this.Prof.isNull(string.Empty));
                connect.Open();
                int rowAffected = cmd.ExecuteNonQuery();
                connect.Close();
            }
            catch (Exception ex)
            {
                ex.StackTrace.Replace(Environment.NewLine, ex.ToString());
            }

        }
       

            public IList<Client> List()
        {

            List<Client> Listclient = new List<Client>();
            using (SqlConnection con = new SqlConnection(Connectionstrings.Connectionstring()))
            {
                try
                {
                    //string sqlquery = "select * from Customer";
                    SqlCommand cmd = new SqlCommand("selectcustomer", con);
                    cmd.CommandType = CommandType.StoredProcedure;
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
                    ex.StackTrace.Replace(Environment.NewLine, ex.ToString());
                }
            }

            return Listclient;
        }
        public override void Affiche(int id)
        {
            try
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
            catch (Exception ex)
            {
                ex.StackTrace.Replace(Environment.NewLine, ex.ToString());
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
            try
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
            catch (Exception ex)
            {
                ex.StackTrace.Replace(Environment.NewLine, ex.ToString());
            }

        }
    }
}