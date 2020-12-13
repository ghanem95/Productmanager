using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace ProductManagement.Models
{
    public class Commande : Crud
    {
        private int id;
        private int idpdt;
        private string namepdt;
        private string nameclt;
        private int idclt;
        private int qt;
        private DateTime datec;
        public int Id { get => id; set => id = value; }
        public int Idpdt { get => idpdt; set => idpdt = value; }
        public int Idclt { get => idclt; set => idclt = value; }
        public int Qt { get => qt; set => qt = value; }
        public DateTime Datec { get => datec; set => datec = value; }
        public string Namepdt { get => namepdt; set => namepdt = value; }
        public string Nameclt { get => nameclt; set => nameclt = value; }

        public Commande()
        {
            this.Id = 0;
            this.Datec = DateTime.Now;
        }
        public override void Add()
        {
            try
            {
                SqlConnection connect = new SqlConnection(Connectionstrings.Connectionstring());
                SqlCommand cmd = connect.CreateCommand();
                cmd.CommandText = "Execute addcommande @idpdt,@idclt,@qt,@datec";
                cmd.Parameters.Add("@idpdt", SqlDbType.NVarChar, 50).Value = this.Idpdt;
                cmd.Parameters.Add("@idclt", SqlDbType.NVarChar, 50).Value = this.Idclt;
                cmd.Parameters.Add("@qt", SqlDbType.NVarChar).Value = this.Qt;
                cmd.Parameters.Add("@datec", SqlDbType.Date).Value = this.Datec;
                connect.Open();
                cmd.ExecuteNonQuery();
                connect.Close();
            }
            catch (Exception ex)
            {
                ex.StackTrace.Replace(Environment.NewLine, ex.ToString());
            }

        }

        public IList<Commande> List()
        {

            List<Commande> Commande = new List<Commande>();
            using (SqlConnection con = new SqlConnection(Connectionstrings.Connectionstring()))
            {
                try
                {
                    string sqlquery = "select c.id,c.idpdt,c.idclt,p.name as namepdt,cl.firstname,c.qt,c.datec from Commande c " +
                        "inner join product p on p.id=c.idpdt" +
                        " inner join customer cl on cl.id=c.idclt";
                    SqlCommand cmd = new SqlCommand(sqlquery, con);
                    con.Open();
                    SqlDataReader read = cmd.ExecuteReader();
                    while (read.Read())
                    {
                        Commande commande = new Commande();
                        commande.Id = Convert.ToInt32(read["id"]);
                        commande.Idpdt = Convert.ToInt32(read["idpdt"]);
                        commande.Idclt = Convert.ToInt32(read["idclt"]);
                        commande.Nameclt = read["firstname"].ToString();
                        commande.Namepdt = read["namepdt"].ToString();
                        commande.Qt = Convert.ToInt32(read["qt"]);
                        commande.Datec = Convert.ToDateTime(read["datec"]);
                        Commande.Add(commande);

                    }
                    con.Close();
                }
                catch (Exception ex)
                {
                    ex.StackTrace.Replace(Environment.NewLine, ex.ToString());
                }
            }

            return Commande;
        }
        public override void Affiche(int id)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(Connectionstrings.Connectionstring()))
                {
                    string sqlquery = "select * from Commande where id=" + id;
                    SqlCommand cmd = new SqlCommand(sqlquery, con);
                    con.Open();
                    SqlDataReader read = cmd.ExecuteReader();
                    while (read.Read())
                    {
                        this.Id = Convert.ToInt32(read["id"]);
                        this.Idclt = Convert.ToInt32(read["idclt"]);
                        this.Idpdt = Convert.ToInt32(read["idpdt"]);
                        this.Qt = Convert.ToInt32(read["qt"]);
                        this.Datec = Convert.ToDateTime(read["datec"]);
                    }
                    con.Close();
                }
            }
            catch (Exception ex)
            {
                ex.StackTrace.Replace(Environment.NewLine, ex.ToString());
            }

        }
        public int countcmd()
        {
            int nb = 0;
            using (SqlConnection con = new SqlConnection(Connectionstrings.Connectionstring()))
            {
                string sqlquery = "select count(*) as nb from Commande ";
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

        public override string Delete(int id)
        {
            string msg;
            try
            {
                using (SqlConnection con = new SqlConnection(Connectionstrings.Connectionstring()))
                {
                    string sqlquery = "Delete from Commande where id =" + id;
                    SqlCommand cmd = new SqlCommand(sqlquery, con);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                    msg = "Commande supprimé";
                }
            }
            catch
            {
                msg = "Commande ne peut pas être supprimé";
            }
            return msg;


        }

        public override void Update()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(Connectionstrings.Connectionstring()))
                {
                    string sqlquery = "update Commande set idclt='" + this.Idclt +
                        "', idpdt = '" + this.Idpdt + "'" +
                        ", Qt ='" + this.Qt + "'" +
                        ", Datec ='" + this.Datec + "'" +
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