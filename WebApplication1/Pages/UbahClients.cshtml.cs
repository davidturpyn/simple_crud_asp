using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;
using static WebApplication1.Pages.ClientsModel;

namespace WebApplication1.Pages
{
    public class UbahClientsModel : PageModel
    {
        public ClientsInfo clientsinfo = new ClientsInfo();
        public String pesanError = "";
        public String pesanSukses = "";
        
        public void OnGet()
        {
            String id = Request.Query["id"];

            try
            {
                String connectionString = "Data Source=.\\sqlexpress;Initial Catalog=belajar_crud_asp;Integrated Security=True";

                using (SqlConnection c = new SqlConnection(connectionString))
                {
                    c.Open();
                    String q = "SELECT * FROM clients WHERE id = @id";

                    using (SqlCommand command = new SqlCommand(q, c))
                    {
                        command.Parameters.AddWithValue("@id", id); 
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                clientsinfo.id = "" + reader.GetInt32(0);
                                clientsinfo.nama = reader.GetString(1);
                                clientsinfo.email = reader.GetString(2);
                                clientsinfo.telepon = reader.GetString(3);
                                clientsinfo.alamat = reader.GetString(4);
                                clientsinfo.created_at = reader.GetDateTime(5).ToString();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                pesanError = ex.Message;
            }
        }

        public void OnPost()
        {
            clientsinfo.id = Request.Form["id"];
            clientsinfo.nama = Request.Form["nama"];
            clientsinfo.email = Request.Form["email"];
            clientsinfo.telepon = Request.Form["telepon"];
            clientsinfo.alamat = Request.Form["alamat"];

            if (clientsinfo.nama.Length == 0 || clientsinfo.email.Length == 0 || clientsinfo.telepon.Length == 0 || clientsinfo.alamat.Length == 0)
            {
                pesanError = "Semua data harus diisi";
                return;
            }
            else
            {
                try
                {
                    String connectionString = "Data Source=.\\sqlexpress;Initial Catalog=belajar_crud_asp;Integrated Security=True";

                    using (SqlConnection c = new SqlConnection(connectionString))
                    {
                        c.Open();
                        String q = "UPDATE clients " +
                                   "SET nama=@nama, email=@email, telepon=@telepon, alamat=@alamat " +
                                   "WHERE id=@id";

                        using (SqlCommand command = new SqlCommand(q, c))
                        {
                            command.Parameters.AddWithValue("@nama", clientsinfo.nama);
                            command.Parameters.AddWithValue("@email", clientsinfo.email);
                            command.Parameters.AddWithValue("@telepon", clientsinfo.telepon);
                            command.Parameters.AddWithValue("@alamat", clientsinfo.alamat);
                            command.Parameters.AddWithValue("@id", clientsinfo.id);


                            command.ExecuteNonQuery();
                        }
                        pesanSukses = "Data Berhasil DIubah";

                        // Response.Redirect("/Clients");

                    }
                }
                catch (Exception ex)
                {
                    pesanError = ex.Message;
                    return;
                }
            }
        }
    }
}
