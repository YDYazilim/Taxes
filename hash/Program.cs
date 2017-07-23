using System;
using System.Collections.Generic;
using System.Data;
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

            
            void kapak()
            {
                //database class
                database db = new database();

                SqlConnection baglanti = new SqlConnection("Data Source=DESKTOP-4SEP11A;Initial Catalog=Citizens;Integrated Security=SSPI;MultipleActiveResultSets=True");
                //string tc = "99559453436";
                //var dr = db.GetDataRow("SELECT *  FROM tbl_citizens where tc_no= '" + tc + "' ");
                //Console.WriteLine(dr["email"].ToString());
                ////database class






                baglanti.Open();

                string TC = "00559453436";

                string resss = "258025802vergtgtgitabani.com";
                var passs = SecurePasswordHasher.Hash(resss);

                string mail = "buneeradi@com";

                string phone = "5050505";

                DataTable sql5 = db.GetDataTable("select * from tbl_citizens");




                if (sql5.Rows.Count > 0)
                {
                    foreach (DataRow dvr in sql5.Rows)
                    {
                        Console.Write(Environment.NewLine + dvr["email"].ToString());
                    }
                }



                var sql = db.GetDataSet("INSERT INTO tbl_citizens (tc_no, password, email, phone_number) values  ('" + TC + "' , '" + passs + "', '" + mail + "' ,'" + phone + "' ) ");

            }

            void ekle()
            {
                try
                {
               
                    string sifrem = "258frd802qaz";
                    
                    string sql2 = "insert into tbl_citizens(tc_no, password, email, phone_number) values (@tc,@pass,@email,@tel)";


                    SqlCommand komut2 = new SqlCommand(sql2, baglanti);
                    komut2.Parameters.AddWithValue("@tc", "77559453436");
                  
                    komut2.Parameters.AddWithValue("@pass", sifrem);
                    komut2.Parameters.AddWithValue("@email", "hashlandi@com");
                    komut2.Parameters.AddWithValue("@tel", "2222");

                    komut2.ExecuteNonQuery();


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

                String sql = @"SELECT * FROM [tbl_citizen_info] WHERE [citizen_email] =@email";
                SqlCommand comm = new SqlCommand(sql, baglanti);
                comm.Parameters.AddWithValue("@email", username);

                SqlDataReader nwReader = comm.ExecuteReader();

                while (nwReader.Read())
                {
                    string email = (string)nwReader["citizen_email"];
                    string pass = (string)nwReader["citizen_password"];

                    var res = SecurePasswordHasher.Verify(word, pass);

                    //Console.WriteLine(res);
                    if (res == true)
                    {
                        Console.Write(Environment.NewLine + "Sifre Dogrudur...");
                    }


                }


                nwReader.Close();
                baglanti.Close();


            }
            

            Console.Read();
        }

         
    }
}
