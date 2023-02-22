using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace WebApplication1.Pages
{
    public class ClientsModel : PageModel
    {
        public List<ClientsInfo> listClients = new List<ClientsInfo>();
        public void OnGet()
        {
            try
            {
                String connectionString = "Data Source=.\\sqlexpress;Initial Catalog=belajar_crud_asp;Integrated Security=True";

                using (SqlConnection c = new SqlConnection(connectionString))
                {
                    c.Open();
                    String q = "SELECT * FROM clients";

                    using (SqlCommand command = new SqlCommand(q, c))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                ClientsInfo clientinfo = new ClientsInfo();
                                clientinfo.id = "" + reader.GetInt32(0);
                                clientinfo.nama = reader.GetString(1);
                                clientinfo.email = reader.GetString(2);
                                clientinfo.telepon = reader.GetString(3);
                                clientinfo.alamat = reader.GetString(4);
                                clientinfo.created_at = reader.GetDateTime(5).ToString();

                                listClients.Add(clientinfo);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.ToString());
            }
        }

        public class ClientsInfo
        {
            public string id;
            public string nama;
            public string email;
            public string telepon;
            public string alamat;
            public string created_at;
        }
    }
}
