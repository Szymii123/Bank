﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank
{
    public class KontoLimit
    {
        private Konto konto;
        private decimal limitDebetowy;

        public KontoLimit(string klient, decimal bilansNaStart, decimal limitDebetowy)
        {
            konto = new Konto(klient, bilansNaStart);
            this.limitDebetowy = limitDebetowy;
        }

        public string Nazwa => konto.Nazwa;
        public decimal Bilans => konto.Bilans + limitDebetowy;
        public bool Zablokowane => konto.Zablokowane;

        public void Wplata(decimal kwota)
        {
            konto.Wplata(kwota);
            if (konto.Bilans >= 0) konto.OdblokujKonto();
        }

        public void Wyplata(decimal kwota)
        {
            konto.Wyplata(kwota);
            if (konto.Bilans < 0) konto.BlokujKonto();
        }
    }
}
