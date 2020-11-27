using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace exerciciocc2.Controllers
{
   
    public class LoginController : Controller
    {
        public static String GetHash(String input)
        {
            MD5 md5Hash = MD5.Create();

            byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes("Gansim" + input + "Burro"));

            StringBuilder sBuilder = new StringBuilder();

            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            return sBuilder.ToString();
        }

        public IActionResult Index()
        {
            return View();
        }

        public bool LogarUsuario(string email, string senha)
        {
            string senhaCriptografada = GetHash(senha);
            bool retorno = new Sqlinsert.sql1().logarusuario(email, senhaCriptografada);
            return retorno;
        }
        public bool registrarUsuario(string email, string senha)
        {
            string senhaCriptografada = GetHash(senha);
            bool retorno = new Sqlinsert.sql1().registrarUsuario(email, senhaCriptografada);
            return true;
        }
    }
}

namespace Sqlinsert
{
    public partial class sql1
    {
        SqlCommand cmd;
        SqlConnection con;
        SqlDataAdapter da;
                
        public bool registrarUsuario(string email, string senha)
        {
            con = new SqlConnection(@"Data Source=DESKTOP-CV303VK\SQLEXPRESS01;Initial Catalog=ExercicioCC2;Integrated Security= true");            
            cmd = new SqlCommand("INSERT INTO usuario (email,senha) VALUES (@Email,@Senha)", con);
            cmd.Parameters.AddWithValue("@Email", email);
            cmd.Parameters.AddWithValue("@Senha", senha);
            con.Open();                        
            return cmd.ExecuteNonQuery() > 0;
        }

        public bool logarusuario (string email, string senha)
        {
            con = new SqlConnection(@"Data Source=DESKTOP-CV303VK\SQLEXPRESS01;Initial Catalog=ExercicioCC2;Integrated Security= true");            
            SqlCommand cmd = new SqlCommand("SELECT * FROM usuario WHERE email = @email AND senha = @senha;", con);
            
            if (email == "" || email == null)
                cmd.Parameters.Add(new SqlParameter("@email", "erro"));
            else
                cmd.Parameters.Add(new SqlParameter("@email", email));

            if (senha == "" || senha == null)
                cmd.Parameters.Add(new SqlParameter("@senha", "erro"));
            else
                cmd.Parameters.Add(new SqlParameter("@senha", senha));

            con.Open();

            return Convert.ToInt32(cmd.ExecuteScalar()) > 0;                        
        }

    }

}

 