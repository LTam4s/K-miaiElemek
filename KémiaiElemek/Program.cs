using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace infojegyzethu_kemiaiElemekFelfedezese
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] lines = File.ReadAllLines("felfedezesek.csv");

            List<Adatok> AdatokList = new List<Adatok>();

            for (int i = 1; i < lines.Length; i++)
            {
                Adatok Adatok = new Adatok(lines[i]);
                AdatokList.Add(Adatok);
            }

            Console.Write("3. feladat:");
            numberOfChemicalElements(AdatokList);

            Console.Write("4. feladat:");
            discoveriesInAncientTimes(AdatokList);

            Console.Write("5. feladat:");
            string vegyjel = queryAChemicalSymbolFromUser();

            Console.Write("6. feladat: Keresés");
            search(AdatokList, vegyjel);

            Console.Write("7. feladat:");
            timeSpanBetweenDiscoveries(AdatokList);

            Console.WriteLine("8. feladat: Statisztika");
            statistics(AdatokList);

            Console.ReadKey();
        }

        private static void statistics(List<Adatok> AdatokList)
        {
            int examinedDiscoverysYear = 0;

            List<int> DiscoveryYears = new List<int>();
            List<int> DiscoveryYearsDistinct = new List<int>();

            for (int i = 0; i < AdatokList.Count; i++)
            {
                if (int.TryParse(AdatokList[i].ev, out examinedDiscoverysYear))
                {
                    DiscoveryYears.Add(examinedDiscoverysYear);
                }
            }

            DiscoveryYearsDistinct = DiscoveryYears.Distinct().ToList();

            foreach (var DiscoveryYear in DiscoveryYearsDistinct)
            {
                int AdatokCounter = 0;

                for (int i = 1; i < AdatokList.Count; i++)
                {
                    if (AdatokList[i].ev == DiscoveryYear.ToString())
                    {
                        AdatokCounter++;
                    }
                }

                if (AdatokCounter >= 4)
                {
                    Console.WriteLine(DiscoveryYear + ": " + AdatokCounter + " db");
                }
            }
        }

        private static void timeSpanBetweenDiscoveries(List<Adatok> AdatokList)
        {
            List<int> timeSpanList = new List<int>();
            int examinedDiscoverysYear = 0;
            int previousDiscoverysYear = 0;

            for (int i = 1; i < AdatokList.Count; i++)
            {
                if (int.TryParse(AdatokList[i].ev, out examinedDiscoverysYear) && int.TryParse(AdatokList[i - 1].ev, out previousDiscoverysYear))
                {
                    timeSpanList.Add(examinedDiscoverysYear - previousDiscoverysYear);
                }
            }

            int maxTimeSpan = 0;

            for (int i = 0; i < timeSpanList.Count; i++)
            {
                if (timeSpanList[i] > maxTimeSpan)
                {
                    maxTimeSpan = timeSpanList[i];
                }
            }
            Console.WriteLine(" " + maxTimeSpan + " év volt a leghosszabb időszak két elem felfedezése között.");
        }

        private static void search(List<Adatok> AdatokList, string vegyjel)
        {
            bool chemicalFound = false;

            for (int i = 0; i < AdatokList.Count; i++)
            {
                if (AdatokList[i].vegyjel.ToUpper() == vegyjel.ToUpper())
                {
                    Console.WriteLine("Az elem vegyjele: " + AdatokList[i].vegyjel);
                    Console.WriteLine("Az elem neve: " + AdatokList[i].elem);
                    Console.WriteLine("Rendszáma: " + AdatokList[i].rendszam);
                    Console.WriteLine("Felfedezés éve: " + AdatokList[i].ev);
                    Console.WriteLine("Felfedező: " + AdatokList[i].felfedezo);
                }
                else
                {
                    chemicalFound = false;
                }
            }

            if (!chemicalFound)
            {
                Console.WriteLine("Nincs ilyen elem az adatbázisban!");
            }
        }

        private static string queryAChemicalSymbolFromUser()
        {
            string pattern = @"^[a-zA-Z]+$";
            Regex rx = new Regex(pattern);
            Match match;

            string vegyjel;

            do
            {
                Console.Write("Kérek egy vegyjelet: ");
                vegyjel = Console.ReadLine();

                match = rx.Match(vegyjel);

            } while (!(vegyjel.Length == 1 || vegyjel.Length == 2) && match.Success);

            return vegyjel;
        }

        private static void numberOfChemicalElements(List<Adatok> AdatokList)
        {
            Console.WriteLine(AdatokList.Count);
        }

        private static void discoveriesInAncientTimes(List<Adatok> AdatokList)
        {
            int discoveriesInAncientTimesCounter = 0;

            foreach (var Adatok in AdatokList)
            {
                if (Adatok.ev == "Ókor")
                {
                    discoveriesInAncientTimesCounter++;
                }
            }
            Console.WriteLine("Felfedezések száma az ókorban: " + discoveriesInAncientTimesCounter);
        }
    }
}