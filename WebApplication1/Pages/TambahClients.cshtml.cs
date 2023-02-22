using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;
using static WebApplication1.Pages.ClientsModel;

namespace WebApplication1.Pages
{
    public class TambahClientsModel : PageModel
    {

        public ClientsInfo clientsinfo = new ClientsInfo();
        public String pesanError = "";
        public String pesanSukses = "";

        public void OnGet()
        {
            
        }

        public void OnPost() {
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
                        String q = "INSERT INTO clients " +
                                   "(nama, email, telepon, alamat) VALUES " +
                                   "(@nama, @email, @telepon, @alamat)";

                        using (SqlCommand command = new SqlCommand(q, c))
                        {
                            command.Parameters.AddWithValue("@nama", clientsinfo.nama);
                            command.Parameters.AddWithValue("@email", clientsinfo.email);
                            command.Parameters.AddWithValue("@telepon", clientsinfo.telepon);
                            command.Parameters.AddWithValue("@alamat", clientsinfo.alamat);

                            command.ExecuteNonQuery();
                        }
                        clientsinfo.nama = "";
                        clientsinfo.email = "";
                        clientsinfo.telepon = "";
                        clientsinfo.alamat = "";
                        pesanSukses = "Data Berhasil Ditambahkan";

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
