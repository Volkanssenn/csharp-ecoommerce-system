using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data;
using System.Data.SqlClient;

namespace Tema1v3.Pages
{
    public class addCartModel : PageModel
    {
        public string errorMessage = "";
        public string errorMessage2 = "";
        public int ItemCount { get; set; }

        public void OnGet()
        {
            string urun_id = Request.Query["urun_id"];
            string urun_fiyat = Request.Query["urun_fiyat"];
            string user_id = Request.Query["user_id"];
            errorMessage2 = "girdi";

            try
            {
                string connectionString = "Data Source=KAIS;Initial Catalog=demo;Integrated Security=True";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string sql = "INSERT INTO UserCart " +
                        "(user_id, product_id, quantity, " +
                        "price) VALUES (@UserId, @ProductId, 1, @ProductPrice)";
                    using (SqlCommand cmd = new SqlCommand(sql, connection))
                    {
                        cmd.Parameters.AddWithValue("@UserId", user_id);
                        cmd.Parameters.AddWithValue("@ProductId", urun_id);
                        cmd.Parameters.Add("@ProductPrice", SqlDbType.Decimal).Value = urun_fiyat;


                        cmd.ExecuteNonQuery();
                        

                    }
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
            }
            Response.Redirect("Index");
        }
    }
    }

