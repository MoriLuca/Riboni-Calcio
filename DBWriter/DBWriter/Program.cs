using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DBWriter
{
    class Program
    {
        static void Main(string[] args)
        {
            System.Globalization.CultureInfo customCulture = (System.Globalization.CultureInfo)System.Threading.Thread.CurrentThread.CurrentCulture.Clone();
            customCulture.NumberFormat.NumberDecimalSeparator = ".";

            System.Threading.Thread.CurrentThread.CurrentCulture = customCulture;

            while (true)
            {
                Classes.CSVReader csv = new Classes.CSVReader();
                List<string> s = csv.Start();
                List<Classes.DBObj> objs = new List<Classes.DBObj>();
                foreach (string txt in s)
                {
                    try
                    {
                        List<string> rec = txt.Split(',').ToList();
                        objs.Add(new Classes.DBObj()
                        {
                            Data = DateTime.Parse(rec[1]),
                            Lotto = rec[8].Replace("\"", ""),
                            Contapezzi = Int32.Parse(rec[7]),
                            Diametro = (float)(Int32.Parse(rec[11]) / 100F),
                            LunghezzaTaglio = (float)(Int32.Parse(rec[6]) / 100F),
                            RotazioneSega = (float)(Int32.Parse(rec[10])),
                            TempoCiclo = (float)(Int32.Parse(rec[3]) / 10F),
                            FileName = rec[0]
                        });
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }


                }

                foreach (Classes.DBObj o in objs)
                {
                    if (o.WriteDBLog() == 1) csv.CSVKiller(o.FileName);
                }
                Thread.Sleep(10000);
            }

            //Console.Read();
        }
    }
}
