using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Threading;

namespace _2019EmeltMajusNASA
{
    class Keres
    {
        public static List<Keres> Adat = new List<Keres>();

        public int Sor;
        public string Cim;
        public string DatumIdo;
        public string Get;
        public string HttpKod;
        public string Meret;

        public Keres(int sor, string cim, string datumIdo, string get, string httpKod, string meret)
        {
            Sor = sor;
            Cim = cim;
            DatumIdo = datumIdo;
            Get = get;
            HttpKod = httpKod;
            Meret = meret;
        }

        public static void NegyedikFeladat(string fajl)
        {
            int sor = 0;

            using (StreamReader olvas = new StreamReader(fajl))
            {
                while (!olvas.EndOfStream)
                {
                    sor++;

                    string[] split = olvas.ReadLine().Split('*');
                    string cim = split[0];
                    string datumIdo = split[1];
                    string get = split[2];
                    string httpKodMeret = split[3];
                    string[] splitKod = httpKodMeret.Split(' ');
                    string httpKod = splitKod[0];
                    string meret = splitKod[1];

                    Keres keres = new Keres(sor, cim, datumIdo, get, httpKod, meret);

                    Adat.Add(keres);
                }
            }
        }

        public static void OtodikFeladat() => Console.WriteLine($"5. feladat: Kérések száma: {Adat.Count()}");

        public static int ByteMeret()
        {
            int ossz = 0;

            for (int i = 0; i < Adat.Count; i++)
            {
                if (Adat[i].Meret.ToString() != "-")
                {
                    ossz += Convert.ToInt32(Adat[i].Meret);
                }
            }

            return ossz;
        }

        public static void HatodikFeladat() => Console.WriteLine($"6. feladat: Válaszok összes mérete: {ByteMeret()} byte");

        public static bool Domain(string domain)
        {
            if (Char.IsDigit(domain[domain.Length - 1]))
                return false;
            else
                return true;
        }

        public static void NyolcadikFeladat()
        {
            double domainDb = 0;

            for (int i = 0; i < Adat.Count; i++)
            {
                if (Domain(Adat[i].Cim))
                    domainDb++;
            }

            Console.WriteLine($"8. feladat: Domain-es kérések: {Math.Round(((double)domainDb / (double)Adat.Count) * 100, 2)}%");
        }

        public static void KilencedikFeladat()
        {
            Console.WriteLine("9. feladat: Statisztika: ");

            SortedDictionary<string, int> rendezett = new SortedDictionary<string, int>();

            for (int i = 0; i < Adat.Count; i++)
            {
                if (rendezett.ContainsKey(Adat[i].HttpKod))
                    rendezett[Adat[i].HttpKod]++;
                else
                    rendezett[Adat[i].HttpKod] = 1;
            }

            foreach (var kod in rendezett)
            {
                Console.WriteLine($"\t{kod.Key} {kod.Value}");
            }
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            Feladatok();

            Console.ReadKey();
        }

        private static void Feladatok()
        {
            Keres.NegyedikFeladat(@"NASAlog.txt");
            Keres.OtodikFeladat();
            Keres.HatodikFeladat();
            Keres.NyolcadikFeladat();
            Keres.KilencedikFeladat();
        }
    }
}
