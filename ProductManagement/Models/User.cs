using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace ProductManagement.Models
{
    public class User
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
                    }
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