using System;
using System.Collections.Generic;
using System.Text;

namespace infojegyzethu_kemiaiElemekFelfedezese
{
    class Adatok
    {
        public string ev { get; set; }
        public string elem { get; set; }
        public string vegyjel { get; set; }
        public int rendszam { get; set; }
        public string felfedezo { get; set; }

        public Adatok(string line)
        {
            string[] AdatSplit = line.Split(';');
            ev = AdatSplit[0];
            elem = AdatSplit[1];
            vegyjel = AdatSplit[2];
            rendszam = Convert.ToInt32(AdatSplit[3]);
            felfedezo = AdatSplit[4];
        }
    }
}