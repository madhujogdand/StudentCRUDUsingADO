using System.Data.SqlClient;
namespace StudentCRUDUsingADO.Models
{
    public class ProductDAL
    {
        SqlConnection con;
        SqlCommand cmd;
        SqlDataReader dr;
        private readonly IConfiguration configuration;
        public ProductDAL(IConfiguration configuration)
        {
            this.configuration = configuration;
            string connstr = this.configuration.GetConnectionString("DefaultConnection");
            con = new SqlConnection(connstr);
        }


        //Display List
        public List<Product> GetProducts()
        {
            List<Product> productlist = new List<Product>();
            string qry = "select * from product";
            cmd = new SqlCommand(qry, con);
            con.Open();
            dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    Product product = new Product();
                    product.Id = Convert.ToInt32(dr["id"]);
                    product.Name = dr["name"].ToString();
                    product.Price = Convert.ToDouble(dr["price"]);
                    productlist.Add(product);
                }
            }
            con.Close();
            return productlist;
        }

        //Add
        public int AddProduct(Product product)
        {
            int result = 0;
            string qry = "insert into product values(@name, @price)";
            cmd = new SqlCommand(qry, con);
            cmd.Parameters.AddWithValue("@name", product.Name);
            cmd.Parameters.AddWithValue("@price", product.Price);
            con.Open();
            result = cmd.ExecuteNonQuery();
            con.Close();
            return result;

        }

        //Edit
        public int EditProduct(Product product)
        {
            int result = 0;
            string qry = "update product set name=@name, price=@price where id=@id";
            cmd = new SqlCommand(qry, con);
            cmd.Parameters.AddWithValue("@name", product.Name);
            cmd.Parameters.AddWithValue("@price", product.Price);
            cmd.Parameters.AddWithValue("@id", product.Id);
            con.Open();
            result = cmd.ExecuteNonQuery();
            con.Close();
            return result;

        }

        //Select Single Product

        public Product GetProductById(int id)
        {
            Product product = new Product();
            string qry = "select * from product where id=@id";
            cmd = new SqlCommand(qry, con);
            cmd.Parameters.AddWithValue("@id", id);
            con.Open();
            dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {

                    product.Id = Convert.ToInt32(dr["id"]);
                    product.Name = dr["name"].ToString();
                    product.Price = Convert.ToDouble(dr["price"]);

                }
            }
            con.Close();
            return product;
        }

        //Delete

        public int DeleteProduct(int id)
        {
            int result = 0;
            string qry = "delete from product where id=@id";
            cmd = new SqlCommand(qry, con);
            cmd.Parameters.AddWithValue("@id", id);
            con.Open();
            result = cmd.ExecuteNonQuery();
            con.Close();
            return result;
        }
    }
}
