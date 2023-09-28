using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;

namespace Tema1v3.Pages
{
    public class IndexModel : PageModel
    {
        public List<Product> products = new List<Product>();
        public int ItemCount { get; set; }

        public void OnGet()
        {
            HttpContext.Session.SetString("UserName", "John");
            HttpContext.Session.SetInt32("UserId", 50);
            int userId = HttpContext.Session.GetInt32("UserId") ?? 0;

            string connectionString = "Data Source=KAIS;Initial Catalog=demo;Integrated Security=True"; // Veritabanı bağlantı dizesi
            string query = "SELECT * FROM products"; // Veri çekme SQL sorgusu


            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string query2 = "SELECT COUNT(*) FROM UserCart WHERE user_id = @UserId";

                using (SqlCommand command = new SqlCommand(query2, connection))
                {
                    command.Parameters.AddWithValue("@UserId", userId);
                    ItemCount = (int)command.ExecuteScalar();
                }

                using (SqlCommand command = new SqlCommand(query, connection))
                {

                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        Product product = new Product
                        {
                            ProductId = (int)reader["product_id"],
                            ProductTitle = reader["product_title"].ToString(),
                            ProductDescription = reader["product_description"].ToString(),
                            ProductUrl = reader["product_Url"].ToString(),
                            ProductPrice = (decimal)reader["product_price"],
                            ProductLike = (int)reader["product_like"],
                            ProductShare = (int)reader["product_share"]
                        };
                        products.Add(product);
                    }
                }
            }
    }
}

    public class Product
    {
        public int ProductId { get; set; }
        public string? ProductTitle { get; set; }
        public string? ProductDescription { get; set; }
        public string? ProductUrl { get; set; }
        public decimal ProductPrice { get; set; }
        public int ProductLike { get; set; }
        public int ProductShare { get; set; }
    }
}
