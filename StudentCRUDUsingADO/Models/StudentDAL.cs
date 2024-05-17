using System.Data.SqlClient;

namespace StudentCRUDUsingADO.Models
{
    public class StudentDAL
    {
        SqlConnection con;
        SqlCommand cmd;
        SqlDataReader dr;
        private readonly IConfiguration configuration;
        public StudentDAL(IConfiguration configuration)
        {
            this.configuration = configuration;
            string connstr = this.configuration.GetConnectionString("DefaultConnection");
            con = new SqlConnection(connstr);
        }


        //Display List
        public List<Student> GetStudents() 
        {
           List<Student> studentlist = new List<Student>();
            string qry = "select * from student";
            cmd=new SqlCommand(qry, con);
            con.Open();
            dr= cmd.ExecuteReader();
            if(dr.HasRows) 
            {
               while(dr.Read()) 
                {
                  Student student = new Student();
                    student.RollNo = Convert.ToInt32(dr["rollno"]);
                    student.Name = dr["name"].ToString();
                    student.Course = dr["course"].ToString();
                    student.Fees = Convert.ToDouble(dr["fees"]);
                    studentlist.Add(student);
                }
            }
            con.Close();
            return studentlist;
        }

        //Add
        public int AddStudent(Student stud)
        {
            int result = 0;
            string qry = "insert into student values(@name, @course, @fees)";
            cmd=new SqlCommand(qry, con);
            cmd.Parameters.AddWithValue("@name", stud.Name);
            cmd.Parameters.AddWithValue("@course", stud.Course);
            cmd.Parameters.AddWithValue("@fees", stud.Fees);
            con.Open();
            result=cmd.ExecuteNonQuery();
            con.Close();
            return result;

        }

        //Edit
        public int EditStudent(Student stud)
        {
            int result = 0;
            string qry = "update student set name=@name, course=@course, fees=@fees where rollno=@rollno";
            cmd = new SqlCommand(qry, con);
            cmd.Parameters.AddWithValue("@name", stud.Name);
            cmd.Parameters.AddWithValue("@course", stud.Course);
            cmd.Parameters.AddWithValue("@fees", stud.Fees);
            cmd.Parameters.AddWithValue("@rollno", stud.RollNo);
            con.Open();
            result = cmd.ExecuteNonQuery();
            con.Close();
            return result;

        }

        //Select Single Student

        public Student GetStudentByRollNo(int rollno)
        {
            Student student = new Student();
            string qry = "select * from student where rollno=@rollno";
            cmd = new SqlCommand(qry, con);
            cmd.Parameters.AddWithValue("@rollno", rollno);
            con.Open();
            dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                  
                    student.RollNo = Convert.ToInt32(dr["rollno"]);
                    student.Name = dr["name"].ToString();
                    student.Course = dr["course"].ToString();
                    student.Fees = Convert.ToDouble(dr["fees"]);
                 
                }
            }
            con.Close();
            return student;
        }

        //Delete

        public int DeleteStudent(int rollno)
        {
            int result = 0;
            string qry = "delete from student where rollno=@rollno";
            cmd = new SqlCommand(qry, con);
            cmd.Parameters.AddWithValue("@rollno", rollno);
            con.Open();
            result = cmd.ExecuteNonQuery();
            con.Close();
            return result;
        }
    }
}
