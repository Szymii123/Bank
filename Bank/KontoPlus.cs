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

        public KontoPlus(string klient, decimal bilansNaStart, decimal limitDebetowy) : base(klient, bilansNaStart + limitDebetowy)
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

        public new void Wyplata(decimal kwota)
        {
            base.Wyplata(kwota);
            if (base.Bilans < limitDebetowy) BlokujKonto();
        }

        public new void Wplata(decimal kwota)
        {
            base.Wplata(kwota);
            if (base.Bilans >= limitDebetowy) OdblokujKonto();
        }

    }
}

