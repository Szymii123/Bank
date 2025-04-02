using Bank;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank
{
    public class KontoPlus : Konto
    {
        private decimal limitDebetowy;

        public KontoPlus(string klient, decimal bilansNaStart, decimal limitDebetowy) : base(klient, bilansNaStart)
        {
            this.limitDebetowy = limitDebetowy;
        }

        public decimal LimitDebetowy
        {
            get => limitDebetowy;
            set
            {
                if (value < 0) throw new ArgumentException("Limit debetowy musi być nieujemny.");
                limitDebetowy = value;
            }
        }

        public new decimal Bilans => base.Bilans + limitDebetowy;

        public new void Wyplata(decimal kwota)
        {
            base.Wyplata(kwota);
            if (base.Bilans < 0) BlokujKonto();
        }

        public new void Wplata(decimal kwota)
        {
            base.Wplata(kwota);
            if (base.Bilans >= 0) OdblokujKonto();
        }

    }
}

