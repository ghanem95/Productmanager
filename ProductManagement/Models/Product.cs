using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace ProductManagement.Models
{
    public class Product : Crud
    {
        private int id;
        private string reference;
        private string name;
        private string desc;
        private float price;
        private int type;
        private string desctype;
        private int qt;
        bool available;
        DateTime datefab;

        public int Id { get => id; set => id = value; }
        public string Reference { get => reference; set => reference = value; }
        public string Name { get => name; set => name = value; }
        public string Desc { get => desc; set => desc = value; }
        public float Price { get => price; set => price = value; }
        public int Type { get => type; set => type = value; }
        public int Qt { get => qt; set => qt = value; }
        public bool Available { get => available; set => available = value; }
        public DateTime Datefab { get => datefab; set => datefab = value; }
        public string Desctype { get => desctype; set => desctype = value; }

        public override void Add()
        {
            try
            {
                SqlConnection connect = new SqlConnection(Connectionstrings.Connectionstring());
                SqlCommand cmd = connect.CreateCommand();
                cmd.CommandText = "Execute addproduct @ref,@name,@description,@datefab,@type,@price,@qt,@available";
                cmd.Parameters.Add("@ref", SqlDbType.NVarChar, 50).Value = this.Reference;
                cmd.Parameters.Add("@name", SqlDbType.NVarChar, 50).Value = this.Name;
                cmd.Parameters.Add("@description", SqlDbType.NVarChar).Value = this.Desc.isNull(string.Empty);
                cmd.Parameters.Add("@datefab", SqlDbType.Date).Value = this.Datefab;
                cmd.Parameters.Add("@type", SqlDbType.Int).Value = this.Type;
                cmd.Parameters.Add("@price", SqlDbType.Float, 50).Value = this.Price;
                cmd.Parameters.Add("@qt", SqlDbType.Int).Value = this.Qt;
                cmd.Parameters.Add("@available", SqlDbType.Decimal).Value = Convert.ToDecimal(this.Available);
                connect.Open();
                cmd.ExecuteNonQuery();
                connect.Close();
            }
            catch (Exception ex)
            {
                ex.StackTrace.Replace(Environment.NewLine, ex.ToString());
            }

        }
        public int countpdt()
        {
            int nb = 0;
            using (SqlConnection con = new SqlConnection(Connectionstrings.Connectionstring()))
            {
                string sqlquery = "select count(*) as nb from Product ";
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
        public IList<Product> List()
        {

            List<Product> Listpdt = new List<Product>();
            using (SqlConnection con = new SqlConnection(Connectionstrings.Connectionstring()))
            {
                try
                {
                    string sqlquery = "select p.id,ref,name,p.description,datefab,t.type as desctype,p.type,price,qt,available from product p" +
                                      " inner join typeproduct t on t.id = p.type";
                    SqlCommand cmd = new SqlCommand(sqlquery, con);
                    con.Open();
                    SqlDataReader read = cmd.ExecuteReader();
                    while (read.Read())
                    {
                        Product product = new Product();
                        product.Id = Convert.ToInt32(read["id"]);
                        product.Reference = read["ref"].ToString();
                        product.Name = read["name"].ToString();
                        product.Desc = read["description"].ToString();
                        product.Datefab = Convert.ToDateTime(read["datefab"]);
                        product.Type = Convert.ToInt32(read["type"]);
                        product.Desctype = read["desctype"].ToString();
                        product.Price = Convert.ToInt32(read["price"]);
                        product.Qt = Convert.ToInt32(read["qt"]);
                        product.Available = Convert.ToBoolean(read["available"]);
                        Listpdt.Add(product);

                    }
                    con.Close();
                }
                catch (Exception ex)
                {
                    ex.StackTrace.Replace(Environment.NewLine, ex.ToString());
                }
            }

            return Listpdt;
        }
        public override void Affiche(int id)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(Connectionstrings.Connectionstring()))
                {
                    string sqlquery = "select * from Product where id=" + id;
                    SqlCommand cmd = new SqlCommand(sqlquery, con);
                    con.Open();
                    SqlDataReader read = cmd.ExecuteReader();
                    while (read.Read())
                    {

                        this.Id = Convert.ToInt32(read["id"]);
                        this.Reference = read["ref"].ToString();
                        this.Name = read["name"].ToString();
                        this.Desc = read["description"].ToString();
                        this.Datefab = Convert.ToDateTime(read["datefab"]);
                        this.Type = Convert.ToInt32(read["type"]);
                        this.Price = Convert.ToInt32(read["price"]);
                        this.Qt = Convert.ToInt32(read["qt"]);
                        this.Available = Convert.ToBoolean(read["available"]);
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
                    string sqlquery = "Delete from Product where id =" + id;
                    SqlCommand cmd = new SqlCommand(sqlquery, con);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                    msg = "Produit supprimé";

                }
            }
            catch (Exception ex)
            {
                msg = "Produit ne peut pas être supprimé";
            }

            return msg;
        }
        public override void Update()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(Connectionstrings.Connectionstring()))
                {
                    string sqlquery = "update Product set Ref='" + this.Reference +
                        "', Name = '" + this.Name + "'" +
                        ", Description ='" + this.Desc + "'" +
                        ", datefab ='" + this.Datefab + "'" +
                        ", Type ='" + this.Type + "'" +
                        ", Price ='" + this.Price + "'" +
                        ", Qt ='" + this.Qt + "'" +
                        ", Available ='" + Convert.ToDecimal(this.Available) + "'" +
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