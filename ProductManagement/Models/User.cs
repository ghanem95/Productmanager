using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace ProductManagement.Models
{
    public class User : Crud
    {
        private int id;
        private string login;
        private string password;
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
        private int typeprofil;

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
        public string Login { get => login; set => login = value; }
        public string Password { get => password; set => password = value; }
        public int Typeprofil { get => typeprofil; set => typeprofil = value; }

        public User()
        {
            this.Id = 0;
            this.Login = string.Empty;
            this.Password = string.Empty;
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
            this.Typeprofil = 0;

        }
        public void Authentication()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(Connectionstrings.Connectionstring()))
                {
                    SqlCommand cmd = new SqlCommand("selectuser", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@login", this.Login);
                    cmd.Parameters.AddWithValue("@password", this.Password);
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
                        this.Login = read["Login"].ToString();
                        this.Password = read["Password"].ToString();
                        this.Typeprofil = Convert.ToInt32(read["Typeprofil"]);
                    }
                    con.Close();
                }
            }
            catch (Exception ex)
            {
                ex.StackTrace.Replace(Environment.NewLine, ex.ToString());
            }

        }

        public override void Add()
        {
            try
            {
                SqlConnection connect = new SqlConnection(Connectionstrings.Connectionstring());
                SqlCommand cmd = new SqlCommand("adduser", connect);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@login", this.Login.isNull(string.Empty));
                cmd.Parameters.AddWithValue("@password", this.Password.isNull(string.Empty));
                cmd.Parameters.AddWithValue("@typeprofil", this.Typeprofil);
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
        public int CountListDatatable(int length, int start, string searchVal, string tri, string column)
        {
            int nbr = 0;
            using (SqlConnection conn = new SqlConnection(Connectionstrings.Connectionstring()))
            {
                try
                {
                    column = column.Replace("0", "id").Replace("1", "firstname").Replace("2", "lastname").Replace("3", "birthdate")
                        .Replace("4", "Adresse").Replace("5", "cite").Replace("6", "countrie").Replace("7", "codep").Replace("8", "email")
                        .Replace("9", "tel").Replace("10", "prof");
                    SqlCommand cmd = new SqlCommand("countdatatableusers", conn);
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
        public IList<User> ListDatatable(int length, int start, string searchVal, string tri, string column)
        {
            column = column.Replace("0", "id").Replace("1", "firstname").Replace("2", "lastname").Replace("3", "birthdate")
                .Replace("4", "Adresse").Replace("5", "cite").Replace("6", "countrie").Replace("7", "codep").Replace("8", "email")
                .Replace("9", "tel").Replace("10", "prof");
            List<User> users = new List<User>();
            using (SqlConnection conn = new SqlConnection(Connectionstrings.Connectionstring()))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("selectusers", conn);
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
                        users.Add(new User()
                        {
                            Id = Convert.ToInt32(reader["Id"]),
                            Firstname = reader["Firstname"].ToString(),
                            Lastname = reader["Lastname"].ToString(),
                            Birthdate = Convert.ToDateTime(reader["Birthdate"]),
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


            return users;
        }
        public override void Affiche(int id)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(Connectionstrings.Connectionstring()))
                {
                    SqlCommand cmd = new SqlCommand("selectuserbyid", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id", id);
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
                        this.Login = read["Login"].ToString();
                        this.Password = read["Password"].ToString();
                        this.Typeprofil = Convert.ToInt32(read["Typeprofil"]);
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
                using (SqlConnection connect = new SqlConnection(Connectionstrings.Connectionstring()))
                {
                    SqlCommand cmd = new SqlCommand("updateuser", connect);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id", this.Id);
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
                    cmd.Parameters.AddWithValue("@login", this.Login.isNull(string.Empty));
                    cmd.Parameters.AddWithValue("@password", this.Password.isNull(string.Empty));
                    cmd.Parameters.AddWithValue("@typeprofil", this.Typeprofil);
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

        public override string Delete(int id)
        {
            string msg;
            try
            {
                using (SqlConnection con = new SqlConnection(Connectionstrings.Connectionstring()))
                {
                    SqlConnection connect = new SqlConnection(Connectionstrings.Connectionstring());
                    SqlCommand cmd = new SqlCommand("Deleteuser", connect);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id", id);
                    connect.Open();
                    int rowAffected = cmd.ExecuteNonQuery();
                    connect.Close();
                    msg = "Client supprimé";
                }
            }
            catch
            {
                msg = "Client ne peut pas être supprimé";
            }
            return msg;


        }
    }
}