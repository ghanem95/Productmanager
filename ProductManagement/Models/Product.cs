using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
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
        DateTime datefab;

        public int Id { get => id; set => id = value; }
        public string Reference { get => reference; set => reference = value; }
        public string Name { get => name; set => name = value; }
        public string Desc { get => desc; set => desc = value; }
        public float Price { get => price; set => price = value; }
        public int Type { get => type; set => type = value; }
        public int Qt { get => qt; set => qt = value; }
        public DateTime Datefab { get => datefab; set => datefab = value; }
        public string Desctype { get => desctype; set => desctype = value; }


        public IList<Product> List()
        {

            List<Product> Listpdt = new List<Product>();
            using (SqlConnection con = new SqlConnection(Connectionstrings.Connectionstring()))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("Listproducts", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    SqlDataReader read = cmd.ExecuteReader();
                    while (read.Read())
                    {
                        Product product = new Product();
                        product.Id = Convert.ToInt32(read["id"]);
                        product.Name = read["name"].ToString();
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
        public int CountListDatatable(int length, int start, string searchVal, string tri, string column)
        {
            int nbr = 0;
            using (SqlConnection conn = new SqlConnection(Connectionstrings.Connectionstring()))
            {
                try
                {
                    column = (Convert.ToInt32(column) + 1).ToString();
                    column = column.Replace("1", "id").Replace("2", "ref").Replace("3", "name").Replace("4", "description")
                        .Replace("5", "datefab").Replace("6", "type").Replace("7", "price").Replace("8", "qt");
                    SqlCommand cmd = new SqlCommand("countdatatableproducts", conn);
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
        public IList<Product> ListDatatable(int length, int start, string searchVal, string tri, string column)
        {
            List<Product> clients = new List<Product>();
            using (SqlConnection conn = new SqlConnection(Connectionstrings.Connectionstring()))
            {
                try
                {
                    column = column.Replace("0", "pdt.id").Replace("1", "pdt.ref").Replace("2", "pdt.name").Replace("3", "pdt.description")
                    .Replace("4", "pdt.datefab").Replace("5", "t.type").Replace("6", "pdt.price").Replace("7", "pdt.qt");
                    SqlCommand cmd = new SqlCommand("selectproducts", conn);
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
                        clients.Add(new Product()
                        {
                            Id = Convert.ToInt32(reader["Id"]),
                            Reference = reader["Ref"].ToString(),
                            Name = reader["Name"].ToString(),
                            Datefab = Convert.ToDateTime(reader["Datefab"]),
                            Desc = reader["Description"].ToString(),
                            Desctype= reader["desctype"].ToString(),
                            Type = Convert.ToInt32(reader["Type"]),
                            Price = Convert.ToInt32(reader["Price"]),
                            Qt = Convert.ToInt32(reader["Qt"]),
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
                SqlCommand cmd = new SqlCommand("addproduct", connect);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ref", this.Reference.isNull(string.Empty));
                cmd.Parameters.AddWithValue("@name", this.Name.isNull(string.Empty));
                cmd.Parameters.AddWithValue("@description", this.Desc.isNull(string.Empty));
                cmd.Parameters.AddWithValue("@datefab", this.Datefab);
                cmd.Parameters.AddWithValue("@type", this.Type);
                cmd.Parameters.AddWithValue("@price", this.Price);
                cmd.Parameters.AddWithValue("@qt", this.Qt);
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
                SqlCommand cmd = new SqlCommand("countproducts", con);
                cmd.CommandType = CommandType.StoredProcedure;
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
        public override void Affiche(int id)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(Connectionstrings.Connectionstring()))
                {
                    SqlCommand cmd = new SqlCommand("selectproductbyid", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id", id);
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
                using (SqlConnection connect = new SqlConnection(Connectionstrings.Connectionstring()))
                {
                    SqlCommand cmd = new SqlCommand("Deleteproduct", connect);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id", id);
                    connect.Open();
                    int rowAffected = cmd.ExecuteNonQuery();
                    connect.Close();
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
                using (SqlConnection connect = new SqlConnection(Connectionstrings.Connectionstring()))
                {
                    SqlCommand cmd = new SqlCommand("updateproduct", connect);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id", this.Id);
                    cmd.Parameters.AddWithValue("@ref", this.Reference.isNull(string.Empty));
                    cmd.Parameters.AddWithValue("@name", this.Name.isNull(string.Empty));
                    cmd.Parameters.AddWithValue("@Description", this.Desc.isNull(string.Empty));
                    cmd.Parameters.AddWithValue("@datefab", this.Datefab);
                    cmd.Parameters.AddWithValue("@type", this.Type);
                    cmd.Parameters.AddWithValue("@price", this.Price);
                    cmd.Parameters.AddWithValue("@qt", this.Qt);
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