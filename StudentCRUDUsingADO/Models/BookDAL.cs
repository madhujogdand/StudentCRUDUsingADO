using System.Data.SqlClient;
namespace StudentCRUDUsingADO.Models
{
    public class BookDAL
    {
        SqlConnection con;
        SqlCommand cmd;
        SqlDataReader dr;
        private readonly IConfiguration configuration;
        public BookDAL(IConfiguration configuration)
        {
            this.configuration = configuration;
            string connstr = this.configuration.GetConnectionString("DefaultConnection");
            con = new SqlConnection(connstr);
        }


        //Display List
        public List<Book> GetBooks()
        {
            List<Book> booklist = new List<Book>();
            string qry = "select * from book";
            cmd = new SqlCommand(qry, con);
            con.Open();
            dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                   Book book = new Book();
                    book.Id = Convert.ToInt32(dr["id"]);
                    book.Name = dr["name"].ToString();
                    book.Author = dr["author"].ToString();
                    book.Price = Convert.ToDouble(dr["price"]);
                    booklist.Add(book);
                }
            }
            con.Close();
            return booklist;
        }

        //Add
        public int AddBook(Book book)
        {
            int result = 0;
            string qry = "insert into book values(@name, @author , @price)";
            cmd = new SqlCommand(qry, con);
            cmd.Parameters.AddWithValue("@name", book.Name);
            cmd.Parameters.AddWithValue("@author", book.Author);
            cmd.Parameters.AddWithValue("@price", book.Price);
            con.Open();
            result = cmd.ExecuteNonQuery();
            con.Close();
            return result;

        }

        //Edit
        public int EditBook(Book book)
        {
            int result = 0;
            string qry = "update book set name=@name, author=@author, price=@price where id=@id";
            cmd = new SqlCommand(qry, con);
            cmd.Parameters.AddWithValue("@name", book.Name);
            cmd.Parameters.AddWithValue("@author", book.Author);
            cmd.Parameters.AddWithValue("@price", book.Price);
            cmd.Parameters.AddWithValue("@id", book.Id);
            con.Open();
            result = cmd.ExecuteNonQuery();
            con.Close();
            return result;

        }

        //Select Single Book

        public Book GetBookById(int id)
        {
            Book book = new Book();
            string qry = "select * from book where id=@id";
            cmd = new SqlCommand(qry, con);
            cmd.Parameters.AddWithValue("@id", id);
            con.Open();
            dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {

                    book.Id = Convert.ToInt32(dr["id"]);
                    book.Name = dr["name"].ToString();
                    book.Author = dr["author"].ToString();
                    book.Price = Convert.ToDouble(dr["price"]);

                }
            }
            con.Close();
            return book;
        }

        //Delete

        public int DeleteBook(int id)
        {
            int result = 0;
            string qry = "delete from book where id=@id";
            cmd = new SqlCommand(qry, con);
            cmd.Parameters.AddWithValue("@id", id);
            con.Open();
            result = cmd.ExecuteNonQuery();
            con.Close();
            return result;
        }
    }
}
