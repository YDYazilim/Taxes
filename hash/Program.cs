using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace hash
{
    class Program
    {

        static void Main(string[] args)
        {

            SqlConnection baglanti = new SqlConnection("Data Source=DESKTOP-4SEP11A;Initial Catalog=Citizens;Integrated Security=SSPI;MultipleActiveResultSets=True");


            baglanti.Open();
            
       
            void ekle()
            {
                try
                {
               
                    string sifrem = "258025802qaz";
                    var hasht = SecurePasswordHasher.Hash(sifrem);
                    string sql = "insert into info(Tc, User_name, pass, email, tel) values (@tc,@isim,@pass,@email,@tel)";


                    SqlCommand komut = new SqlCommand(sql, baglanti);
                    komut.Parameters.AddWithValue("@tc", "8888");
                    komut.Parameters.AddWithValue("@isim", "denemem");
                    komut.Parameters.AddWithValue("@pass", hasht);
                    komut.Parameters.AddWithValue("@email", "hashlandi@com");
                    komut.Parameters.AddWithValue("@tel", "2222");

                    komut.ExecuteNonQuery();


                    baglanti.Close();
                    
                }
                catch (Exception hata)
                {
                    Console.WriteLine(hata);
                }
            }


            void dene()
            {
                string username = "hashlandi@com";
                string word = "258025802qaz";

                String sql = @"SELECT * FROM [info] WHERE [email] =@email";


                
                SqlCommand comm = new SqlCommand(sql, baglanti);
                comm.Parameters.AddWithValue("@email", username);

                
                SqlDataReader nwReader = comm.ExecuteReader();
                while (nwReader.Read())
                {
                    string email = (string)nwReader["email"];
                    string pass = (string)nwReader["pass"];

                    var res = SecurePasswordHasher.Verify(word, pass);

                    Console.WriteLine(res);
                    if (res == true)
                    {
                        Console.Write("tamamdir");
                    }


                }
                

                nwReader.Close();
                baglanti.Close();

               
            }
            dene();
            
            Console.Read();
        }

         
    }
}
