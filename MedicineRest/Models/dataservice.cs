using System.Data.SqlClient;
using static MedicineRest.Models.securityservic;
//using static MedicineRest.Models.securityservic;

namespace MedicineRest.Models
{
    public class dataservice
    {
        IConfiguration config;
        SqlConnection con;
        SqlCommand? cmd;
        public dataservice(IConfiguration _config)
        {
            this.config = _config;
            con = new SqlConnection();
            con.ConnectionString = config["ConnectionStrings:cstr"];
        }



        public string? FindUser(OperatorLoginModel model)
        {
            string? role = null;
            cmd = new SqlCommand();
            cmd.Connection = con;


            cmd.CommandText = "select role from Users where username=@user and password=@pwd";



            cmd.Parameters.AddWithValue("@user", model.Username);
            cmd.Parameters.AddWithValue("@pwd", model.Password);
            /*cmd.CommandText = "select Email from Users where Email=@email and Password=@pwd";



            cmd.Parameters.AddWithValue("@email", model.Email);
            cmd.Parameters.AddWithValue("@pwd", model.Password);*/
            con.Open();
            var result = cmd.ExecuteScalar();
            if (result != null) { role = result.ToString(); }
            con.Close();
            return role;
        }
    }
}