using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace Tema1v3.Pages
{
    public class SepetModel : PageModel
    {
        public int ItemCount { get; set; }
        public string errorMessage2 = "";
        public List<UserCart> userCarts = new List<UserCart>();
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
                    string sql = "SELECT * FROM UserCart WHERE user_id = @UserId";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@UserId", user_id);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                // Verileri alýn ve web sitesinde görüntülemek için kullanabilirsiniz
                                int CartId = Convert.ToInt32(reader["cart_id"]);
                                string productId = reader["product_id"].ToString();

                                userCarts.Add(new UserCart
                                {
                                    Id = CartId,
                                    product_id = productId,
                                    // Diðer özellikleri de burada ayarlayabilirsiniz
                                });


                            }
                        }

                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                errorMessage2 = ex.Message;
            }
        }
    }

    public class UserCart
    {
        public int Id { get; set; }
        public string? user_id { get; set; }
        public string? product_id{ get; set; }
        public string? quantity { get; set; }
        public decimal price { get; set; }
    }
}
