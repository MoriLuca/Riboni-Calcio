using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBWriter.Classes
{
    public class DBObj
    {
        public DateTime Data;
        public string Lotto;
        public string FileName;
        public int Contapezzi;
        public float Diametro;
        public float LunghezzaTaglio;
        public float RotazioneSega;
        public float TempoCiclo;


        public int WriteDBLog()
        {
            string query = $"INSERT INTO logs VALUES ('{Data.ToString("yyyy-MM-dd HH:mm:ss")}','{Lotto}',{Contapezzi},{LunghezzaTaglio},{Diametro},{RotazioneSega},{TempoCiclo})";
            Console.WriteLine(query);

            String MyConString = ConfigurationSettings.AppSettings.Get("mysqlConnector");
            using (MySqlConnection connection = new MySqlConnection(MyConString))
            {
                try
                {
                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    connection.Open();
                    return cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("not entered "+ex.Message);
                    return -1;
                }

            }
        }
    }
}