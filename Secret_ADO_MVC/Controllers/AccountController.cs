using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace Secret_ADO_MVC.Controllers
{
    public class AccountController : Controller
    {
        SqlConnection _connect;
        string _connectionString = @"Data Source=SHANGHAIK;Initial Catalog=DEMOADO;Integrated Security=True;TrustServerCertificate=True";
        public AccountController()
        {
            _connect = new SqlConnection(_connectionString);
        }
        public IActionResult Login(string username, string password)
        {
            if(username == null || password == null)
            {
                ViewData["message"] = null;
                return View();
            }
            string query = $"select * from Account where username = '{username}' and password = '{password}'";
            SqlCommand cmd = new SqlCommand(query, _connect);
            try
            {
                _connect.Open(); // Mở kết nối
                // ExecuteNonQuery(); // dành cho các truy vấn có tác động lên các bản ghi (Insert, Update, Delete) return số row affected
                // ExecuteScalar(); // trả về giá trị là cột đầu tiên của dòng đầu tiên là truy vấn lấy ra được
                // ExecuteReader(); // Lấy tất cả dữ liệu và cho phép đọc liên tục
                var user = cmd.ExecuteScalar();
                if (user != null)
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ViewData["message"] = "Đặng nhập thất bại";
                    return View();
                }
            }
            catch (Exception e)
            {
                return Content(e.Message);
            }
            finally
            {
                _connect.Close();
            }
        }
        public IActionResult SignUp(string username, string password, int role)
        {
            if (username == null && password == null)
            {
                return View();
            }
            else
            {
                string query = $"insert into Account values('{username}','{password}', {role})";
                SqlCommand cmd = new SqlCommand(query, _connect);
                try
                {
                    _connect.Open(); // Mở kết nối
                                     // ExecuteNonQuery(); // dành cho các truy vấn có tác động lên các bản ghi (Insert, Update, Delete) return số row affected
                                     // ExecuteScalar(); // trả về giá trị là cột đầu tiên của dòng đầu tiên là truy vấn lấy ra được
                                     // ExecuteReader(); // Lấy tất cả dữ liệu và cho phép đọc liên tục
                    var user = cmd.ExecuteNonQuery();
                    if (user == 1) // Nếu Insert được 1 bản ghi
                    {
                        return RedirectToAction("Login", "Account");
                    }
                    else
                    {
                        ViewData["message"] = "Đăng ký thất bại";
                        return View();
                    }
                }
                catch (Exception e)
                {
                    return Content(e.Message);
                }
                finally
                {
                    _connect.Close();
                }
            }
        }
        public IActionResult CreateTable(string tablename)
        {
            if (tablename == null) return View();
            string query = $"create table {tablename} (id int identity(1,1) primary key, ten nvarchar(50))";
            SqlCommand cmd = new SqlCommand(query, _connect);
            try
            {
                _connect.Open(); // Mở kết nối
                                 // ExecuteNonQuery(); // dành cho các truy vấn có tác động lên các bản ghi (Insert, Update, Delete) return số row affected
                                 // ExecuteScalar(); // trả về giá trị là cột đầu tiên của dòng đầu tiên là truy vấn lấy ra được
                                 // ExecuteReader(); // Lấy tất cả dữ liệu và cho phép đọc liên tục
                var done = cmd.ExecuteReader();
                if (done != null) // Nếu Insert được 1 bản ghi
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                 {
                    ViewData["message"] = "Tạo thất bại";
                    return View();
                }
            }
            catch (Exception e)
            {
                return Content(e.Message);
            }
            finally
            {
                _connect.Close();
            }
        }
    }
}
